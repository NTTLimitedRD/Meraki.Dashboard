using System.Net;
using System.Runtime.Serialization;
using MerakiDashboard;

// Ignore XML documentation warnings from here on. 
#pragma warning disable CS1591

namespace Meraki.Dashboard
{
    [DataContract]
    public class Client
    {
        [DataMember(Name="description")]
        public string Description { get; set; }

        [DataMember(Name = "mdnsName")]
        public string MdnsName { get; set; }

        [DataMember(Name = "dhcpHostname")]
        public string DhcpHostname { get; set; }

        [DataMember(Name = "usage")]
        public Usage Usage { get; set; }

        [DataMember(Name = "mac")]
        public string Mac { get; set; }

        [DataMember(Name = "ip")]
        public IPAddress Ip { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "switchPort")]
        public uint SwitchPort { get; set; }
    }
}
