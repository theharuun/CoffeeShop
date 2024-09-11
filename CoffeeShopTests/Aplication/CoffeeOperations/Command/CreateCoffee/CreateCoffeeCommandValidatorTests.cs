using CoffeeShop.Aplication.CoffeeOperations.Command.CreateCoffee;
using CoffeeShopTests.TestsSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopTests.Aplication.CoffeeOperations.Command.CreateCoffee
{
    public class CreateCoffeeCommandValidatorTests : IClassFixture<CommonTestsFixture>
    {

        [Theory]
        [InlineData("", 1, 5)]   // coffeeName boş
        [InlineData("", 0, 5)]   // coffeeName boş
        [InlineData("", 1, 0)]   // coffeeName boş
        [InlineData("l", 1, 5)]   // coffeeName boş
        [InlineData("l", 0, 5)]   // coffeeName boş
        [InlineData("l", 1, 0)]   // coffeeName boş
        [InlineData("La", 1, 5)]  // coffeeName 3 karakterden kısa
        [InlineData("La", 0, 5)]  // coffeeName 3 karakterden kısa
        [InlineData("La", 1, 0)]  // coffeeName 3 karakterden kısa
        [InlineData("Latte", 0, 5)] // genreId 0
        [InlineData("Latte", 1, 0)]   // price 0
        [InlineData("Latte", 1, -1)]  // price negatif
        [InlineData("Latte", -1, 5)] // genreId 0
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string coffeeName, int genreId, int price)
        {
            // Arrange
            CreateCoffeeCommand command = new CreateCoffeeCommand(null,null)
            {
                Model = new CreateCoffeeModel
                {
                    coffeeName = coffeeName,
                    genreId = genreId,
                    price = price
                }
            };

            CreateCoffeeCommandValidator validator = new CreateCoffeeCommandValidator();

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.False(result.IsValid); // Hatalı inputlar için IsValid false olmalı
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // Arrange
            CreateCoffeeCommand command = new CreateCoffeeCommand(null,null)
            {
                Model = new CreateCoffeeModel
                {
                    coffeeName = "Latte21",
                    genreId = 1,
                    price = 5
                }
            };

            CreateCoffeeCommandValidator validator = new CreateCoffeeCommandValidator();

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.True(result.IsValid); // Doğru inputlar için IsValid true olmalı
        }
    }
}
