using AutoMapper;
using CoffeeShop.DbOperations;
using CoffeeShop.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CoffeeShop.Aplication.CoffeeOperations.Command.CreateCoffee
{
    public class CreateCoffeeCommand
    {

        private readonly ICoffeeShopDbContext context;
        private readonly IMapper mapper;
        public CreateCoffeeModel Model { get; set; }
        public CreateCoffeeCommand(ICoffeeShopDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void Handle()
        {
            var coffee = context.Coffees.SingleOrDefault(x => x.CoffeeName == Model.coffeeName);
            if (coffee != null)
                throw new InvalidOperationException("There is already Coffee - Kahve zaten var");

            coffee = mapper.Map<Coffee>(Model);
            context.Coffees.Add(coffee);
            context.SaveChanges();
            
        }
    }

    public class CreateCoffeeModel
    {
        public string? coffeeName { get; set; }

        public int genreId { get; set; }

        public int price { get; set; }

        // soğuk/sıcak durumu 
        public bool isCold { get; set; } = true;
    }
}
