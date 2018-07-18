using FluentValidation;
using Airport.DAL.Entities;

namespace Airport.BLL.Validators
{
    public class PilotValidator : AbstractValidator<Pilot>
    {
        public PilotValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(x => x.Experience)
                .NotEmpty()
                .GreaterThan(1);

            RuleFor(x => x.BirthDate)
                .NotEmpty();
        }
    }
}
