using System.Runtime.Serialization;
using Meraki.Dashboard.Converters;
using MerakiDashboard;

// Ignore XML documentation warnings
#pragma warning disable CS1591

namespace Meraki.Dashboard
{
    [DataContract]
    public class SnmpGetSettings
    {
        [DataMember(Name= "v2cEnabled")]
        public bool V2cEnabled { get; set; }

        [DataMember(Name= "v3Enabled")]
        public bool V3Enabled { get; set; }

        [DataMember(Name = "v3AuthMode")]
        public string V3AuthenticationModeRaw
        {
            get => SnmpAuthenticationModeConverter.FromEnum(V3AuthenticationMode);
            set => V3AuthenticationMode = SnmpAuthenticationModeConverter.ToEnum(value);
        }

        [IgnoreDataMember]
        public SnmpAuthenticationMode V3AuthenticationMode { get; set; }

        [DataMember(Name = "v3PrivMode")]
        public string V3PrivacyModeRaw
        {
            get => SnmpPrivacyModeConverter.FromEnum(V3PrivacyMode);
            set => V3PrivacyMode = SnmpPrivacyModeConverter.ToEnum(value);
        }

        [IgnoreDataMember]
        public SnmpPrivacyMode V3PrivacyMode { get; set; }

        [DataMember(Name = "hostname")]
        public string Hostname { get; set; }

        [DataMember(Name = "port")]
        public uint Port { get; set; }
    }
}
