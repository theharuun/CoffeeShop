using AutoMapper;
using CoffeeShop.Aplication.CoffeeOperations.Command.CreateCoffee;
using CoffeeShop.Aplication.CoffeeOperations.Queries.GetCoffeeByName;
using CoffeeShop.DbOperations;
using CoffeeShop.Entities;
using CoffeeShopTests.TestsSetup;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopTests.Aplication.CoffeeOperations.Queries.GetCoffeeByName
{
    public class GetCoffeeByNameTests : IClassFixture<CommonTestsFixture>
    {
        public CoffeeShopDbContext Context { get; set; }
        public IMapper mapper { get; set; }
        public GetCoffeeByNameTests(CommonTestsFixture testsFixture)
        {
            Context = testsFixture.Context;
            mapper = testsFixture.mapper;
        }

        [Fact] 
        public void WhenTryingToGetNonExistCoffee_InvalidOperationException_ShouldBeReturn()
        {
            GetCoffeeByNameQuery query = new GetCoffeeByNameQuery(Context, mapper);
            query.coffeeName = "hththrthrth";
            //act
            //assert
            FluentActions
               .Invoking(() => query.Handle())
               .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Coffee not found - Kahve bulunamadı");

        }

        [Fact]  // happy path
        public void WhenValidInputasAreGiven_Validator_ShouldBeCreated()
        { //arrange
            var coffee2 = new Coffee()
            {
                CoffeeID = 21,
                CoffeeName = "Latte2",
                GenreId = 1,
                Price = 100
            };


            Context.Coffees.Add(coffee2);
            Context.SaveChanges();



            //arrange
            GetCoffeeByNameQuery command = new GetCoffeeByNameQuery(Context, mapper);
            command.coffeeName = "Latte2";
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //assert
            var coffee = Context.Coffees
                .Include(X=>X.GenreCoffee)
                .Where(c => c.CoffeeName == command.coffeeName)
                .SingleOrDefault();
            coffee.Should().NotBeNull();
            coffee.CoffeeName.Should().Be(coffee2.CoffeeName);
            coffee.GenreId.Should().Be(coffee2.GenreId);
            coffee.Price.Should().Be(coffee2.Price);
        }
    }
}
