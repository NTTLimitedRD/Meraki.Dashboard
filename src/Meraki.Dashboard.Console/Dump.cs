using System;
using System.Threading.Tasks;
using MerakiDashboard;

namespace Meraki.Dashboard.Console
{
    internal class Dump
    {
        public async Task Run(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("Cannot be null, empty or whitespace", nameof(apiKey));
            }

            using (MerakiDashboardClient merakiDashboardClient =
                MerakiDashboardClientFactory.Create(mcs => mcs.ApiKey = apiKey))
            {
                System.Console.WriteLine("By organization by network:");
                foreach (Organization organization in await merakiDashboardClient.GetOrganizationsAsync())
                {
                    System.Console.WriteLine($"{organization.Name} ({organization.Id}):");
                    foreach (Network network in await merakiDashboardClient.GetOrganizationNetworksAsync(organization))
                    {
                        System.Console.WriteLine($"  {network.Name} ({network.Id}):");
                        foreach (Device device in await merakiDashboardClient.GetNetworkDevicesAsync(network))
                        {
                            System.Console.WriteLine($"    {device.Serial} ({device.Model}, {device.Mac})");
                        }
                    }
                }

                System.Console.WriteLine();
                System.Console.WriteLine("By organization inventory:");
                foreach (Organization organization in await merakiDashboardClient.GetOrganizationsAsync())
                {
                    System.Console.WriteLine($"{organization.Name} ({organization.Id}):");
                    foreach (InventoryEntry inventory in await merakiDashboardClient.GetOrganizationInventoryAsync(
                        organization))
                    {
                        System.Console.WriteLine(
                            $"    {inventory.Serial} ({inventory.Model}) claimed at {inventory.ClaimedAt}");
                    }
                }
            }
        }
    }
}
