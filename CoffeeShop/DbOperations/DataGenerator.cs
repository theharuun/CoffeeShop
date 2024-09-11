using Microsoft.EntityFrameworkCore;
using CoffeeShop.Entities;

namespace CoffeeShop.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CoffeeShopDbContext(serviceProvider.GetRequiredService<DbContextOptions<CoffeeShopDbContext>>())) 
            {
                if (context.Coffees.Any())
                {

                    return;
                }
                context.GenreCoffees.AddRange(
                    new GenreCoffee()
                    {
                       Name= "Etiyopya"
                    },
                    new GenreCoffee()
                    {
                        Name= "Brezilya"
                    },
                     new GenreCoffee()
                     {
                         Name = "Kolombiya"
                     },
                      new GenreCoffee()
                      {
                          Name = "Kostarika"
                      },
                       new GenreCoffee()
                       {
                           Name = "Jamaika"
                       }
                    );

                context.SaveChanges();

                context.Coffees.AddRange(
                    
                    new Coffee()
                    {
                      CoffeeName= "Iced Espresso" , GenreId = 1 , Price=100+10
                    } ,
                    new Coffee()
                    {
                      CoffeeName= "Iced Cappuccino", GenreId = 1 , Price=120+10
                    },
                    new Coffee()
                    {
                      CoffeeName= "Iced Latte", GenreId = 2 , Price=120 + 10
                    },
                    new Coffee()
                    {
                      CoffeeName= "Iced Americano", GenreId = 2 , Price=90 +10
                    },
                    new Coffee()
                    {
                      CoffeeName= "Iced Macchiato", GenreId = 3 , Price=1200 +10
                    },
                    new Coffee()
                    {
                      CoffeeName= "Iced Mocha", GenreId = 3, Price=130 +10
                    },
                    new Coffee()
                    {
                      CoffeeName= "Iced Affogato", GenreId = 4 , Price=100 +10
                    },   
                    new Coffee()
                    {
                      CoffeeName= "Iced Cortado", GenreId = 4 , Price=100 +10
                    },
                    new Coffee()
                    {
                      CoffeeName= "Soğuk Türk Kahvesi" , GenreId = 5 , Price=75 +10
                    },
                    new Coffee()
                    {
                      CoffeeName= "Soğuk Filtre Kahve" , GenreId = 5 , Price=80  +10
                    },
                    new Coffee()
                    {
                      CoffeeName= "Espresso" , GenreId = 1 , Price=100 , IsCold=false
                    } ,
                    new Coffee()
                    {
                      CoffeeName= "Cappuccino", GenreId = 1 , Price=120  , IsCold= false
                    },
                    new Coffee()
                    {
                      CoffeeName= "Latte", GenreId = 2 , Price=120 , IsCold= false
                    },
                    new Coffee()
                    {
                      CoffeeName= "Americano", GenreId = 2 , Price=90 , IsCold= false
                    },
                    new Coffee()
                    {
                      CoffeeName= "Macchiato", GenreId = 3 , Price=1200 , IsCold= false
                    },
                    new Coffee()
                    {
                      CoffeeName= "Mocha", GenreId = 3, Price=130 , IsCold= false
                    },
                    new Coffee()
                    {
                      CoffeeName= "Affogato", GenreId = 4 , Price=100 , IsCold= false
                    },   
                    new Coffee()
                    {
                      CoffeeName= "Cortado", GenreId = 4 , Price=100 , IsCold= false
                    },
                    new Coffee()
                    {
                      CoffeeName= "Türk Kahvesi" , GenreId = 5 , Price=75 , IsCold=false
                    },
                    new Coffee()
                    {
                      CoffeeName= "Filtre Kahve" , GenreId = 5 , Price=80  , IsCold=false
                    }
                    );

                context.SaveChanges();

                // Coffee ve Genre ID'lerini almak için sorgulama yapılır - Query is made to get Coffee and Genre IDs
                var coffeeIds = context.Coffees.ToDictionary(m => m.CoffeeName, m => m.CoffeeID);
                var genreIds = context.GenreCoffees.ToDictionary(g => g.Name, g => g.Id);
                // Customers
                context.Customers.AddRange(
                    new Customer() { Name = "John", Surname = "Doe", Email = "johndoe@coffeeShop.com", Password = "john1234" },
                    new Customer() { Name = "Jane", Surname = "Smith", Email = "janeswitch@coffeeShop.com", Password = "jane1234", }
                );
                context.SaveChanges();

                // Orders
                context.Orders.AddRange(
                    new Order()
                    {
                        OrderDate = new DateTime(2010, 5, 6),
                        CustomerID = 1,
                        Coffees = new List<Coffee>
                        {
                            context.Coffees.Find(coffeeIds["Iced Espresso"]) ,
                            context.Coffees.Find(coffeeIds["Iced Cappuccino"]),
                            context.Coffees.Find(coffeeIds["Iced Latte"])

                        }
           
                    },
                      new Order()
                      {
                          OrderDate = new DateTime(2012, 5, 6),
                          CustomerID = 2,
                          Coffees = new List<Coffee>
                        {
                            context.Coffees.Find(coffeeIds["Iced Americano"]) ,
                            context.Coffees.Find(coffeeIds["Iced Macchiato"]),
                            context.Coffees.Find(coffeeIds["Iced Mocha"])

                        }
                      }

                );
                context.SaveChanges();

                context.purchasedCoffees.AddRange(
                    new PurchasedCoffee { CustomerID = 1  , CoffeeID= 1  },
                    new PurchasedCoffee { CustomerID = 1,  CoffeeID = 2 },
                    new PurchasedCoffee { CustomerID = 1, CoffeeID = 3 },
                  
                    new PurchasedCoffee { CustomerID = 2, CoffeeID = 4 }, 
                    new PurchasedCoffee { CustomerID = 2, CoffeeID = 5 },
                    new PurchasedCoffee { CustomerID = 2, CoffeeID = 6 }

                    );

                context.favoriteGenres.AddRange(
                    new FavoriteGenre { CustomerID = 1, GenreID = genreIds["Etiyopya"] },
                    new FavoriteGenre { CustomerID = 1, GenreID = genreIds["Kolombiya"] },
                    new FavoriteGenre { CustomerID = 1, GenreID = 4 },


                    new FavoriteGenre { CustomerID = 2, GenreID = genreIds["Brezilya"] },
                    new FavoriteGenre { CustomerID = 2, GenreID = genreIds["Jamaika"] }


                );

                context.SaveChanges();
              

                context.SaveChanges( );
            }
        }
    }
}
