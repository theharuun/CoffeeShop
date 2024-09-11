using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Entities
{
    public class GenreCoffee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Coffee> Coffees { get; set; } = new List<Coffee>();
        public bool IsActive { get; set; } = true;
    }
}
