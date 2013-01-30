using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace CTCT.Util
{
    /// <summary>
    /// Extansions class.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// ISO-8601 date time format string.
        /// </summary>
        public const string ISO8601 = "yyyy-MM-ddTHH:mm:ss.fffZ";

        /// <summary>
        /// Converts a DateTime object to an ISO8601 representation.
        /// </summary>
        /// <param name="dateTime">DateTime.</param>
        /// <returns>Returns the ISO8601 string representation for the provided datetime.</returns>
        public static string ToISO8601String(this DateTime dateTime)
        {
            return dateTime.ToString(ISO8601, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Gets the DateTime from an ISO8601 string.
        /// </summary>
        /// <param name="str">String.</param>
        /// <returns>Returns a datetime object.</returns>
        public static DateTime FromISO8601String(this string str)
        {
            return DateTime.ParseExact(str, ISO8601, CultureInfo.InvariantCulture);
        }
    }
}
