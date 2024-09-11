using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CoffeeShop.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        public DateTime OrderDate { get; set; }

        // İlişkilendirilmiş müşteri
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        public ICollection<Coffee> Coffees { get; set; } = new List<Coffee>();
        

    }
}
