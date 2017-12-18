using System.Collections.Generic;
using System.Net;
using MerakiDashboard;
using Xunit;

namespace Meraki.Dashboard.Test
{
    public class TestDevice
    {
        [Theory]
        [MemberData(nameof(PublicIpAddressRawTestData))]
        public void LanIpAddressRaw(string raw, IPAddress expectedIpAddress)
        {
            Device inventoryEntry = new Device { LanIpAddressRaw = raw };
            Assert.Equal(expectedIpAddress, inventoryEntry.LanIpAddress);
            Assert.Equal(raw, inventoryEntry.LanIpAddressRaw);
        }

        public static IEnumerable<object[]> PublicIpAddressRawTestData()
        {
            return TestInventoryEntry.PublicIpAddressRawTestData();
        }
    }
}
