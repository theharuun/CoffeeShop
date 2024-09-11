using AutoMapper;
using CoffeeShop.Common;
using CoffeeShop.DbOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopTests.TestsSetup
{
    public class CommonTestsFixture
    {
        public CoffeeShopDbContext Context { get; set; }
        public IMapper mapper { get; set; }

        public CommonTestsFixture()
        {
            var options = new DbContextOptionsBuilder<CoffeeShopDbContext>().UseInMemoryDatabase(databaseName: "CoffeeShopTestDB").Options;
            Context = new CoffeeShopDbContext(options);
            Context.Database.EnsureCreated();
            Context.AddGenreCoffee();
            Context.SaveChanges();
            Context.AddCoffees();
            Context.SaveChanges();
            Context.AddCustomers();
            Context.SaveChanges();
            Context.AddOrders();
            Context.SaveChanges();
            Context.AddPurchasedCoffees();
            Context.AddFavoriteGenres();
            Context.SaveChanges();

            mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();

        }
    }
}
