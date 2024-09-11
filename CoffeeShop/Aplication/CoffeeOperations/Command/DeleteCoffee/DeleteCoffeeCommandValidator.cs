using FluentValidation;

namespace CoffeeShop.Aplication.CoffeeOperations.Command.DeleteCoffee
{
    public class DeleteCoffeeCommandValidator : AbstractValidator<DeleteCoffeeCommand>
    {
        public DeleteCoffeeCommandValidator() {
        RuleFor(s=>s.coffeeName).NotEmpty().MinimumLength(3); 
        }
    }
}
