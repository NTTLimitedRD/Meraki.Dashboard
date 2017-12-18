using System;

namespace Meraki.Dashboard.Converters
{
    /// <summary>
    /// Convert to and from Unix timestamp, a <see cref="double"/> counting the 
    /// number of seconds since the start of the Unix Epoch, and 
    /// <see cref="DateTime"/>. Unix  timestamps are used by the Meraki 
    /// Dashboard APIs.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Extension methods were considered but there are potentially multiple
    /// conversions between double and times, e.g. OLE automation dates, so
    /// it was left as statics.
    /// </para>
    /// <para>
    /// Note there may be some accuracy lost when converting due to rounding
    /// or loss of precision.
    /// </para>
    /// </remarks>
    internal static class UnixTimestampConverter
    {
        /// <summary>
        /// Convert from a timestamp to a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="timestamp">
        /// The timestamp to convert.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/> representing the same date and time.
        /// </returns>
        public static DateTime ToDateTime(double timestamp)
        {
            return EpochStart.AddSeconds(timestamp);
        }

        /// <summary>
        /// Convert <see cref="DateTime"/> to a timestamp.
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/> to convert.
        /// </param>
        /// <returns>
        /// A <see cref="double"/> represeting the same date and time.
        /// </returns>
        public static double FromDateTime(DateTime dateTime)
        {
            return dateTime.Subtract(EpochStart).TotalSeconds;
        }

        /// <summary>
        /// Unix Epoch date and time.
        /// </summary>
        public static readonly DateTime EpochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }
}
