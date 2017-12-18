using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MerakiDashboard;
using Newtonsoft.Json;

namespace Meraki.Dashboard.Console
{
    internal class Test
    {
        public async Task Run(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("Cannot be null, empty or whitespace", nameof(apiKey));
            }

            using (MerakiDashboardClient merakiDashboardClient =
                MerakiDashboardClientFactory.Create(mcs => mcs.ApiKey = apiKey))
            using(new MerakiHttpApiDebugContext())
            {
                string organizationId = merakiDashboardClient.GetOrganizationsAsync().Result.First().Id;
                await GetSnmpSettings(organizationId, merakiDashboardClient);
                await PutSnmpSettings(organizationId, merakiDashboardClient);
            }
        }

        private static async Task GetSnmpSettings(string organizationId, MerakiDashboardClient merakiDashboardClient)
        {
            SnmpGetSettings snmpGetSettings = await merakiDashboardClient.GetOrganizationSnmpSettingsAsync(organizationId);
            System.Console.Out.WriteLine(JsonConvert.SerializeObject(snmpGetSettings));
        }

        private static async Task PutSnmpSettings(string organizationId, MerakiDashboardClient merakiDashboardClient)
        {
            SnmpPutSettings snmpPutSettings = new SnmpPutSettings
            {
                V2cEnabled = false,
                V3Enabled = true,
                V3AuthenticationMode = SnmpAuthenticationMode.Sha,
                V3AuthenticationPassword = "auth password",
                V3PrivacyMode = SnmpPrivacyMode.Aes128,
                V3PrivacyPassword = "privacy password",
                PeerIps = new[] { IPAddress.Parse("8.8.8.8") }
            };

            System.Console.Out.WriteLine(await merakiDashboardClient.PutOrganizationSnmpSettingsAsync(organizationId, snmpPutSettings));
        }
    }
}
