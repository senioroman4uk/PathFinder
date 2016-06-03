using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Security.WebApi.Validation
{
    public class IntValidator : AbstractValidator<int>
    {
        public IntValidator()
        {
            RuleFor(x => x).NotEmpty().GreaterThan(0);
        }
    }
}
