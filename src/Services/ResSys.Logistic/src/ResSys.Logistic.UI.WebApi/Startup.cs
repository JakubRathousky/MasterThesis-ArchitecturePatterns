using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ResSys.Logistic.Infrastructure.IoC.MongoDB;
using ResSys.Logistic.Infrastructure.IoC.MassTransit;
using ResSys.Common.MongoDB;
using ResSys.Common.Settings;
using ResSys.Common.HealthChecksService;
using ResSys.Logistic.Infrastructure.IoC.Interfaces;
using ResSys.Logistic.Infrastructure.Data.MassTransit.Notifier;

namespace ResSys.Logistic
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
                    .RegisterServices()
                    .RegisterMongo("logisticStore")
                    .RegisterMassTransit();
            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });

            // Confiugre Health Checks
            services.AddHealthCheck();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ResSys.Logistic", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ResSys.Logistic v1"));
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
    }
}
