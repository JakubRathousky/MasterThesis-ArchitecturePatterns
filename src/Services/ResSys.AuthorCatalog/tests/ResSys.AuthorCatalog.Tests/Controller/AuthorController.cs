using Microsoft.AspNetCore.Hosting;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.TestHost;
using ResSys.AuthorCatalog;
using Xunit;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ResSys.AuthorCatalog.WebApi.Tests
{
    // To run integration test localy, you have to set appsettings.dev.json as primary
    public class AuthorControllerTest
    {
        private readonly TestServer server;
        private readonly HttpClient client;

        public AuthorControllerTest()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                });

            this.server = new TestServer(webHostBuilder);
            this.client = server.CreateClient();
        }

        [Fact]
        public async Task Get_Authors()
        {
            string result = await this.client.GetStringAsync("/authors");

            List<JObject> authors = JsonConvert.DeserializeObject<List<JObject>>(result);
            Assert.Equal(9, authors.Count());
        }

        [Fact]
        public async Task Get_Author_With_RegNum()
        {
            string result = await this.client.GetStringAsync("/authors/7");

            Assert.Contains("b6cdbf2f-53c2-4b57-b890-8743431fc0e2", result);
        }
    }
}