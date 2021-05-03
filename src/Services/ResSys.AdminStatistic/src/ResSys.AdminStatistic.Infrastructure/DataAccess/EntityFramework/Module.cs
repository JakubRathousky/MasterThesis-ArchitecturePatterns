using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ResSys.AdminStatistic.Infrastructure.EntityFrameworkDataAccess
{
    /// <summary>
    /// Configuration of Autofac library (dependency injection)
    /// </summary>
    public class Module : Autofac.Module
    {
        public string ConnectionString { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseSqlServer(ConnectionString);
            optionsBuilder.EnableSensitiveDataLogging(true);

            builder.RegisterType<Context>()
              .WithParameter(new TypedParameter(typeof(DbContextOptions), optionsBuilder.Options))
              .InstancePerLifetimeScope();

            //
            // Register all Types in MongoDataAccess namespace
            //
            builder.RegisterAssemblyTypes(typeof(InfrastructureException).Assembly)
                .Where(type => type.Namespace.Contains("EntityFrameworkDataAccess"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterBuildCallback(x =>
            {
                using (var serviceScope = x.Resolve<IServiceScopeFactory>().CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetService<Context>())
                    {
                        context.Database.Migrate();
                    }
                }
            });
        }
    }
}
