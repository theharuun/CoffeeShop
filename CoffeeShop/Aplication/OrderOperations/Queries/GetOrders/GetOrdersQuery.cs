using AutoMapper;
using CoffeeShop.DbOperations;
using CoffeeShop.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Aplication.OrderOperations.Queries.GetOrders
{
    public class GetOrdersQuery
    {
        private readonly ICoffeeShopDbContext context;
        private readonly IMapper mapper;

        public GetOrdersQuery(ICoffeeShopDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<GetOrdersModel> Handle()
        {
            var orders = context.Orders.Include(x => x.Coffees).Include(x => x.Customer).OrderBy(x => x.OrderID).ToList();

            List<GetOrdersModel> list = mapper.Map<List<GetOrdersModel>>(orders);

            return list;
        }
    }



    public class GetOrdersModel
    {

        public string customers { get; set; }
        public DateTime OrderDate { get; set; }
        public List<string> Coffees { get; set; } 
    }
}
