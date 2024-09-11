using CoffeeShop.DbOperations;
using CoffeeShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopTests.TestsSetup
{
    public static class PurchasedCoffees 
    {
     public static void AddPurchasedCoffees(this CoffeeShopDbContext context)
        {
            context.purchasedCoffees.AddRange(
                   new PurchasedCoffee { CustomerID = 1, CoffeeID = 1 },
                   new PurchasedCoffee { CustomerID = 1, CoffeeID = 2 },
                   new PurchasedCoffee { CustomerID = 1, CoffeeID = 3 },

                   new PurchasedCoffee { CustomerID = 2, CoffeeID = 4 },
                   new PurchasedCoffee { CustomerID = 2, CoffeeID = 5 },
                   new PurchasedCoffee { CustomerID = 2, CoffeeID = 6 }

                   );
        }
    }
}
