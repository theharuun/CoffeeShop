using AutoMapper;
using CoffeeShop.DbOperations;
using CoffeeShop.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Aplication.OrderOperations.Command.CreateOrder
{
    public class CreateOrderCommand
    {
        public readonly ICoffeeShopDbContext _context;
        public readonly IMapper _mapper;
        public CreateOrderModel Model { get; set; }
        public CreateOrderCommand(ICoffeeShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            // Sipariş veritabanında zaten var mı kontrol edin
            var order = _context.Orders
                .Include(x=>x.Customer)
                .SingleOrDefault(x => x.CustomerID == Model.customerID && x.OrderDate == Model.orderDate);

            if (order != null)
                throw new InvalidOperationException($"Bu Siparişi bu müsteri zaten verdi");

            // Yeni siparişi haritadan oluşturun
            order = _mapper.Map<Order>(Model);

            if (Model.coffeesIDs != null && Model.coffeesIDs.Any())
            {
                // Film ID'lerine göre veritabanından filmleri alın
                var coffees = _context.Coffees
                                     .Where(movie => Model.coffeesIDs.Contains(movie.CoffeeID))
                                     .ToList();

                // Filmleri siparişe ekleyin
                order.Coffees = coffees;

                // Müşteriyi veritabanından alın
                var customer = _context.Customers
                                       .Include(c => c.purchasedCoffees)
                                       .SingleOrDefault(c => c.CustomerID == Model.customerID);

                if (customer == null)
                    throw new InvalidOperationException("Müşteri bulunamadı");

                // Satın alınan filmleri müşterinin listesine ekleyin
                foreach (var coffee in coffees)
                {
                    // Aynı müşteri ve film için mevcut bir kayıt olup olmadığını kontrol edin
                    if (!customer.purchasedCoffees.Any(pm => pm.CoffeeID == coffee.CoffeeID))
                    {
                        customer.purchasedCoffees.Add(new PurchasedCoffee
                        {
                            CoffeeID = coffee.CoffeeID
                        });
                    }
                }
            }

            _context.Orders.Add(order);
            _context.SaveChanges();
        }


    }

    public class CreateOrderModel
    {

        public DateTime orderDate { get; set; }
        public int customerID { get; set; }
        public List<int> coffeesIDs { get; set; }
    }

}
