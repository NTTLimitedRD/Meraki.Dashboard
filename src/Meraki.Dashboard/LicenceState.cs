using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using Meraki.Dashboard.Converters;

// Ignore XML documentation warnings from here on. 
#pragma warning disable CS1591

namespace MerakiDashboard
{
    [DataContract]
    public class LicenceState
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "expirationDate")]
        public string ExpirationDateRaw
        {
            get => LicenceExpirationDateConverter.FromDateTime(ExpirationDate);
            set => ExpirationDate = LicenceExpirationDateConverter.ToDateTime(value);
        }

        [IgnoreDataMember]
        public DateTime ExpirationDate { get; set; }

        [DataMember(Name = "licensedDeviceCounts")]
        public IDictionary<string, uint> LicensedDeviceCounts { get; set; }
    }
}
