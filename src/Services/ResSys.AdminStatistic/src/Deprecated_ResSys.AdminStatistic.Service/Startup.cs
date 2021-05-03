using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ResSys.Common.HealthChecksService;
using Microsoft.EntityFrameworkCore;
using ResSys.AdminStatistic.Service.Data;
using ResSys.AdminStatistic.Service.Repository;
using ResSys.Common.MassTransit;
using ResSys.Common;
// using System.Data.Entity;
namespace ResSys.AdminStatistic
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
            services.AddDbContext<AdminStatisticContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AdminStat")));
            services.AddScoped(typeof(IRepository<>), typeof(MSSQLRepository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            // Confiugre Health Checks
            services.AddHealthCheck();

            services
                .AddMassTransitWithRabbitMQ();

            // services.AddDbContext<AdminStatisticContext>(options => options.UseNpgsql(Configuration.GetConnectionString("AdminStatisticContext")));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ResSys.AdminStatistics", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ResSys.AdminStatistics v1"));
            }
            UpdateDatabase(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.AddHealthCheckEndpoints();
            });
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<AdminStatisticContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
