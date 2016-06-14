using FluentValidation;
using PathFinder.Security.UserManagement.Constants;
using PathFinder.Security.UserManagement.Models;

namespace PathFinder.Security.UserManagement.Validators
{
    public class RegisterUserModelValidator : AbstractValidator<RegisterUserModel>
    {
        public RegisterUserModelValidator()
        {
            RuleFor(model => model.Email).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty().EmailAddress();
            RuleFor(model => model.Password).Cascade(CascadeMode.StopOnFirstFailure).NotNull().Length(6, 16);
            RuleFor(model => model.RepeatPassword).Cascade(CascadeMode.StopOnFirstFailure).NotNull().Length(6, 16);
            RuleFor(model => model).Must(ArePasswordEquals).WithMessage(ValidationConstants.PasswordsAreNotEqualValidationMessage);
            RuleFor(model => model.LastName).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty();
            RuleFor(model => model.UserName).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty().Matches(ValidationConstants.UserNameRegex);
            RuleFor(model => model.PhoneNumber).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty().Matches(ValidationConstants.PhoneNumberRegex);
        }

        private bool ArePasswordEquals(RegisterUserModel arg)
        {
            if (arg.Password == null) return false;
            return arg.Password.Equals(arg.RepeatPassword);
        }
    }
}
