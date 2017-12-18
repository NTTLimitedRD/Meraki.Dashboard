using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MerakiDashboard;
using Xunit;

namespace Meraki.Dashboard.Test
{
    public class TestInventoryEntry
    {
        [Theory]
        [MemberData(nameof(ClaimedAtRawTestData))]
        public void ClaimedAtRaw(string raw, DateTime expectedDateTime)
        {
            InventoryEntry inventoryEntry = new InventoryEntry {ClaimedAtRaw = double.Parse(raw)};
            Assert.Equal(expectedDateTime, inventoryEntry.ClaimedAt);
            Assert.Equal(double.Parse(raw), inventoryEntry.ClaimedAtRaw, 3);
        }

        public static IEnumerable<object[]> ClaimedAtRawTestData()
        {
            return TestUnixTimestampConverter.ConvertTestData();
        }

        [Theory]
        [MemberData(nameof(PublicIpAddressRawTestData))]
        public void PublicIpAddressRaw(string raw, IPAddress expectedIpAddress)
        {
            InventoryEntry inventoryEntry = new InventoryEntry {PublicIpAddressRaw = raw};
            Assert.Equal(expectedIpAddress, inventoryEntry.PublicIpAddress);
            Assert.Equal(raw, inventoryEntry.PublicIpAddressRaw);
        }

        public static IEnumerable<object[]> PublicIpAddressRawTestData()
        {
            return new[] {"198.27.138.150", "198.27.138.150", "198.27.138.150", "0.0.0.0", "255.255.255.255"}
                .Select(ipAddress => new object[] {ipAddress, IPAddress.Parse(ipAddress)});
        }
    }
}
