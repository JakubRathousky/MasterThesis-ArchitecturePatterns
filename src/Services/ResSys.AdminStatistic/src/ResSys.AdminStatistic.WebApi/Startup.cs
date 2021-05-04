using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Autofac.Configuration;
using Autofac;
using MassTransit;
using ResSys.Common;
using ResSys.Common.HealthChecksService;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace ResSys.AdminStatistic.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMassTransitHostedService();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddControllersWithViews();
            services.AddHealthCheck();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ConfigurationModule(Configuration));
            // builder.RegisterModule(new AutofacModule());
            // builder.RegisterModule<BusModule>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // endpoints.AddHealthCheckEndpoints();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = reg => reg.Tags.Contains("service"),
                    ResponseWriter = HealthChecks.UI.Client.UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}
