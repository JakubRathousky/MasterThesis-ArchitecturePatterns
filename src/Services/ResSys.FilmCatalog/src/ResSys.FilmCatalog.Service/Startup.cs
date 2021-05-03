using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ResSys.Common.MassTransit;
using ResSys.Common.MongoDB;
using ResSys.Common.Settings;
using ResSys.FilmCatalog.Service.Entities;
using ResSys.FilmCatalog.Service.Clients;
using ResSys.FilmCatalog.Service.Interfaces;
using Polly.Timeout;
using Polly;
using System.Net.Http;
using ResSys.Common.HealthChecksService;
using ResSys.FilmCatalog.Service.Consumers;

namespace ResSys.FilmCatalog
{
    public class Startup
    {
        private ServiceSettings serviceSettings;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            serviceSettings = Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

            services
                .AddMongo()
                    .AddMongoRepository<Film>("filmCatalog")
                    .AddMongoRepository<Transaction>("transaction")
            .AddMassTransitWithRabbitMQ(typeof(SupplyFilmConsumer));

            AddClientSynchronize(services);

            // Confiugre Health Checks
            services.AddHealthCheck();
            services.AddScoped<IFilmNotifier, FilmNotifier>();
            services.AddScoped<IFilmService, FilmService>();
            services.AddScoped<IAuthorClient, AuthorClient>();


            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ResSys.FilmCatalog", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ResSys.FilmCatalog v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.AddHealthCheckEndpoints();
            });
        }

        private static void AddClientSynchronize(IServiceCollection services)
        {
            Random jitterer = new Random();
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();

            services.AddHttpClient<AuthorClient>(client =>
            {
                var hostInfo = configuration.GetSection(nameof(SynchClient)).Get<SynchClient>();
                client.BaseAddress = new System.Uri(hostInfo.Host);
            })
            // Exponenciílní opakování dotazu při selhání 
            .AddTransientHttpErrorPolicy(builder => builder.Or<TimeoutRejectedException>().WaitAndRetryAsync(
                5,
                retryAttemp => TimeSpan.FromSeconds(Math.Pow(2, retryAttemp)) + TimeSpan.FromMilliseconds(jitterer.Next(0, 10000)),
                onRetry: (outcome, timespan, retryAttempt) =>
                {
                    var serviceProvider = services.BuildServiceProvider();
                    serviceProvider.GetService<ILogger<AuthorClient>>()?
                    .LogWarning($"Dealying for {timespan.TotalSeconds} seconds, then making retry {retryAttempt}");
                }
            ))
            // Circuit breaker pattern - když je velká šance selhání dotazu, automaticky zabije dotazy a čeká určitý čas než opět povolí
            .AddTransientHttpErrorPolicy(builder => builder.Or<TimeoutRejectedException>().CircuitBreakerAsync(
                3,
                TimeSpan.FromSeconds(15),
                onBreak: (outcome, timespan) =>
                {
                    var serviceProvider = services.BuildServiceProvider();
                    serviceProvider.GetService<ILogger<AuthorClient>>()?
                    .LogWarning($"Opening the circuit for {timespan.TotalSeconds} seconds...");
                },
                onReset: () =>
                {
                    var serviceProvider = services.BuildServiceProvider();
                    serviceProvider.GetService<ILogger<AuthorClient>>()?
                    .LogWarning($"Closing the circuit...");
                }
            ))
            // After 15s services gives up and throws error
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));
        }
    }

    class SynchClient
    {
        public string Host { get; set; }
    }
}
