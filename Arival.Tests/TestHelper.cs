using Framework.DependencyConfig;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Arival.Tests
{
    public static class TestHelper
    {
        public static ServiceCollection Services { get; private set; }
        public static ServiceProvider ServiceProvider { get; private set; }
        static TestHelper()
        {
            Services = new ServiceCollection();
            
        }
        public static void DoConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            ServiceProvider = ApplicationConfig.DoConfig(Services, configuration);
        }
    }
}
