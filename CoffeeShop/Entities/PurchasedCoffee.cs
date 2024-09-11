using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Entities
{
    public class PurchasedCoffee
    {
        [Key]
        [Column(Order = 0)]
        public int CustomerID { get; set; }

        [Key]
        [Column(Order = 1)]
        public int CoffeeID { get; set; }

        public Customer Customer { get; set; }
        public Coffee Coffee { get; set; }
    }
}
