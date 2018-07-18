using FluentValidation;
using Airport.DAL.Entities;

namespace Airport.BLL.Validators
{
    public class TicketValidator : AbstractValidator<Ticket>
    {
        public TicketValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Flight)
                .NotEmpty();

            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThan(1);
        }
    }
}
