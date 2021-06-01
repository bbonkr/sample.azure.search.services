using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Sample.App;
using System.Reflection;

namespace Sample.Services.Tests
{
    public class TestBase
    {
        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(TestBase)).Location;

            var hostBuilder = new WebHostBuilder()
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", optional: false);
                    cb.AddUserSecrets<TestBase>();
                }).UseStartup<Startup>();

            return new TestServer(hostBuilder);
        }
    }
}
