using CoffeeShop.DbOperations;
using CoffeeShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopTests.TestsSetup
{
    public static class GenreCoffee 
    {
        public static void AddGenreCoffee(this CoffeeShopDbContext context) 
        {
            context.GenreCoffees.AddRange(
                new CoffeeShop.Entities.GenreCoffee()
                {
                    Name = "Etiyopya"
                },
                 new CoffeeShop.Entities.GenreCoffee()
                 {
                     Name = "Brezilya"
                 },
                  new CoffeeShop.Entities.GenreCoffee()
                  {
                      Name = "Kolombiya"
                  },
                   new CoffeeShop.Entities.GenreCoffee()
                   {
                       Name = "Kostarika"
                   },
                    new CoffeeShop.Entities.GenreCoffee()
                    {
                        Name = "Jamaika"
                    }
                );
        }
    }
}
