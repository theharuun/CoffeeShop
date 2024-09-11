using AutoMapper;
using CoffeeShop.Aplication.CoffeeOperations.Command.DeleteCoffee;
using CoffeeShop.DbOperations;
using CoffeeShop.Entities;
using CoffeeShopTests.TestsSetup;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopTests.Aplication.CoffeeOperations.Command.DeleteCoffee
{
    public class DeleteCoffeeCommandTests : IClassFixture<CommonTestsFixture>
    {
        public CoffeeShopDbContext Context { get; set; }
        public IMapper mapper { get; set; }

        public DeleteCoffeeCommandTests(CommonTestsFixture testsFixture)
        {
            Context = testsFixture.Context;
            mapper = testsFixture.mapper;
        }

        [Fact]
        public void WhenYouTryToEraseTheNonExistentCoffee_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            var coffee = new Coffee()
            {
               
                CoffeeName = "Latte123",
                GenreId = 1,
                Price = 100
            };

            DeleteCoffeeCommand command = new DeleteCoffeeCommand(Context);
            command.coffeeName=coffee.CoffeeName;

            FluentActions.Invoking(() => command.Handle())
              .Should().Throw<InvalidOperationException>().And.Message.Should().Be("This coffee is not in - Bu kahve yok ");
        }

        [Fact]  // happy path
        public void WhenValidInputasAreGiven_Validator_ShouldBeCreated()
        {
            //arrange
            var coffee = new Coffee()
            {
                CoffeeID = 100,
                CoffeeName = "Latte123",
                GenreId = 1,
                Price = 100
            };
            

            Context.Coffees.Add(coffee);
            Context.SaveChanges();

            DeleteCoffeeCommand command = new DeleteCoffeeCommand(Context);
            command.coffeeName = coffee.CoffeeName;

            //act &assert
            FluentActions.Invoking(() => command.Handle())
                .Should().NotThrow();

        }
    }
}
