using System;
using System.Runtime.Serialization;
using MerakiDashboard;

namespace Meraki.Dashboard
{
    /// <summary>
    /// Configuration settings for a <see cref="MerakiDashboardClient"/>.
    /// </summary>
    [DataContract]
    public class MerakiDashboardClientOptions
    {
        /// <summary>
        /// The default Meraki dashboard API base address.
        /// </summary>
        /// <remarks>
        /// This intentionally omits the training "/v0" as the version may change in future
        /// API releases.
        /// </remarks>
        public static readonly Uri DefaultMerakiDashboardApiBaseAddress = new Uri("https://dashboard.meraki.com/api/");

        /// <summary>
        /// The scheme and host name portion of the URL, usually 
        /// <see cref="DefaultMerakiDashboardApiBaseAddress"/>.
        /// </summary>
        [DataMember(Name = "Uri")]
        public Uri BaseAddress { get; set; } = DefaultMerakiDashboardApiBaseAddress;

        /// <summary>
        /// The API Key.
        /// </summary>
        [DataMember(Name = "ApiKey")]
        public string ApiKey { get; set; }
    }
}