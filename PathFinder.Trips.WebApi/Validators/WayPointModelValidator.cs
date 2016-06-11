using FluentValidation;
using PathFinder.Trips.WebApi.Models;

namespace PathFinder.Trips.WebApi.Validators
{
    public class WayPointModelValidator : AbstractValidator<WayPointModel>
    {
        public WayPointModelValidator()
        {
            RuleFor(model => model.Place).NotNull();
        }
    }
}
