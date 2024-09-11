using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Entities
{
    public class Coffee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CoffeeID { get; set; }  // Property olarak tanımlandı
        public string? CoffeeName { get; set; }

        public int GenreId { get; set; }
        public GenreCoffee GenreCoffee { get; set; }

        public int Price { get; set; }

        // soğuk/sıcak durumu 
        public bool IsCold { get; set; } = true;

       


    }
}
