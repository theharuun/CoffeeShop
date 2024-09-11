using FluentValidation;

namespace CoffeeShop.Aplication.OrderOperations.Command.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty().GreaterThan(0);
        }
    }
}
