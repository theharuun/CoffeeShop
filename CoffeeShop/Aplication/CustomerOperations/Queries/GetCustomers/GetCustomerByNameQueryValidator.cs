using FluentValidation;

namespace CoffeeShop.Aplication.CustomerOperations.Queries.GetCustomers
{
    public class GetCustomerByNameQueryValidator :AbstractValidator<GetCustomerByNameQuery>
    {
        public GetCustomerByNameQueryValidator()
        {
            RuleFor(x=>x.customerEmail).NotEmpty().MinimumLength(3);
        }
    }
}
