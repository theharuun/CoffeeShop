using FluentValidation;

namespace CoffeeShop.Aplication.CustomerOperations.Command.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator() 
        {
            RuleFor(x=>x.Model.email).NotEmpty().MinimumLength(3);
        }
    }
}
