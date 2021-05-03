using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ResSys.Common.MassTransit;
using ResSys.Common.MongoDB;
using ResSys.Common.Settings;
using System;
using ResSys.AuthorCatalog.Service.Entities;
using ResSys.AuthorCatalog.Service;

using ResSys.Common.HealthChecksService;
namespace ResSys.AuthorCatalog
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
                    .AddMongoRepository<Author>("authorCatalog")
                .AddMassTransitWithRabbitMQ();
            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });
            // Confiugre Health Checks
            services.AddHealthCheck();
            services.AddScoped<ISeedService, SeedService>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ResSys.AuthorCatalog", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ResSys.AuthorCatalog v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.AddHealthCheckEndpoints();
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
               .CreateScope())
            {
                serviceScope.ServiceProvider.SeedDatabase();
            }
        }
    }
    public static class Config
    {
        public static void SeedDatabase(this IServiceProvider serviceCollection)
        {
            var uow = serviceCollection.GetService<ISeedService>();
            uow.Seed();
        }
    }
}
