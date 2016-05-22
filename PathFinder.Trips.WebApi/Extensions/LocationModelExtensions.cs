// file:	extensions\locationmodelextensions.cs
//
// summary:	Implements the locationmodelextensions class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder.Trips.WebApi.Models;

namespace PathFinder.Trips.WebApi.Extensions
{
    /// <summary>   A location model extensions. </summary>
    ///
    /// <remarks>   Vladyslav, 25.05.2016. </remarks>

    internal static class LocationModelExtensions
    {
        /// <summary>   The delimiter. </summary>
        private const char Delimiter = '|';

        /// <summary>
        ///     A GooglePlaceModel extension method that converts a place to a lat long string.
        /// </summary>
        ///
        /// <remarks>   Vladyslav, 25.05.2016. </remarks>
        ///
        /// <param name="place">    The place to act on. </param>
        ///
        /// <returns>   place as a string. </returns>

        private static string ToLatLongString(this GooglePlaceModel place)
        {
            if (place.Geometry == null) return string.Empty;

            return string.Format("{0},{1}", place.Geometry.Location.Lat.ToGoogleString(), place.Geometry.Location.Lng.ToGoogleString());
        }

        /// <summary>
        ///     An IEnumerable&lt;GooglePlaceModel&gt; extension method that prepare waypoints request
        ///     string.
        /// </summary>
        ///
        /// <remarks>   Vladyslav, 25.05.2016. </remarks>
        ///
        /// <param name="waypoints">    The waypoints to act on. </param>
        ///
        /// <returns>   A string. </returns>

        public static string PrepareWaypointsRequestString(this IEnumerable<GooglePlaceModel> waypoints)
        {
            return waypoints.Aggregate(string.Empty, (str, waypoint) => str + (waypoint.ToLatLongString() + Delimiter))
                .TrimEnd(Delimiter);
        }
    }
}
