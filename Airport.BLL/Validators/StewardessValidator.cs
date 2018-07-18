using FluentValidation;
using Airport.DAL.Entities;

namespace Airport.BLL.Validators
{
    public class StewardessValidator : AbstractValidator<Stewardess>
    {
        public StewardessValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(x => x.SecondName)
                .NotEmpty()
                .MinimumLength(3);
            
            RuleFor(x => x.BirthDate)
                .NotEmpty();
        }
    }
}
