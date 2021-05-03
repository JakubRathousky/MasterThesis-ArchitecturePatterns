using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;

namespace ResSys.AdminStatistic.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    // Microsoft.Extensions.Hosting.IHostingEnvironment env = (Microsoft.Extensions.Hosting.IHostingEnvironment)builderContext.HostingEnvironment;
                    config.AddJsonFile("autofac.entityframework.json");
                    // config.AddJsonFile("autofac.dev.json");
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices(services => services.AddAutofac())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
