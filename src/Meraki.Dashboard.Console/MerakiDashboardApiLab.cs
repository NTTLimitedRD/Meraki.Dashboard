using System;
using System.Threading.Tasks;
using MerakiDashboard;

namespace Meraki.Dashboard.Console
{
    /// <summary>
    /// Follow the exercises at https://learninglabs.cisco.com/modules/getting-started-with-meraki/meraki-dashboard-api.
    /// </summary>
    internal class MerakiDashboardApiLab
    {
        public async Task Run(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("Cannot be null, empty or whitespace", nameof(apiKey));
            }

            MerakiDashboardClient merakiDashboardClient = MerakiDashboardClientFactory.Create(mcs => mcs.ApiKey = apiKey);

            const string organizationName = "Meraki Live Sandbox";
            string organizationId = MerakiDashboardHelper.GetOrganizationId(merakiDashboardClient, organizationName).Result;

            foreach (Func<MerakiDashboardClient, string, Task> exercise in
                new Func<MerakiDashboardClient, string, Task>[] { })
            {
                await exercise(merakiDashboardClient, organizationId);
            }
        }
    }
}
