// file:	extensions\doubleextensions.cs
//
// summary:	Implements the doubleextensions class

using System.Globalization;

namespace PathFinder.Trips.WebApi.Extensions
{
    /// <summary>   A double extensions. </summary>
    ///
    /// <remarks>   Vladyslav, 25.05.2016. </remarks>

    internal static class DoubleExtensions
    {
        /// <summary>   A double extension method that converts a value to a google string. </summary>
        ///
        /// <remarks>   Vladyslav, 25.05.2016. </remarks>
        ///
        /// <param name="value">    The value to act on. </param>
        ///
        /// <returns>   value as a string. </returns>

        public static string ToGoogleString(this double value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
