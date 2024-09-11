using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CoffeeShop.Entities
{
    public class FavoriteGenre
    {
        [Key]
        [Column(Order = 0)]
        public int CustomerID { get; set; }

        [Key]
        [Column(Order = 1)]
        public int GenreID { get; set; }

        public Customer Customer { get; set; }
        public GenreCoffee GenreCoffee { get; set; }
    }
}
