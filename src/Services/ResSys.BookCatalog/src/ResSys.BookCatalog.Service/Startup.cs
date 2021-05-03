using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Timeout;
using ResSys.BookCatalog.Service.Clients;
using ResSys.BookCatalog.Service.Entities;
using ResSys.Common.MassTransit;
using ResSys.Common.MongoDB;
using ResSys.Common.Settings;
using ResSys.Common.HealthChecksService;
using ResSys.BookCatalog.Service.Consumers;
namespace ResSys.BookCatalog
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
                    .AddMongoRepository<Book>("bookCatalog")
                    .AddMongoRepository<Transaction>("transaction")
                .AddMassTransitWithRabbitMQ(typeof(SupplyBookConsumer));

            // Confiugre Health Checks
            services.AddHealthCheck();
            AddCatalogClientSynchronize(services);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ResSys.BookCatalog", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ResSys.BookCatalog v1"));
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
        private static void AddCatalogClientSynchronize(IServiceCollection services)
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
            // po 1s to vzdá a hodí chybu při dotazování
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));
        }
    }
    class SynchClient
    {
        public string Host { get; set; }
    }
}
