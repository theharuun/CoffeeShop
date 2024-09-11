using AutoMapper;
using CoffeeShop.Aplication.CoffeeOperations.Command.CreateCoffee;
using CoffeeShop.Aplication.CoffeeOperations.Command.DeleteCoffee;
using CoffeeShop.Aplication.CoffeeOperations.Command.UpdateCoffee;
using CoffeeShop.Aplication.CoffeeOperations.Queries.GetCoffeeByName;
using CoffeeShop.Aplication.CoffeeOperations.Queries.GetsCoffee;
using CoffeeShop.DbOperations;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class CoffeeController : ControllerBase
    {
    
      
        private readonly ICoffeeShopDbContext _dbContext;
        private readonly IMapper _mapper;

        public CoffeeController(ICoffeeShopDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCoffees() 
        {
            GetsCoffeeQuery query = new GetsCoffeeQuery(_dbContext, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("coffeeName")]
        public IActionResult GetCoffees(string coffeeName)
        {
            GetCoffeeByNameQuery query = new GetCoffeeByNameQuery(_dbContext, _mapper);
            query.coffeeName = coffeeName;
            GetCoffeeByNameQueryValidator validator = new GetCoffeeByNameQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateCoffee([FromBody] CreateCoffeeModel model)
        {
            CreateCoffeeCommand command= new CreateCoffeeCommand(_dbContext, _mapper);
            command.Model = model;
            CreateCoffeeCommandValidator validator = new CreateCoffeeCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [Authorize]
        [HttpDelete("coffeeName")]
        public IActionResult DeleteCoffee(string coffeeName) 
        {
            DeleteCoffeeCommand command = new DeleteCoffeeCommand(_dbContext);
            command.coffeeName = coffeeName;
            DeleteCoffeeCommandValidator validationRules = new DeleteCoffeeCommandValidator();
            validationRules.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [Authorize]
        [HttpPut("coffeeName")]
        public IActionResult UpdateCoffee([FromBody] UpdateCoffeeModel model , string coffeeName)
        {
            UpdateCoffeeCommand command = new UpdateCoffeeCommand(_dbContext, _mapper);
            command.coffeNameChange = coffeeName;
            command.updateCoffeeModel = model;
            UpdateCoffeeCommandValidator validationRules= new UpdateCoffeeCommandValidator();
            validationRules.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}
