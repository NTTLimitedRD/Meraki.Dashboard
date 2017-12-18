using System.Runtime.Serialization;

// Ignore XML documentation warnings
#pragma warning disable CS1591

namespace Meraki.Dashboard
{
    /// <summary>
    /// A Meraki Network.
    /// </summary>
    [DataContract]
    public class Network
    {
        [DataMember(Name="id")]
        public string Id { get; set; }

        [DataMember(Name= "organizationId")]
        public string OrganizationId { get; set; }

        [DataMember(Name = "type")]
        public string NetworkType { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "timeZone")]
        public string TimeZone { get; set; }

        [DataMember(Name = "tags")]
        public string Tags { get; set; }
    }
}
