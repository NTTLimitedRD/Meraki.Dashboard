using System;
using Microsoft.Extensions.Options;

namespace Meraki.Dashboard
{
    /// <summary>
    /// Initialize a <see cref="MerakiDashboardClientSettings"/> object.
    /// </summary>
    public class MerakiDashboardClientSettingsSetup : ConfigureOptions<MerakiDashboardClientSettings>
    {
        /// <summary>
        /// The default Meraki dashboard API base address.
        /// </summary>
        /// <remarks>
        /// This intentionally omits the training "/v0" as the version may change in future
        /// API releases.
        /// </remarks>
        public static readonly string DefaultMerakiDashboardApiBaseAddress = "https://dashboard.meraki.com/api/";

        /// <summary>
        /// Create a new <see cref="MerakiDashboardClientSettingsSetup"/> object.
        /// </summary>
        public MerakiDashboardClientSettingsSetup() : base(ConfigureOptions)
        {
            // Do nothing
        }

        /// <summary>
        /// Configure the <see cref="MerakiDashboardClientSettings"/> object.
        /// </summary>
        /// <param name="options">
        /// The <see cref="MerakiDashboardClientSettings"/> object to configure.
        /// </param>
        private static void ConfigureOptions(MerakiDashboardClientSettings options)
        {
            options.BaseAddress = new Uri(DefaultMerakiDashboardApiBaseAddress, UriKind.Absolute);
            options.ApiKey = "";
        }
    }
}