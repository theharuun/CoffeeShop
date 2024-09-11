using AutoMapper;
using CoffeeShop.DbOperations;
using CoffeeShop.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Aplication.OrderOperations.Command.UpdateOrder
{
    public class UpdateOrderCommand
    {
        public readonly ICoffeeShopDbContext _context;
        public readonly IMapper _mapper;
        public UpdateOrderModel Model;
        public int OrderId { get; set; }
        public UpdateOrderCommand(ICoffeeShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            // Mevcut siparişi ve ilişkili verileri al
            var order = _context.Orders
                .Include(c => c.Customer)
                .ThenInclude(c => c.purchasedCoffees)
                .Include(o => o.Coffees)  // Coffees koleksiyonunu da dahil et
                .SingleOrDefault(x => x.OrderID == OrderId);

            if (order == null)
                throw new InvalidOperationException("Bu sipariş bulunamadı");

            // Sipariş tarihini güncelle
            order.OrderDate = Model.orderDate != default ? Model.orderDate : order.OrderDate;

            // Kahve ekleme işlemi
            var coffeeListToAdd = Model.AddCoffeesIDs != null && Model.AddCoffeesIDs.Count > 0
                ? _context.Coffees.Where(c => Model.AddCoffeesIDs.Contains(c.CoffeeID)).ToList()
                : new List<Coffee>();

            // Yeni kahveleri ekle
            foreach (var coffee in coffeeListToAdd)
            {
               
                    order.Coffees.Add(coffee);

                   var purchasedCoffee = new PurchasedCoffee
                        {
                            CustomerID = order.Customer.CustomerID,
                            CoffeeID = coffee.CoffeeID
                           
                        };
                        // PurchasedCoffees listesine ekle
                        _context.purchasedCoffees.Add(purchasedCoffee);
                
            }

            // Kahve silme işlemi
            var coffeeListToDelete = Model.DeleteCoffeesIDs != null && Model.DeleteCoffeesIDs.Count > 0
                ? _context.Coffees.Where(c => Model.DeleteCoffeesIDs.Contains(c.CoffeeID)).ToList()
                : new List<Coffee>();

            // Kahveleri kaldır
            foreach (var coffee in coffeeListToDelete)
            {
                if (order.Coffees.Contains(coffee))
                {
                    order.Coffees.Remove(coffee);

                    // Müşterinin purchasedCoffees listesinden sil
                    var purchasedCoffeeToRemove = order.Customer.purchasedCoffees
                        .SingleOrDefault(pc => pc.CoffeeID == coffee.CoffeeID);
                    if (purchasedCoffeeToRemove != null)
                    {
                        _context.purchasedCoffees.Remove(purchasedCoffeeToRemove);
                    }
                }
            }


            // Değişiklikleri kaydet
            _context.SaveChanges();
        }
    }

    public class UpdateOrderModel
    {
        public DateTime orderDate { get; set; }
        public List<int> AddCoffeesIDs { get; set; }

        public List<int> DeleteCoffeesIDs { get; set; }
    }
}
