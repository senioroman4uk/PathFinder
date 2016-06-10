using FluentValidation;
using PathFinder.Security.UserManagement.Constants;
using PathFinder.Security.UserManagement.Models;

namespace PathFinder.Security.UserManagement.Validators
{
    class UpdateUserModelValidator : AbstractValidator<UpdateUserModel>
    {
        public UpdateUserModelValidator()
        {
            RuleFor(model => model.FirstName).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty();
            RuleFor(model => model.MiddleName).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty();
            RuleFor(model => model.LastName).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty();
            RuleFor(model => model.PhoneNumber).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty().Matches(ValidationConstants.PhoneNumberRegex);
            RuleFor(model => model.Email).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty().EmailAddress();
            RuleFor(model => model.Id).GreaterThan(0);
        }
    }
}

