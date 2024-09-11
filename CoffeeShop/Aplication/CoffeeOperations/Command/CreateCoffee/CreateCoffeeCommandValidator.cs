using FluentValidation;

namespace CoffeeShop.Aplication.CoffeeOperations.Command.CreateCoffee
{
    public class CreateCoffeeCommandValidator : AbstractValidator<CreateCoffeeCommand>
    {
        public CreateCoffeeCommandValidator() 
        {
            RuleFor(s=>s.Model.coffeeName).NotEmpty().MinimumLength(3);
            RuleFor(s=>s.Model.genreId).NotEmpty().GreaterThan(0);
            RuleFor(s=>s.Model.price).NotEmpty().GreaterThan(0);
        }
    }
}
