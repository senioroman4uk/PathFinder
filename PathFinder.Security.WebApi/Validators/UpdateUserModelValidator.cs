using FluentValidation;
using PathFinder.Security.WebApi.Constants;
using PathFinder.Security.WebApi.Models;

namespace PathFinder.Security.WebApi.Validation
{
    class UpdateUserModelValidator : AbstractValidator<RegisterUserModel>
    {
        public UpdateUserModelValidator()
        {
            RuleFor(model => model.FirstName).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty();
            RuleFor(model => model.MiddleName).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty();
            RuleFor(model => model.LastName).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty();
            RuleFor(model => model.PhoneNumber).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty().Matches(@"\+38\([0-9]{3}\)\s[0-9]{3}-[0-9]{2}-[0-9]{2}");
            RuleFor(model => model.Email).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty().EmailAddress();
        }
    }
}

