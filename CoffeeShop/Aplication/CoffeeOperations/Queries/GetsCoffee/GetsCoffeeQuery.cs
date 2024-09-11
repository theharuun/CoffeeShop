using AutoMapper;
using CoffeeShop.DbOperations;
using CoffeeShop.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Aplication.CoffeeOperations.Queries.GetsCoffee
{
    public class GetsCoffeeQuery
    {
        private readonly ICoffeeShopDbContext context;
        private readonly IMapper mapper;

        public GetsCoffeeQuery(ICoffeeShopDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<GetCoffeeModel> Handle()
        {
            var coffeeList = context.Coffees.Include(c => c.GenreCoffee).OrderBy(c => c.CoffeeID).ToList();

            List<GetCoffeeModel> list = mapper.Map<List<GetCoffeeModel>>(coffeeList);

            return list;
        }
    }
    public class GetCoffeeModel
    {
        public string? coffeeName { get; set; }

        public string genre { get; set; }

        public int price { get; set; }

        // soğuk/sıcak durumu 
        public bool isCold { get; set; } = true;
    }
}
