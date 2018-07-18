using FluentValidation;
using Airport.DAL.Entities;

namespace Airport.BLL.Validators
{
    public class DepartureValidator : AbstractValidator<Departure>
    {
        public DepartureValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Airplane)
                .NotNull();

            RuleFor(x => x.Time)
                .NotEmpty();

            RuleFor(x => x.Crew)
                .NotNull();
        }
    }
}
