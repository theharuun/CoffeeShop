using CoffeeShop.Aplication.CoffeeOperations.Command.UpdateCoffee;
using CoffeeShopTests.TestsSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopTests.Aplication.CoffeeOperations.Command.UpdateCoffee
{
    public class UpdateCoffeeCommandValidatorTests : IClassFixture<CommonTestsFixture>
    {
        [Theory]
        [InlineData("l")]
        [InlineData("")]       // coffeNameChange boş
        [InlineData("La")]     // coffeNameChange 3 karakterden kısa
        public void WhenInvalidCoffeeNameChangeIsGiven_Validator_ShouldReturnErrors(string coffeNameChange)
        {
            // Arrange
            UpdateCoffeeCommand command = new UpdateCoffeeCommand(null,null)
            {
                coffeNameChange = coffeNameChange 
            };

            UpdateCoffeeCommandValidator validator = new UpdateCoffeeCommandValidator();

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.False(result.IsValid); // Hatalı coffeeNameChange için IsValid false olmalı
        }

        [Fact]
        public void WhenValidCoffeeNameChangeIsGiven_Validator_ShouldNotReturnError()
        {
            // Arrange
            UpdateCoffeeCommand command = new UpdateCoffeeCommand(null,null)
            {
                coffeNameChange = "Espresso"
            };

            UpdateCoffeeCommandValidator validator = new UpdateCoffeeCommandValidator();

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.True(result.IsValid); // Doğru coffeeNameChange için IsValid true olmalı
        }
    }
}

