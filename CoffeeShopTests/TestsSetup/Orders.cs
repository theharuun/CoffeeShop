using CoffeeShop.DbOperations;
using CoffeeShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopTests.TestsSetup
{
    public static class Orders
    {
        public static void AddOrders(this CoffeeShopDbContext context)
        {
            var coffeeIds = context.Coffees.ToDictionary(m => m.CoffeeName, m => m.CoffeeID);
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
        }
    } 
}
