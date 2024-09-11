using CoffeeShop.DbOperations;
using CoffeeShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopTests.TestsSetup
{
    public static class Customers 
    {
        public static void AddCustomers(this CoffeeShopDbContext context)
        {
            context.Customers.AddRange(
                 
                    new Customer() { Name = "John", Surname = "Doe", Email = "johndoe@coffeeShop.com", Password = "john1234" },
                    new Customer() { Name = "Jane", Surname = "Smith", Email = "janeswitch@coffeeShop.com", Password = "jane1234", }
     
                );
        }
    }
}
