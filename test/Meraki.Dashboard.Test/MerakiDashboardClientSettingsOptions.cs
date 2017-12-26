using MerakiDashboard;
using Microsoft.Extensions.Options;

namespace Meraki.Dashboard.Test
{
    internal class MerakiDashboardClientSettingsOptions: IOptions<MerakiDashboardClientOptions>
    {
        /// <summary>The configured TOptions instance.</summary>
        public MerakiDashboardClientOptions Value { get; set; }
    }
}
