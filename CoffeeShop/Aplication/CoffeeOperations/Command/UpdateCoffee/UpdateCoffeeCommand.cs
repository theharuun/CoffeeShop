using AutoMapper;
using CoffeeShop.DbOperations;

namespace CoffeeShop.Aplication.CoffeeOperations.Command.UpdateCoffee
{
    public class UpdateCoffeeCommand
    {

        private readonly ICoffeeShopDbContext context;
        private readonly IMapper mapper;
        public string coffeNameChange;
        public UpdateCoffeeModel updateCoffeeModel { get; set; }
        public UpdateCoffeeCommand(ICoffeeShopDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void Handle()
        { 
            
            var coffee = context.Coffees.SingleOrDefault(c => c.CoffeeName == updateCoffeeModel.coffeeName);
            if (coffee!=null)
                throw new InvalidOperationException("There is already a Coffee with this name  - Bu isimde kahve zaten var");

            coffee = context.Coffees.SingleOrDefault(c => c.CoffeeName == coffeNameChange);
            if (coffee == null)
                throw new InvalidOperationException("This coffee is not in - Bu Kahve yok");

           


            coffee.CoffeeName = !string.IsNullOrEmpty(updateCoffeeModel.coffeeName) ? updateCoffeeModel.coffeeName : coffee.CoffeeName;
            coffee.Price = updateCoffeeModel.price != default ? updateCoffeeModel.price : coffee.Price;
            // isCold'ü null kontrolü ile güncelle
            if (updateCoffeeModel.isCold.HasValue)
            {
                coffee.IsCold = updateCoffeeModel.isCold.Value;
            }
            coffee.GenreId = updateCoffeeModel.genreId != default ? updateCoffeeModel.genreId : coffee.GenreId;

            context.SaveChanges();
        }
    }
    public class UpdateCoffeeModel
    {
        public string? coffeeName { get; set; }

        public int genreId { get; set; }

        public int price { get; set; }

        // soğuk/sıcak durumu 
        public bool? isCold { get; set; }
    }
}
