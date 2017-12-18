using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Ignore XML documentation warnings
#pragma warning disable CS1591

namespace Meraki.Dashboard
{
    /// <summary>
    /// A Meraki Organization.
    /// </summary>
    [DataContract]
    public class Organization : IEquatable<Organization>
    {
        [DataMember(Name="id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Are two objects equal?
        /// </summary>
        /// <param name="obj">
        /// The objec to compare.
        /// </param>
        /// <returns>
        /// True if they are equal, false otherwise.
        /// </returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Organization);
        }

        /// <summary>
        /// Are two <see cref="Organization"/> objects equal?
        /// </summary>
        /// <param name="other">
        /// The objec to compare.
        /// </param>
        /// <returns>
        /// True if they are equal, false otherwise.
        /// </returns>
        public bool Equals(Organization other)
        {
            return other != null &&
                   Id == other.Id &&
                   Name == other.Name;
        }

        /// <summary>
        /// Get a hash code.
        /// </summary>
        /// <returns>
        /// The hash code.
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = -1919740922;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }
}