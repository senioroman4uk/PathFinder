// file:	Queries\DistanceMatrixQuery.cs
//
// summary:	Implements the distance matrix query class

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PathFinder.Trips.WebApi.Extensions;
using PathFinder.Trips.WebApi.Models;

namespace PathFinder.Trips.WebApi.Queries
{
    /// <summary>   A distance matrix query. Generates matrix for route search algorithms </summary>
    ///
    /// <remarks>   Vladyslav, 24.05.2016. </remarks>

    internal class DistanceMatrixQuery : IDistanceMatrixQuery
    {
        private const string DistanseMatrixUrl = "https://maps.googleapis.com/maps/api/distancematrix/json?origins={0}&destinations={1}&key={2}";

        /// <summary>   The HTTP client. </summary>
        private readonly HttpClient _httpClient;

        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Vladyslav, 24.05.2016. </remarks>
        ///
        /// <param name="httpClient">   The HTTP client. </param>

        public DistanceMatrixQuery(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>   Gets distance matrix. </summary>
        ///
        /// <remarks>   Vladyslav, 24.05.2016. </remarks>
        ///
        /// <exception cref="ApplicationException"> Thrown when an response from Google has unsuccessfull status code. </exception>
        ///
        /// <param name="waypoints">    The waypoints. </param>
        ///
        /// <returns>   The distance matrix. </returns>

        public async Task<DistanseMatrixResponseModel> GetDistanceMatrix(IEnumerable<GooglePlaceModel> waypoints)
        {
            string requestWaypoints = waypoints.PrepareWaypointsRequestString();
            string key = ConfigurationManager.AppSettings["API_KEY"];
            string url = string.Format(DistanseMatrixUrl, requestWaypoints, requestWaypoints, key);
            
            using(var message = new HttpRequestMessage(HttpMethod.Get, url))
            using (var response = await _httpClient.SendAsync(message))
            {
                if (!response.IsSuccessStatusCode)
                    throw new ApplicationException("Google Distance Matrix Query Failed");

                string json = await response.Content.ReadAsStringAsync();
                DistanseMatrixResponseModel model = Deserialise(json);
                return model;
            }
        }

        /// <summary>   Deserialise json string to DistanseMatrixResponseModel. </summary>
        ///
        /// <remarks>   Vladyslav, 24.05.2016. </remarks>
        ///
        /// <param name="json"> The JSON string. </param>
        ///
        /// <returns>   A DistanseMatrixResponseModel. </returns>

        private DistanseMatrixResponseModel Deserialise(string json)
        {
            return JsonConvert.DeserializeObject<DistanseMatrixResponseModel>(json);
        }
    }

    /// <summary>   Interface for distance matrix query. </summary>
    ///
    /// <remarks>   Vladyslav, 24.05.2016. </remarks>

    public interface IDistanceMatrixQuery
    {
        /// <summary>   Gets distance matrix. </summary>
        ///
        /// <param name="waypoints">    The waypoints. </param>
        ///
        /// <returns>   The distance matrix. </returns>

        Task<DistanseMatrixResponseModel> GetDistanceMatrix(IEnumerable<GooglePlaceModel> waypoints);
    }
}
