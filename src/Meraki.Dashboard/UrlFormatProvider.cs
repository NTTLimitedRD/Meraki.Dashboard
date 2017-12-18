using System;

namespace Meraki.Dashboard
{
    /// <summary>
    /// A <see cref="IFormatProvider"/> that ensures data substituted unto a URI is escaped
    /// appropriately.
    /// </summary>
    internal class UrlFormatProvider : IFormatProvider
    {
        private static readonly UrlFormatter Formatter = new UrlFormatter();

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return Formatter;
            }

            return null;
        }
    }
}