using System;

namespace Meraki.Dashboard
{
    internal class UrlFormatter : ICustomFormatter
    {
        /// <summary>
        /// 
        /// </summary>
        public const string DoNotEscape = "r";

        /// <summary>
        /// Format the given string, escaping substituted values for inclusion in a URI.
        /// </summary>
        /// <param name="format">
        /// The format specififer. The only supported specifier is <see cref="DoNotEscape"/>, 
        /// which does not escape the value.
        /// </param>
        /// <param name="arg">
        /// The argument to insert into the string.
        /// </param>
        /// <param name="formatProvider">
        /// Ignored.
        /// </param>
        /// <returns>
        /// The formatted string.
        /// </returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null)
            {
                return string.Empty;
            }
            if (format == "r")
            {
                return arg.ToString();
            }
            if (!string.IsNullOrEmpty(format))
            {
                throw new FormatException($"Unknown format specifier '{arg}'");
            }

            return Uri.EscapeDataString(arg.ToString());
        }
    }
}