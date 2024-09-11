using CoffeeShop.DbOperations;
using CoffeeShop.TokenOperations;
using CoffeeShop.TokenOperations.Models;

namespace CoffeeShop.Aplication.CustomerOperations.Command.RefreshToken
{
    public class RefreshTokenCommand
    {

        public string RefreshToken { get; set; }
        private readonly ICoffeeShopDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public RefreshTokenCommand(ICoffeeShopDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _dbContext.Customers.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
            if (user != null)
            {
                //token yarat
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.ExpirationDate.AddMinutes(3);

                _dbContext.SaveChanges();

                return token;
            }
            else
            {
                throw new InvalidOperationException("Valid Bir Refresh Token Bulunamadi!!!");
            }
        }
    }
}
