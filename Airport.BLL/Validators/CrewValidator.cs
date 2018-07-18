using FluentValidation;
using Airport.DAL.Entities;

namespace Airport.BLL.Validators
{
    public class CrewValidator : AbstractValidator<Crew>
    {
        public CrewValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Pilot)
                .NotNull();

            RuleFor(x => x.Stewardesses)
                .NotEmpty();
        }
    }
}
