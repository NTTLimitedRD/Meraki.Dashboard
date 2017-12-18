using System;
using System.Net;
using System.Runtime.Serialization;

// Ignore XML documentation warnings from here on. 
#pragma warning disable CS1591

namespace Meraki.Dashboard
{
    /// <summary>
    /// A meraki device.
    /// </summary>
    [DataContract]
    public class Device: IEquatable<Device>
    {
        [DataMember(Name="name")]
        public string Name { get; set; }

        [DataMember(Name="lat")]
        public double Lattitude { get; set; }

        [DataMember(Name = "lng")]
        public double Longitude { get; set; }

        [DataMember(Name = "serial")]
        public string Serial { get; set; }

        [DataMember(Name = "mac")]
        public string Mac { get; set; }

        [DataMember(Name = "model")]
        public string Model { get; set; }

        [DataMember(Name = "address")]
        public string Address { get; set; }

        /// <summary>
        /// Set <see cref="LanIpAddress"/> by converting the string to an <see cref="IPAddress"/>.
        /// </summary>
        [DataMember(Name = "lanIp")]
        public string LanIpAddressRaw
        {
            get => LanIpAddress?.ToString();
            set => LanIpAddress = string.IsNullOrWhiteSpace(value) ? IPAddress.None : IPAddress.Parse(value);
        }

        [IgnoreDataMember]
        public IPAddress LanIpAddress { get; set; }

        [DataMember(Name = "tags")]
        public string Tags { get; set; }

        [DataMember(Name = "networkdId")]
        public string NetworkId { get; set; }

        /// <summary>
        /// Are two <see cref="Device"/> objects equal?
        /// </summary>
        /// <param name="obj">
        /// The objec to compare.
        /// </param>
        /// <returns>
        /// True if they are equal, false otherwise.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Device) obj);
        }

        /// <summary>
        /// Get a hash code.
        /// </summary>
        /// <returns>
        /// The hash code.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Lattitude.GetHashCode();
                hashCode = (hashCode * 397) ^ Longitude.GetHashCode();
                hashCode = (hashCode * 397) ^ (Serial != null ? Serial.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Mac != null ? Mac.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Model != null ? Model.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Address != null ? Address.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LanIpAddress != null ? LanIpAddress.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Tags != null ? Tags.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (NetworkId != null ? NetworkId.GetHashCode() : 0);
                return hashCode;
            }
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">
        /// An object to compare with this object.
        /// </param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Device other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) 
                && Lattitude.Equals(other.Lattitude) 
                && Longitude.Equals(other.Longitude) 
                && string.Equals(Serial, other.Serial) 
                && string.Equals(Mac, other.Mac) 
                && string.Equals(Model, other.Model) 
                && string.Equals(Address, other.Address) 
                && LanIpAddress.Equals(other.LanIpAddress) 
                && string.Equals(Tags, other.Tags) 
                && string.Equals(NetworkId, other.NetworkId);
        }
    }
}
