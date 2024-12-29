using FluentValidation;
using NumberLand.Models;

namespace NumberLand.Validators
{
    public class RegisterValidation : AbstractValidator<UserModel>
    {
        public RegisterValidation()
        {
            RuleFor(n => n.phoneNumber).NotEmpty().WithMessage("Number Cannot Be Empty!")
                .Length(11).WithMessage("Length Should be 11");
            RuleFor(ps => ps.password).NotEmpty().WithMessage("Password Cannot Be Empty!")
                .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$").WithMessage("Password is Weak");
        }
    }
}
