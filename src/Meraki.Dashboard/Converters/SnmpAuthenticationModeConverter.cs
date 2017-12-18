using System;
using MerakiDashboard;

namespace Meraki.Dashboard.Converters
{
    /// <summary>
    /// Convert to and from <see cref="SnmpAuthenticationMode"/> and the raw string version
    /// used by the Meraki Dashboard API.
    /// </summary>
    internal class SnmpAuthenticationModeConverter
    {
        /// <summary>
        /// Convert from a Meraki raw authentication mode format string to a <see cref="SnmpAuthenticationMode"/>.
        /// </summary>
        /// <param name="raw">
        /// The Meraki raw format.
        /// </param>
        /// <returns>
        /// The <see cref="SnmpAuthenticationMode"/> representing the same mode.
        /// </returns>
        public static SnmpAuthenticationMode ToEnum(string raw)
        {
            return !string.IsNullOrWhiteSpace(raw)
                ? (SnmpAuthenticationMode) Enum.Parse(typeof(SnmpAuthenticationMode), raw, true)
                : SnmpAuthenticationMode.Unknown;
        }

        /// <summary>
        /// Convert <see cref="SnmpAuthenticationMode"/> to a Meraki raw format.
        /// </summary>
        /// <param name="authenticationMode">
        /// The <see cref="SnmpAuthenticationMode"/> to convert.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> represeting the same mode.
        /// </returns>
        public static string FromEnum(SnmpAuthenticationMode authenticationMode)
        {
            return authenticationMode != SnmpAuthenticationMode.Unknown
                ? authenticationMode.ToString().ToUpper()
                : null;
        }
    }
}
