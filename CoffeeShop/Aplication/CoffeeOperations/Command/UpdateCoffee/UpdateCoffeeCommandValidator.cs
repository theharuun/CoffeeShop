using FluentValidation;

namespace CoffeeShop.Aplication.CoffeeOperations.Command.UpdateCoffee
{
    public class UpdateCoffeeCommandValidator : AbstractValidator<UpdateCoffeeCommand>
    {
        public UpdateCoffeeCommandValidator() {
            RuleFor(s => s.coffeNameChange).MinimumLength(3).NotEmpty();
        }
    }
}
