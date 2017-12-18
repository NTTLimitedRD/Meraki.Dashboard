using System.Runtime.Serialization;

// Ignore XML documentation warnings
#pragma warning disable CS1591

namespace Meraki.Dashboard
{
    [DataContract]
    public class SwitchPort
    {
        [DataMember(Name="number")]
        public uint Number { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "tags")]
        public string Tags { get; set; }

        [DataMember(Name = "enabled")]
        public bool Enabled { get; set; }

        [DataMember(Name = "type")]
        public string NetworkType { get; set; }

        [DataMember(Name = "vlan")]
        public uint? Vlan { get; set; }

        [DataMember(Name = "voiceVlan")]
        public uint? VoiceVlan { get; set; }

        [DataMember(Name = "poeEnabled")]
        public bool PoeEnabled { get; set; }

        [DataMember(Name = "isolationEnabled")]
        public bool IsolationEnabled { get; set; }

        [DataMember(Name = "rstpEnabled")]
        public bool RstpEnabled { get; set; }

        [DataMember(Name = "stpGuard")]
        public string StpGuard { get; set; }

        [DataMember(Name = "accessPolicyNumber")]
        public string AccessPolicyNumber { get; set; }
    }
}
