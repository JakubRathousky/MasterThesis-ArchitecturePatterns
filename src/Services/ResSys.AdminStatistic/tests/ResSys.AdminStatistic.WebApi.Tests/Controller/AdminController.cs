using Microsoft.AspNetCore.Hosting;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.TestHost;
using ResSys.AdminStatistic.WebApi;
using Xunit;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using System;


namespace ResSys.AdminStatistic.WebApi.Tests
{
    public class AdminController
    {
        private readonly TestServer server;
        private readonly HttpClient client;

        public AdminController()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    // IHostingEnvironment env = builderContext.HostingEnvironment;
                    config.AddJsonFile("autofac.entityframework.json")
                    .AddEnvironmentVariables();
                })
                .ConfigureServices(services => services.AddAutofac());

            server = new TestServer(webHostBuilder);
            client = server.CreateClient();
        }

        [Fact]
        public async Task Some_Method()
        {

        }
    }
}
