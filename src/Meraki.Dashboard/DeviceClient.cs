using Newtonsoft.Json;

namespace Meraki.Dashboard
{
    /// <summary>
    /// A Meraki Device Client.
    /// </summary>
    public class DeviceClient
    {
        /// <summary>
        /// Gets or sets Device Client Id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets Description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets DNS Name.
        /// </summary>
        [JsonProperty("mdnsName")]
        public string MDnsName { get; set; }

        /// <summary>
        /// Gets or sets DHCP Hostname
        /// </summary>
        [JsonProperty("dhcpHostname")]
        public string DhcpHostName { get; set; }

        /// <summary>
        /// Gets or sets Device Mac Address
        /// </summary>
        [JsonProperty("mac")]
        public string MacAddress { get; set; }

        /// <summary>
        /// Gets or sets IP Address
        /// </summary>
        [JsonProperty("ip")]
        public string IPAddress { get; set; }

        /// <summary>
        /// Gets or sets VLAN
        /// </summary>
        [JsonProperty("vlan")]
        public string VLan { get; set; }

        /// <summary>
        /// Gets or sets Switch port
        /// </summary>
        [JsonProperty("switchport")]
        public string SwitchPort { get; set; }

        /// <summary>
        /// Gets or sets User
        /// </summary>
        [JsonProperty("user")]
        public string User { get; set; }
    }
}