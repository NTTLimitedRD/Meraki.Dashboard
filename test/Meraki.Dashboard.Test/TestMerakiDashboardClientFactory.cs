using System;
using Xunit;

namespace Meraki.Dashboard.Test
{
    public class TestMerakiDashboardClientFactory
    {
        [Fact]
        public void Create_ApiKey()
        {
            const string apiKey = "000111222333444555666777888999000aaabbbbcccdddeee";

            using (MerakiDashboardClient merakiDashboardClient = MerakiDashboardClientFactory.Create(apiKey))
            {
                Assert.Equal(MerakiDashboardClientOptions.DefaultMerakiDashboardApiBaseAddress, merakiDashboardClient.Client.BaseAddress);
                Assert.Equal(apiKey, merakiDashboardClient.Client.ApiKey);
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_ApiKey_Null(string apiKey)
        {
            Assert.Throws<ArgumentException>("apiKey", () => MerakiDashboardClientFactory.Create(apiKey));
        }
    }
}
