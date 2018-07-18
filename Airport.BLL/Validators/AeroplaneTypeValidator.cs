using FluentValidation;
using Airport.DAL.Entities;

namespace Airport.BLL.Validators
{
    public class AeroplaneTypeValidator : AbstractValidator<AeroplaneType>
    {
        public AeroplaneTypeValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Model)
                .NotNull()
                .MinimumLength(3);

            RuleFor(x => x.Places)
                .NotEmpty()
                .GreaterThan(1);

            RuleFor(x => x.Carrying)
                .NotEmpty()
                .GreaterThan(10000);
        }
    }
}
