using FluentValidation;
using Airport.DAL.Entities;

namespace Airport.BLL.Validators
{
    public class AeroplaneValidator : AbstractValidator<Aeroplane>
    {
        public AeroplaneValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(x => x.AeroplaneType)
                .NotNull();
        }
    }
}
