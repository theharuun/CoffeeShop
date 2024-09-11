using CoffeeShop.DbOperations;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Aplication.OrderOperations.Command.DeleteOrder
{
    public class DeleteOrderCommand
    {
        public readonly ICoffeeShopDbContext _context;
        public int orderId { get; set; }

        public DeleteOrderCommand(ICoffeeShopDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var order = _context.Orders
                .Include(x=>x.Customer)
                .ThenInclude(c => c.purchasedCoffees)
                .SingleOrDefault(o => o.OrderID == orderId);

            if (order == null)
                throw new InvalidOperationException("Order not found - Sipariş bulunamadı");

            var customer = order.Customer;

            if (customer != null)
            {
                foreach (var coffee in order.Coffees)
                {
                    var purchasedCoffee = customer.purchasedCoffees
                        .SingleOrDefault(pm => pm.CoffeeID == coffee.CoffeeID);

                    if (purchasedCoffee != null)
                    {
                        customer.purchasedCoffees.Remove(purchasedCoffee);
                    }
                }
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
    }
}
