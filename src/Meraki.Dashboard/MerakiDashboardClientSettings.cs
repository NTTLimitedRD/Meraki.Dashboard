using System;
using System.Runtime.Serialization;
using MerakiDashboard;

namespace Meraki.Dashboard
{
    /// <summary>
    /// Configuration settings for a <see cref="MerakiDashboardClient"/>.
    /// </summary>
    [DataContract]
    public class MerakiDashboardClientSettings
    {
        /// <summary>
        /// The scheme and host name portion of the URL, usually 
        /// <see cref="MerakiDashboardClientSettingsSetup.DefaultMerakiDashboardApiBaseAddress"/>.
        /// </summary>
        [DataMember(Name = "Uri")]
        public Uri BaseAddress { get; set; }

        /// <summary>
        /// The API Key.
        /// </summary>
        [DataMember(Name = "ApiKey")]
        public string ApiKey { get; set; }
    }
}