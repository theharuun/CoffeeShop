using AutoMapper;
using CoffeeShop.Aplication.CoffeeOperations.Queries.GetCoffeeByName;
using CoffeeShop.Aplication.CoffeeOperations.Queries.GetsCoffee;
using CoffeeShop.Aplication.CustomerOperations.Command.CreateCustomer;
using CoffeeShop.Aplication.CustomerOperations.Command.CreateToken;
using CoffeeShop.Aplication.CustomerOperations.Command.DeleteCustomer;
using CoffeeShop.Aplication.CustomerOperations.Command.RefreshToken;
using CoffeeShop.Aplication.CustomerOperations.Command.UpdateCustomer;
using CoffeeShop.Aplication.CustomerOperations.Queries.GetCustomerByName;
using CoffeeShop.Aplication.CustomerOperations.Queries.GetCustomers;
using CoffeeShop.DbOperations;
using CoffeeShop.TokenOperations.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static CoffeeShop.Aplication.CustomerOperations.Command.CreateToken.CreateTokenCommand;

namespace CoffeeShop.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class CustomerController : ControllerBase
    {


        private readonly ICoffeeShopDbContext _dbContext;
        private readonly IMapper _mapper;
        readonly IConfiguration _configuration;

        public CustomerController(ICoffeeShopDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetCustomer()
        {
            GetCustomersQuery query = new GetCustomersQuery(_dbContext, _mapper);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_dbContext, _mapper, _configuration);
            command.Model = login;
            var token = command.Handle();
            return Ok(token);

        }

        [HttpGet("refreshToken")]
        public ActionResult<Token> refreshToken([FromQuery] string token)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_dbContext, _configuration);
            command.RefreshToken = token;
            var refreshToken = command.Handle();
            return Ok(refreshToken);

        }

        [HttpGet("customerEmail")]
        public IActionResult GetCoffees(string customerEmail)
        {
            GetCustomerByNameQuery query = new GetCustomerByNameQuery(_dbContext, _mapper);
            query.customerEmail = customerEmail;
            GetCustomerByNameQueryValidator validator = new GetCustomerByNameQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddCustomer([FromBody] CreateCustomerModel model)
        {
            CreateCustomerCommand command = new CreateCustomerCommand(_dbContext, _mapper);
            command.Model = model;
            CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("Email")]
        public IActionResult DeleteCustomer(string Email)
        {
            var query = new DeleteCustomerCommand(_dbContext);
            query.email = Email;
            DeleteCustomerCommandValidator validationRules = new DeleteCustomerCommandValidator();
            validationRules.ValidateAndThrow(query);
            query.Handle();
            return Ok();
        }


        [HttpPut("{Email}")]
        public IActionResult UpdateMovie([FromBody] UpdateCustomerModel model, string Email)
        {
            UpdateCustomerCommand command = new UpdateCustomerCommand(_dbContext, _mapper);
            command.email = Email;
            command.Model = model;
            UpdateCustomerCommandValidator validator = new UpdateCustomerCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}
