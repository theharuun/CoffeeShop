using AutoMapper;
using CoffeeShop.DbOperations;
using CoffeeShop.TokenOperations;
using CoffeeShop.TokenOperations.Models;

namespace CoffeeShop.Aplication.CustomerOperations.Command.CreateToken
{
    public class CreateTokenCommand
    {

        private readonly IMapper _mapper;
        public CreateTokenModel Model { get; set; }
        private readonly ICoffeeShopDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public CreateTokenCommand(ICoffeeShopDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _dbContext.Customers.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
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
                throw new InvalidOperationException("Kullanici Email veya Sifre HATALI!!!");
            }

        }

        public class CreateTokenModel
        {

            public string Email { get; set; }
            public string Password { get; set; }

        }
    }
}
