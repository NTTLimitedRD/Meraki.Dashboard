using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;

namespace Meraki.Dashboard.Test
{
    /// <summary>
    /// Ensure <see cref="MerakiDashboardClient"/> can be mocked.
    /// </summary>
    public class TestMerakiDashboardClientMocking
    {
        [Fact]
        public async void TestMocking()
        {
            Organization organization = new Organization
            {
                Name = "White house",
                Id = "Org Id"
            };
            Network network = new Network
            {
                Id = "Network Id",
                Name = "White House Network",
                NetworkType = "Wireless",
                OrganizationId = organization.Id,
                Tags = "tags",
                TimeZone = "UTC-06:00"
            };
            Device device = new Device
            {
                Address = "1600 Pennsylania Ave Washington DC",
                LanIpAddress = new IPAddress(new byte[] {127, 0, 0, 1}),
                Lattitude = 38.8977,
                Longitude = 77.0365,
                Mac = "00:00:00:00:00:00",
                Model = "MR33",
                Name = "Presidential AP",
                NetworkId = network.Id,
                Serial = "AAAA-BBBB-CCCC",
                Tags = "Tags"
            };

            MerakiDashboardClientSettings merakiDashboardClientSettings = new MerakiDashboardClientSettings
            {
                ApiKey = "api key",
                BaseAddress = new Uri("http://myapi.com")
            };

            Mock<MerakiDashboardClient> merakiDashboardlientMock = new Mock<MerakiDashboardClient>(MockBehavior.Strict, merakiDashboardClientSettings);
            merakiDashboardlientMock.Setup(merakiDashboardClient => merakiDashboardClient.GetOrganizationsAsync())
                                    .Returns(Task.FromResult(new [] {organization}));
            merakiDashboardlientMock.Setup(merakiDashboardClient => merakiDashboardClient.GetOrganizationNetworksAsync(organization))
                                    .Returns(Task.FromResult(new[] { network }));
            merakiDashboardlientMock.Setup(merakiDashboardClient => merakiDashboardClient.GetNetworkDevicesAsync(network))
                                    .Returns(Task.FromResult(new[] { device }));
            // Required by mocking framework
            merakiDashboardlientMock.Protected().Setup("Dispose", true);

            using (MerakiDashboardClient merakiDashboardClient = merakiDashboardlientMock.Object)
            {
                Assert.Equal(new[] {device}, await GetDevicesInAnOrganization(merakiDashboardClient));
            }
            merakiDashboardlientMock.VerifyAll();
        }

        /// <summary>
        /// Get the devices in all networks in all organizations the current API key can access.
        /// </summary>
        /// <param name="merakiDashboardClient">
        /// The <see cref="MerakiDashboardClient"/> to access the device swith.
        /// </param>
        /// <returns>
        /// The <see cref="Device"/>s discovered.
        /// </returns>
        private async Task<IEnumerable<Device>> GetDevicesInAnOrganization(MerakiDashboardClient merakiDashboardClient)
        {
            List<Device> result = new List<Device>();
            foreach (Organization organization in await merakiDashboardClient.GetOrganizationsAsync())
            {
                foreach (Network network in await merakiDashboardClient.GetOrganizationNetworksAsync(organization))
                {
                    foreach (Device device in await merakiDashboardClient.GetNetworkDevicesAsync(network))
                    {
                        result.Add(device);
                    }
                }
            }
            return result;
        }
    }
}
