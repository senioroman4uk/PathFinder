using FluentValidation;
using PathFinder.Security.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Security.WebApi.Validation
{
    public class RegisterUserModelValidator : AbstractValidator<RegisterUserModel>
    {
        public RegisterUserModelValidator()
        {
            RuleFor(model => model.Email).NotNull();
            RuleFor(model => model.Password).NotNull();
            RuleFor(model => model.RepeatPassword).NotNull();
            RuleFor(model => model).Must(ArePasswordEquals).WithMessage("Passwords are not equals");
            RuleFor(model => model.LastName).NotEmpty();
            RuleFor(model => model.UserName).NotNull().NotEmpty();
            RuleFor(model => model.PhoneNumber).NotEmpty();
        }

        private bool ArePasswordEquals(RegisterUserModel arg)
        {
            if (arg.Password == null) return false;
            return arg.Password.Equals(arg.RepeatPassword);
        }
    }
}
