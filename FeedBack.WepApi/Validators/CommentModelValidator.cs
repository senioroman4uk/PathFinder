using FeedBack.WepApi.Models;
using FluentValidation;

namespace FeedBack.WepApi.Validation
{
    public class CommentModelValidator : AbstractValidator<CommentModel>
    {
        public CommentModelValidator()
        {
            RuleFor(x => x.Message).NotNull().NotEmpty();
            RuleFor(x => x.Author).NotNull().Length(3, 20);
            RuleFor(x => x.Email).NotNull().EmailAddress();
        }
    }
}
