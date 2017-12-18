using System;
using System.Globalization;

namespace Meraki.Dashboard.Converters
{
    /// <summary>
    /// Convert to and from the Meraki licence expiration date 
    /// format and <see cref="DateTime"/>s.
    /// </summary>
    /// <remarks>
    /// Extension methods were considered but there are potentially multiple
    /// conversions between strings and date/times so it was left as statics.
    /// </remarks>
    internal class LicenceExpirationDateConverter
    {
        /// <summary>
        /// Convert from a Meraki licence expiration date format string to a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="licenceExpirationDate">
        /// The Meraki license expiration date to convert.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/> representing the same date and time.
        /// </returns>
        public static DateTime ToDateTime(string licenceExpirationDate)
        {
            return DateTime.ParseExact(
                licenceExpirationDate.Replace("UTC", "+00"), 
                ExpirationDateFormat, 
                CultureInfo.InvariantCulture, 
                DateTimeStyles.AllowWhiteSpaces).ToUniversalTime();
        }

        /// <summary>
        /// Convert <see cref="DateTime"/> to a Meraki licence expiration date format string.
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/> to convert.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> represeting the same date and time.
        /// </returns>
        public static string FromDateTime(DateTime dateTime)
        {
            return dateTime.ToString(
                ExpirationDateFormat, 
                CultureInfo.InvariantCulture).Replace("+00", "UTC");
        }

        /// <summary>
        /// Date format used by the Meraki Dashboard API for the licence expiration 
        /// date (with the time zone converted).
        /// </summary>
        public static readonly string ExpirationDateFormat = "MMM dd, yyyy zz";

    }
}
