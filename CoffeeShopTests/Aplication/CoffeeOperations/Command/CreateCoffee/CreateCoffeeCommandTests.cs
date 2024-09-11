using AutoMapper;
using CoffeeShop.Aplication.CoffeeOperations.Command.CreateCoffee;
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

namespace CoffeeShopTests.Aplication.CoffeeOperations.Command.CreateCoffee
{
    public class CreateCoffeeCommandTests : IClassFixture<CommonTestsFixture>
    {  
        public CoffeeShopDbContext Context { get; set; }
        public IMapper mapper { get; set; }
        public CreateCoffeeCommandTests(CommonTestsFixture testsFixture)
        {
            Context = testsFixture.Context;
            mapper = testsFixture.mapper;
        }

        [Fact]
        public void WhenThereIsCoffeeAndAgainWhileCreateItSameName_InvalidOperationException_ShoulBeReturn()
        {
            //arrange
            var coffee = new Coffee()
            { 
                 CoffeeID =21, CoffeeName="Latte" , GenreId=1, Price=100 
            };
            

            Context.Coffees.Add(coffee);
            Context.SaveChanges();


            CreateCoffeeCommand command = new CreateCoffeeCommand(Context, mapper);
            command.Model= new CreateCoffeeModel() { coffeeName= coffee.CoffeeName , genreId=1 , price = coffee.Price };

            //act
            //assert
            FluentActions
               .Invoking(() => command.Handle())
               .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Sequence contains more than one element");

        }

        [Fact]  // happy path
        public void WhenValidInputasAreGiven_Validator_ShouldBeCreated()
        {
            //arrange
            CreateCoffeeCommand command = new CreateCoffeeCommand(Context, mapper);
            CreateCoffeeModel model = new CreateCoffeeModel() { coffeeName = "Lattfefewe", genreId = 1, price = 100 };
            command.Model = model;
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //assert
            var coffee = Context.Coffees.SingleOrDefault(x => x.CoffeeName == command.Model.coffeeName);
            coffee.Should().NotBeNull();
            coffee.CoffeeName.Should().Be(model.coffeeName);
            coffee.GenreId.Should().Be(model.genreId);
            coffee.Price.Should().Be(model.price);
        }

    }
}
