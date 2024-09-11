using AutoMapper;
using CoffeeShop.DbOperations;
using CoffeeShop.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Aplication.CustomerOperations.Queries.GetCustomerByName
{
    public class GetCustomersQuery
    {
        private readonly ICoffeeShopDbContext context;
        private readonly IMapper mapper;

        public GetCustomersQuery(ICoffeeShopDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<GetCustomersModel> Handle()
        {
            var customers= context.Customers
                .Include(x=>x.purchasedCoffees)
                .ThenInclude(pm=>pm.Coffee)
                .Include(a=>a.favoriteGenres)
                .ThenInclude(pm=>pm.GenreCoffee)
                .OrderBy(pm=>pm.CustomerID)
                .ToList();

            List<GetCustomersModel> list = mapper.Map<List<GetCustomersModel>>(customers);

            return list;
        }
    }

    public class GetCustomersModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        // Satın aldığı filmler
        public List<string> purchasedCoffees { get; set; } = new List<string>();

        // Favori türler
        public List<string> favoriteGenres { get; set; } = new List<string>();
    }
}
