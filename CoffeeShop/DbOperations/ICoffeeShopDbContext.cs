using CoffeeShop.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DbOperations
{
    public interface ICoffeeShopDbContext
    {
      
        DbSet<GenreCoffee> GenreCoffees { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Coffee> Coffees { get; set; }

        DbSet<PurchasedCoffee> purchasedCoffees { get; set; }

        int SaveChanges();

    }
}
