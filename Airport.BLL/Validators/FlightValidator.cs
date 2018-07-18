using FluentValidation;
using Airport.DAL.Entities;

namespace Airport.BLL.Validators
{
    public class FlightValidator : AbstractValidator<Flight>
    {
        public FlightValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(5);

            RuleFor(x => x.Destinition)
                .NotEmpty();

            RuleFor(x => x.DeparturePoint)
                .NotEmpty();

            RuleFor(x => x.ArrivalTime)
                .NotEmpty();

            RuleFor(x => x.DepartureTime)
                .NotEmpty();
        }
    }
}
