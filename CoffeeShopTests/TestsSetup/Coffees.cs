using CoffeeShop.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeShop.Entities;

namespace CoffeeShopTests.TestsSetup
{
    public static class Coffees 
    {
        public static void AddCoffees( this CoffeeShopDbContext context )
        {
            context.Coffees.AddRange(
                        new Coffee()
                        {
                            CoffeeName = "Iced Espresso",
                            GenreId = 1,
                            Price = 100 + 10
                        },
                    new Coffee()
                    {
                        CoffeeName = "Iced Cappuccino",
                        GenreId = 1,
                        Price = 120 + 10
                    },
                    new Coffee()
                    {
                        CoffeeName = "Iced Latte",
                        GenreId = 2,
                        Price = 120 + 10
                    },
                    new Coffee()
                    {
                        CoffeeName = "Iced Americano",
                        GenreId = 2,
                        Price = 90 + 10
                    },
                    new Coffee()
                    {
                        CoffeeName = "Iced Macchiato",
                        GenreId = 3,
                        Price = 1200 + 10
                    },
                    new Coffee()
                    {
                        CoffeeName = "Iced Mocha",
                        GenreId = 3,
                        Price = 130 + 10
                    },
                    new Coffee()
                    {
                        CoffeeName = "Iced Affogato",
                        GenreId = 4,
                        Price = 100 + 10
                    },
                    new Coffee()
                    {
                        CoffeeName = "Iced Cortado",
                        GenreId = 4,
                        Price = 100 + 10
                    },
                    new Coffee()
                    {
                        CoffeeName = "Soğuk Türk Kahvesi",
                        GenreId = 5,
                        Price = 75 + 10
                    },
                    new Coffee()
                    {
                        CoffeeName = "Soğuk Filtre Kahve",
                        GenreId = 5,
                        Price = 80 + 10
                    },
                    new Coffee()
                    {
                        CoffeeName = "Espresso",
                        GenreId = 1,
                        Price = 100,
                        IsCold = false
                    },
                    new Coffee()
                    {
                        CoffeeName = "Cappuccino",
                        GenreId = 1,
                        Price = 120,
                        IsCold = false
                    },
                    new Coffee()
                    {
                        CoffeeName = "Latte",
                        GenreId = 2,
                        Price = 120,
                        IsCold = false
                    },
                    new Coffee()
                    {
                        CoffeeName = "Americano",
                        GenreId = 2,
                        Price = 90,
                        IsCold = false
                    },
                    new Coffee()
                    {
                        CoffeeName = "Macchiato",
                        GenreId = 3,
                        Price = 1200,
                        IsCold = false
                    },
                    new Coffee()
                    {
                        CoffeeName = "Mocha",
                        GenreId = 3,
                        Price = 130,
                        IsCold = false
                    },
                    new Coffee()
                    {
                        CoffeeName = "Affogato",
                        GenreId = 4,
                        Price = 100,
                        IsCold = false
                    },
                    new Coffee()
                    {
                        CoffeeName = "Cortado",
                        GenreId = 4,
                        Price = 100,
                        IsCold = false
                    },
                    new Coffee()
                    {
                        CoffeeName = "Türk Kahvesi",
                        GenreId = 5,
                        Price = 75,
                        IsCold = false
                    },
                    new Coffee()
                    {
                        CoffeeName = "Filtre Kahve",
                        GenreId = 5,
                        Price = 80,
                        IsCold = false
                    }
                );
        }
    }
}
