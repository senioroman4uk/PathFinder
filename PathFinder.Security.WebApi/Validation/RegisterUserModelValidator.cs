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
            RuleFor(model => model.Email).NotEmpty();
            RuleFor(model => model).Must(ArePasswordEquels);
            RuleFor(model => model.LastName).NotEmpty();
            RuleFor(model => model.UserName).NotEmpty();
            RuleFor(model => model.PhoneNumber).NotEmpty();
        }

        private bool ArePasswordEquels(RegisterUserModel arg)
        {
            return arg.Password.Equals(arg.RepeatPassword);
        }
    }
}
