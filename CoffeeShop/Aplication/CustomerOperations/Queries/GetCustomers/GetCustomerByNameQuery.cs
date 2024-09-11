using AutoMapper;
using CoffeeShop.Aplication.CoffeeOperations.Queries.GetCoffeeByName;
using CoffeeShop.DbOperations;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Aplication.CustomerOperations.Queries.GetCustomers
{
    public class GetCustomerByNameQuery
    {
        private readonly ICoffeeShopDbContext context;
        private readonly IMapper mapper;

        public string customerEmail { get; set; }
        public GetCustomerByNameQuery(ICoffeeShopDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public GetCustomerByNameModel Handle()
        {
            var customer = context.Customers
                     .Include(x => x.purchasedCoffees)
                     .ThenInclude(pm => pm.Coffee)
                     .Include(a => a.favoriteGenres)
                     .ThenInclude(pm => pm.GenreCoffee)
                     .Where(c => c.Email == customerEmail)
                     .SingleOrDefault();

            if (customer == null)
                throw new InvalidOperationException("Customer not found - Müşteri bulunamadı");


            GetCustomerByNameModel model = mapper.Map<GetCustomerByNameModel>(customer);

            return model;
        }
    }

    public class GetCustomerByNameModel
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
