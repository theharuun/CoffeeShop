using AutoMapper;
using CoffeeShop.Aplication.CoffeeOperations.Command.CreateCoffee;
using CoffeeShop.Aplication.CoffeeOperations.Command.UpdateCoffee;
using CoffeeShop.DbOperations;
using CoffeeShop.Entities;
using CoffeeShopTests.TestsSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopTests.Aplication.CoffeeOperations.Command.UpdateCoffee
{
    public class UpdateCoffeeCommandTests : IClassFixture<CommonTestsFixture>
    {
        public CoffeeShopDbContext Context { get; set; }
        public IMapper mapper { get; set; }
        public UpdateCoffeeCommandTests(CommonTestsFixture testsFixture)
        {
            Context = testsFixture.Context;
            mapper = testsFixture.mapper;
        }

        [Fact]
        public void WhenThereIsCoffeeInTheNameItIsTryingToUpdate_InvalidOperationException_ShouldBeReturn()
        {
        

            UpdateCoffeeCommand command = new UpdateCoffeeCommand(Context, mapper);
            command.coffeNameChange = "Mocha";
            command.updateCoffeeModel = new UpdateCoffeeModel() { coffeeName = "Latte", genreId = 1, price = 100};

            //act
            //assert
            FluentActions
               .Invoking(() => command.Handle())
               .Should().Throw<InvalidOperationException>().And.Message.Should().Be("There is already a Coffee with this name  - Bu isimde kahve zaten var");
        }

        [Fact]
        public void WhenThereIsNoCoffeeToUpdate_InvalidOperationException_ShouldBeReturn()
        {

            UpdateCoffeeCommand command = new UpdateCoffeeCommand(Context, mapper);
            command.coffeNameChange = "Mochargrgr";
            command.updateCoffeeModel = new UpdateCoffeeModel() { coffeeName = "Lattfefe", genreId = 1, price = 100 };

            //act
            //assert
            FluentActions
               .Invoking(() => command.Handle())
               .Should().Throw<InvalidOperationException>().And.Message.Should().Be("This coffee is not in - Bu Kahve yok");
        }
    }
}
