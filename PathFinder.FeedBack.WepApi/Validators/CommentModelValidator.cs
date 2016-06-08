using FeedBack.WepApi.Models;
using FluentValidation;

namespace FeedBack.WepApi.Validators
{
    public class CommentModelValidator : AbstractValidator<FeedBackModel>
    {
        public CommentModelValidator()
        {
            RuleFor(x => x.Message).NotNull().NotEmpty().Length(5, 100);
            RuleFor(x => x.Author).NotNull().Length(3, 20);
            RuleFor(x => x.Email).NotNull().EmailAddress();
        }
    }
}
