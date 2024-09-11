using AutoMapper;
using CoffeeShop.Aplication.CoffeeOperations.Command.CreateCoffee;
using CoffeeShop.DbOperations;
using CoffeeShop.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Aplication.CustomerOperations.Command.CreateCustomer
{
    public class CreateCustomerCommand
    {
        private readonly ICoffeeShopDbContext context;
        private readonly IMapper mapper;

        public CreateCustomerCommand(ICoffeeShopDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public CreateCustomerModel Model { get; set; }
        public void Handle() 
        {
            var customer = context.Customers.SingleOrDefault(x => x.Email == Model.email);
            if (customer != null)
                throw new InvalidOperationException("Bu emaile sahip musteri var- there is a customer with same email");

            customer = mapper.Map<Customer>(Model);

            // Satin aldigi filmleri ekle
            if (Model.purchasedCoffeeIDs != null && Model.purchasedCoffeeIDs.Any())
            {
                customer.purchasedCoffees = Model.purchasedCoffeeIDs.Select(purchasedCoffeId => new PurchasedCoffee
                {
                    CoffeeID = purchasedCoffeId
                }).ToList();
            }
            // favori türleri ekle 
            if (Model.favoriteGenresIDs != null && Model.favoriteGenresIDs.Any())
            {
                customer.favoriteGenres = Model.favoriteGenresIDs.Select(favGenreId => new FavoriteGenre
                {
                    GenreID = favGenreId
                }).ToList();
            }
            // RefreshToken'ı burada ayarlayın
          //  customer.RefreshToken = "initial_refresh_token";  // Geçici bir değer atayabilirsiniz
            context.Customers.Add(customer);
            context.SaveChanges();

        }
    }

    public class CreateCustomerModel
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }

        // Satın aldığı filmler
        public List<int> purchasedCoffeeIDs { get; set; } // Movie ID'lerini tutacak liste

        // Favori türler
        public List<int> favoriteGenresIDs { get; set; } // favori genre ID'lerini tutacak liste
        public string password { get; set; }
    }
}

