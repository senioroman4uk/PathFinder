using FluentValidation;
using PathFinder.Security.WebApi.Constants;
using PathFinder.Security.WebApi.Models;

namespace PathFinder.Security.WebApi.Validation
{
    public class RegisterUserModelValidator : AbstractValidator<RegisterUserModel>
    {
        public RegisterUserModelValidator()
        {
            RuleFor(model => model.Email).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty().EmailAddress();
            RuleFor(model => model.Password).NotNull();
            RuleFor(model => model.RepeatPassword).NotNull();
            RuleFor(model => model).Must(ArePasswordEquals).WithMessage(ValidationConstants.PasswordsAreNotEqualValidationMessage);
            RuleFor(model => model.LastName).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty();
            RuleFor(model => model.UserName).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty().Matches(@"^[A-Za-z0-9@_]+$");
            RuleFor(model => model.PhoneNumber).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty().Matches(@"\+38\([0-9]{3}\)\s[0-9]{3}-[0-9]{2}-[0-9]{2}");
        }

        private bool ArePasswordEquals(RegisterUserModel arg)
        {
            if (arg.Password == null) return false;
            return arg.Password.Equals(arg.RepeatPassword);
        }
    }
}
