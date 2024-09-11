using AutoMapper;
using CoffeeShop.DbOperations;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Aplication.OrderOperations.Queries.GetOrderByID
{
    public class GetOrderByIDQuery
    {
        private readonly ICoffeeShopDbContext context;
        private readonly IMapper mapper;

        public int OrderID { get; set; }
        public GetOrderByIDQuery(ICoffeeShopDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public GetOrderByIDModel Handle()
        {
            var order = context.Orders.Include(x => x.Coffees).Include(x => x.Customer).Where(s=>s.OrderID == OrderID).SingleOrDefault();
            if (order == null)
                throw new InvalidOperationException("Order Not Found - Sipariş Bulunamdı");

            GetOrderByIDModel model =mapper.Map<GetOrderByIDModel>(order);
            return model;

        }
    }
     public class GetOrderByIDModel
    {

        public string customers { get; set; }
        public DateTime OrderDate { get; set; }
        public List<string> Coffees { get; set; }
    }
}
