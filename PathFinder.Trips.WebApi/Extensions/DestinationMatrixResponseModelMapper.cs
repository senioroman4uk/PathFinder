// file:	Extensions\DestinationMatrixResponseModelMapper.cs
//
// summary:	Implements the destination matrix response model mapper class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder.Trips.WebApi.Models;

namespace PathFinder.Trips.WebApi.Extensions
{
    /// <summary>   A destination matrix response model mapper. </summary>
    ///
    /// <remarks>   Vladyslav, 25.05.2016. </remarks>

    internal static class DestinationMatrixResponseModelMapper
    {
        /// <summary>
        ///     A DistanseMatrixResponseModel extension method that convert this object into an array
        ///     representation.
        /// </summary>
        ///
        /// <remarks>   Vladyslav, 25.05.2016. </remarks>
        ///
        /// <param name="model">    The model to act on. </param>
        ///
        /// <returns>   An array that represents the data in this object. </returns>

        public static double[,] ToArray(this DistanseMatrixResponseModel model)
        {
            var matrix = new double[model.Rows.Count,model.Rows.Count];
            for (int i = 0; i < model.Rows.Count; i++)
            {
                var row = model.Rows[i];
                for (int j = 0; j < row.Elements.Count; j++)
                {
                    var element = row.Elements[j];
                    matrix[i, j] = element.Distance.Value;
                }
            }

            return matrix; 
        }
    }
}
