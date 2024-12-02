using FluentValidation;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Validators
{
    public class BlogValidator : AbstractValidator<CreateBlogDTO>
    {
        public BlogValidator()
        {
            RuleFor(b => b.blogSlug)
                .NotEmpty().WithMessage("Slug Cannot Be Empty!")
                .Matches("^[^\u0600-\u06FF]+$").WithMessage("Slug cannot contain Persian characters.");
        }
    }
}
