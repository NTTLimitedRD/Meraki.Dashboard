using System.Runtime.Serialization;

// Ignore XML documentation warnings
#pragma warning disable CS1591

namespace Meraki.Dashboard
{
    [DataContract]
    public class Usage
    {
        [DataMember(Name="sent")]
        public uint Sent { get; set; }

        [DataMember(Name = "received")]
        public uint Received { get; set; }
    }
}
