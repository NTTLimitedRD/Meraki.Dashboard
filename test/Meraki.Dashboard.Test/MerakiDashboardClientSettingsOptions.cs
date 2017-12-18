using MerakiDashboard;
using Microsoft.Extensions.Options;

namespace Meraki.Dashboard.Test
{
    internal class MerakiDashboardClientSettingsOptions: IOptions<MerakiDashboardClientSettings>
    {
        /// <summary>The configured TOptions instance.</summary>
        public MerakiDashboardClientSettings Value { get; set; }
    }
}
