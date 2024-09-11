using CoffeeShop.Aplication.CoffeeOperations.Queries.GetCoffeeByName;
using CoffeeShopTests.TestsSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopTests.Aplication.CoffeeOperations.Queries.GetCoffeeByName
{
    public class GetCoffeeByNameValidatorTests : IClassFixture<CommonTestsFixture>
    {
        [Theory]
        [InlineData("l")]
        [InlineData("la")]
        [InlineData("")]       // coffeeName boş
        [InlineData("Lat")]    // coffeeName 4 karakterden kısa
        public void WhenInvalidCoffeeNameIsGiven_Validator_ShouldReturnErrors(string coffeeName)
        {
            // Arrange
            GetCoffeeByNameQuery query = new GetCoffeeByNameQuery(null,null)
            {
                coffeeName = coffeeName
            };

            GetCoffeeByNameQueryValidator validator = new GetCoffeeByNameQueryValidator();

            // Act
            var result = validator.Validate(query);

            // Assert
            Assert.False(result.IsValid); // Hatalı coffeeName için IsValid false olmalı
        }

        [Fact]
        public void WhenValidCoffeeNameIsGiven_Validator_ShouldNotReturnError()
        {
            // Arrange
            GetCoffeeByNameQuery query = new GetCoffeeByNameQuery(null,null)
            {
                coffeeName = "Latte"
            };

            GetCoffeeByNameQueryValidator validator = new GetCoffeeByNameQueryValidator();

            // Act
            var result = validator.Validate(query);

            // Assert
            Assert.True(result.IsValid); // Doğru coffeeName için IsValid true olmalı
        }
    }
}

