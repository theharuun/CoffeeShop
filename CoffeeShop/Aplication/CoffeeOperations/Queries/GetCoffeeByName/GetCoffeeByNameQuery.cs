using AutoMapper;
using CoffeeShop.DbOperations;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Aplication.CoffeeOperations.Queries.GetCoffeeByName
{
    public class GetCoffeeByNameQuery
    {
        private readonly ICoffeeShopDbContext context;
        private readonly IMapper mapper;

        public string coffeeName { get; set; }

        public GetCoffeeByNameQuery(ICoffeeShopDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public GetCoffeeByNameModel Handle()
        {
            var coffee = context.Coffees
                .Include(c => c.GenreCoffee)
                .Where(c => c.CoffeeName == coffeeName)
                .SingleOrDefault();
            if (coffee == null)
                throw new InvalidOperationException("Coffee not found - Kahve bulunamadı");


            GetCoffeeByNameModel model = mapper.Map<GetCoffeeByNameModel>(coffee);

            return model;
        }
    }

    public class GetCoffeeByNameModel
    {
        public string? coffeeName { get; set; }

        public string genre { get; set; }

        public int price { get; set; }

        // soğuk/sıcak durumu 
        public bool isCold { get; set; } = true;
    }
}
