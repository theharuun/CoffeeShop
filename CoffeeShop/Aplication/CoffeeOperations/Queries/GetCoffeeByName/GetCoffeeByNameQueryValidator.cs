using FluentValidation;

namespace CoffeeShop.Aplication.CoffeeOperations.Queries.GetCoffeeByName
{
    public class GetCoffeeByNameQueryValidator : AbstractValidator<GetCoffeeByNameQuery>
    {
        public GetCoffeeByNameQueryValidator() { 
        RuleFor(x=>x.coffeeName).NotEmpty().MinimumLength(4);
        }
    }
}
