using FluentValidation;

namespace CoffeeShop.Aplication.OrderOperations.Queries.GetOrderByID
{
    public class GetOrderByIDQueryValidator : AbstractValidator<GetOrderByIDQuery>
    {
        public GetOrderByIDQueryValidator() {
        
        RuleFor(x=>x.OrderID).NotEmpty().GreaterThan(0);
        }
    }
}
