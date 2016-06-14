// file:	validators\distansematrixresponsevalidator.cs
//
// summary:	Implements the distansematrixresponsevalidator class

using System.Linq;
using PathFinder.Trips.WebApi.Constants;
using PathFinder.Trips.WebApi.Models;

namespace PathFinder.Trips.WebApi.Validators
{
    /// <summary>   A distanse matrix response validator. </summary>
    ///
    /// <remarks>   Vladyslav, 24.05.2016. </remarks>

    internal static class DistanseMatrixResponseValidator
    {
        /// <summary>
        ///     A DistanseMatrixResponseModel extension method that validates the given model.
        /// </summary>
        ///
        /// <remarks>   Vladyslav, 24.05.2016. </remarks>
        ///
        /// <param name="model">    The model to act on. </param>
        ///
        /// <returns>   true if validation succeeds, false if it fails. </returns>

        internal static bool Validate(this DistanseMatrixResponseModel model)
        {
            if (model.Status != ValidationConstants.Ok)
                return false;

            if (model.Rows.Any(row => row.Elements.Any(el => el.Status != ValidationConstants.Ok)))
                return false;

            return true;
        }
    }
}
