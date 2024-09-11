using AutoMapper;
using CoffeeShop.Aplication.CustomerOperations.Queries.GetCustomerByName;
using CoffeeShop.Aplication.OrderOperations.Command.CreateOrder;
using CoffeeShop.Aplication.OrderOperations.Command.DeleteOrder;
using CoffeeShop.Aplication.OrderOperations.Command.UpdateOrder;
using CoffeeShop.Aplication.OrderOperations.Queries.GetOrderByID;
using CoffeeShop.Aplication.OrderOperations.Queries.GetOrders;
using CoffeeShop.DbOperations;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class OrderController : ControllerBase
    {
        private readonly ICoffeeShopDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderController(ICoffeeShopDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetOrders()
        {
            GetOrdersQuery query = new GetOrdersQuery(_dbContext, _mapper);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpGet("orderId")]
        public IActionResult GetOrderByID(int orderId)
        {
            GetOrderByIDQuery query = new GetOrderByIDQuery(_dbContext, _mapper);
            query.OrderID = orderId;
            GetOrderByIDQueryValidator validator = new GetOrderByIDQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }



        [HttpPost]
        public IActionResult AddOrder([FromBody] CreateOrderModel model)
        {
            CreateOrderCommand command = new CreateOrderCommand(_dbContext, _mapper);
            command.Model = model;
            CreateOrderCommandValidator validationRules = new CreateOrderCommandValidator();
            validationRules.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }


        [HttpDelete("{OrderId}")]
        public IActionResult DeleteOrder(int OrderId)
        {
            DeleteOrderCommand command = new DeleteOrderCommand(_dbContext);

            command.orderId = OrderId;
            DeleteOrderCommandValidator validator = new DeleteOrderCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
        

        [HttpPut("{id}")]
        public IActionResult UpdateOrder([FromBody] UpdateOrderModel model, int id)
        {
            UpdateOrderCommand command = new UpdateOrderCommand(_dbContext, _mapper);
            command.OrderId = id;
            command.Model = model;
            UpdateOrderCommandValidator validator = new UpdateOrderCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}
