using CoffeeShop.Aplication.CoffeeOperations.Command.DeleteCoffee;
using CoffeeShopTests.TestsSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopTests.Aplication.CoffeeOperations.Command.DeleteCoffee
{
    public class DeleteCoffeeCommandValidatorTests : IClassFixture<CommonTestsFixture>
    {
        [Theory]
        [InlineData("")]       // coffeeName boş
        [InlineData("l")]
        [InlineData("La")]     // coffeeName 3 karakterden kısa
        public void WhenInvalidCoffeeNameIsGiven_Validator_ShouldReturnErrors(string coffeeName)
        {
            // Arrange
            DeleteCoffeeCommand command = new DeleteCoffeeCommand(null)
            {
                coffeeName = coffeeName
            };

            DeleteCoffeeCommandValidator validator = new DeleteCoffeeCommandValidator();

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.False(result.IsValid); // Hatalı coffeeName için IsValid false olmalı
        }

        [Fact]
        public void WhenValidCoffeeNameIsGiven_Validator_ShouldNotReturnError()
        {
            // Arrange
            DeleteCoffeeCommand command = new DeleteCoffeeCommand(null)
            {
                coffeeName = "Latte"
            };

            DeleteCoffeeCommandValidator validator = new DeleteCoffeeCommandValidator();

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.True(result.IsValid); // Doğru coffeeName için IsValid true olmalı
        }
    }
}
