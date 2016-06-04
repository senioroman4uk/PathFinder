using FluentValidation;

namespace PathFinder.Infrastructure.Validators
{
    public class StringValidator : AbstractValidator<string>
    {
        public StringValidator()
        {
            RuleFor(x => x).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty();
        }
    }
}
