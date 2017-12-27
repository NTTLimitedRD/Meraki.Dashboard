using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

namespace Meraki.Dashboard.Test
{
    public class TestOptions
    {
        [Fact]
        public void Options()
        {
            // Ensure this matches the appSettings.json file.
            const string apiKey = "8A7H65KJ098MHGI09";

            using (ServiceProvider serviceProvider = BuildServiceProvider())
            {
                MerakiDashboardClient merakiDashboardClient = serviceProvider.GetService<MerakiDashboardClient>();
                Assert.Equal(apiKey, merakiDashboardClient.Client.ApiKey);
                Assert.Equal(MerakiDashboardClientOptions.DefaultMerakiDashboardApiBaseAddress, merakiDashboardClient.Client.BaseAddress);
            }
        }

        private static ServiceProvider BuildServiceProvider()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                            .AddJsonFile("appSettings.json")
                            .Build();

            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddOptions();
            // Ensure the section matches the appSettings.json file
            serviceCollection.Configure<MerakiDashboardClientOptions>(configuration.GetSection("merakiDashboard"));
            serviceCollection.AddScoped<MerakiDashboardClient>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
