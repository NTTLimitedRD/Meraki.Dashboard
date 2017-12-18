using System;
using MerakiDashboard;

namespace Meraki.Dashboard.Converters
{
    /// <summary>
    /// Convert to and from <see cref="SnmpPrivacyMode"/> and the raw string version
    /// used by the Meraki Dashboard API.
    /// </summary>
    internal class SnmpPrivacyModeConverter
    {
        /// <summary>
        /// Convert from a Meraki raw privacy mode format string to a <see cref="SnmpPrivacyMode"/>.
        /// </summary>
        /// <param name="raw">
        /// The Meraki raw format.
        /// </param>
        /// <returns>
        /// The <see cref="SnmpPrivacyMode"/> representing the same mode.
        /// </returns>
        public static SnmpPrivacyMode ToEnum(string raw)
        {
            return !string.IsNullOrWhiteSpace(raw)
                ? (SnmpPrivacyMode) Enum.Parse(typeof(SnmpPrivacyMode), raw, true)
                : SnmpPrivacyMode.Unknown;
        }

        /// <summary>
        /// Convert <see cref="SnmpPrivacyMode"/> to a Meraki raw format.
        /// </summary>
        /// <param name="privacyMode">
        /// The <see cref="SnmpPrivacyMode"/> to convert.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> represeting the same mode.
        /// </returns>
        public static string FromEnum(SnmpPrivacyMode privacyMode)
        {
            return privacyMode != SnmpPrivacyMode.Unknown
                ? privacyMode.ToString().ToUpper()
                : null;
        }
    }
}
