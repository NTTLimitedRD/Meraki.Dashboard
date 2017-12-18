using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using Meraki.Dashboard.Converters;

// Ignore XML documentation warnings
#pragma warning disable CS1591

namespace MerakiDashboard
{
    [DataContract]
    public class InventoryEntry
    {
        [DataMember(Name="mac")]
        public string Mac { get; set; }

        [DataMember(Name = "serial")]
        public string Serial { get; set;  }

        [DataMember(Name = "networkId")]
        public string NetworkId { get; set; }

        [DataMember(Name = "model")]
        public string Model { get; set; }

        /// <summary>
        /// Set <see cref="ClaimedAt"/> by converting the string to a <see cref="DateTime"/>.
        /// </summary>
        [DataMember(Name = "claimedAt")]
        public double ClaimedAtRaw
        {
            get => UnixTimestampConverter.FromDateTime(ClaimedAt);
            set => ClaimedAt = UnixTimestampConverter.ToDateTime(value);
        }

        [IgnoreDataMember]
        public DateTime ClaimedAt { get; set; }

        /// <summary>
        /// Set <see cref="PublicIpAddress"/> by converting the string to an <see cref="IPAddress"/>.
        /// </summary>
        [DataMember(Name = "publicIp")]
        public string PublicIpAddressRaw
        {
            get => PublicIpAddress?.ToString();
            set => PublicIpAddress = string.IsNullOrWhiteSpace(value) ? IPAddress.None : IPAddress.Parse(value);
        }

        [IgnoreDataMember]
        public IPAddress PublicIpAddress { get; set; }
    }
}
