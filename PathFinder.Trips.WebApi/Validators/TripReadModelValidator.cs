using FluentValidation;
using PathFinder.Trips.WebApi.Models;
using System;

namespace PathFinder.Trips.WebApi.Validators
{
    public class TripReadModelValidator:AbstractValidator<TripReadModel>
    {
        public TripReadModelValidator()
        {
            RuleFor(model => model.StartPoint).NotNull();
            RuleFor(model => model.EndPoint).NotNull();
            RuleFor(model => model.Algorithm).NotNull();
            RuleFor(model => model.DepartureDate).NotNull().GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(model => model.Price).NotEmpty();
        }
    }
}
