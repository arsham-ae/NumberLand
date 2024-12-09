using FluentValidation;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Validators
{
    public class NumberValidator : AbstractValidator<CreateNumberDTO>
    {
        public NumberValidator()
        {
            RuleFor(n => n.number)
                .NotEmpty().WithMessage("Number Cannot Be Empty!")
                .MaximumLength(15).WithMessage("Max Length Should be 15")
                .Matches(@"^\+?[0-9]+$").WithMessage("Phone number must contain only numbers and the '+' sign.");
            RuleFor(n => n.expireTime)
                .NotEmpty().WithMessage("Expire Time Cannot be Empty!")
                .GreaterThan(DateTime.Now.AddMinutes(30)).WithMessage("Expire Time Cannot Be Les than 30Min!");
            RuleFor(n => n.operatorId)
                .NotEmpty().WithMessage("OperatorId Cannot Be Empty!")
                .GreaterThanOrEqualTo(1).WithMessage("OperatorId Should Be 1 Or Above!");
            RuleFor(n => n.categoryId)
                .NotEmpty().WithMessage("CategoryId Cannot Be Empty!")
                .GreaterThanOrEqualTo(1).WithMessage("CategoryId Should Be 1 Or Above!");
        }
    }
}
