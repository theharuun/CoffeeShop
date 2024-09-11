using CoffeeShop.DbOperations;

namespace CoffeeShop.Aplication.CoffeeOperations.Command.DeleteCoffee
{
    public class DeleteCoffeeCommand
    {
        public string coffeeName { get; set; }
        private readonly ICoffeeShopDbContext context;

        public DeleteCoffeeCommand(ICoffeeShopDbContext context)
        {
            this.context = context;
        }

        public void Handle()
        {
            var coffee=context.Coffees.SingleOrDefault(x=>x.CoffeeName==coffeeName);
            if (coffee == null)
                throw new InvalidOperationException("This coffee is not in - Bu kahve yok ");

            context.Coffees.Remove(coffee);
            context.SaveChanges();

        }
    }
}
