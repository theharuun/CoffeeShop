using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        // Satın aldığı filmler
        public List<PurchasedCoffee> purchasedCoffees { get; set; } = new List<PurchasedCoffee>();

        // Favori türler
        public List<FavoriteGenre> favoriteGenres { get; set; } = new List<FavoriteGenre>();

        public string Password { get; set; }
        public string? RefreshToken { get; set; }  // Zorunlu olmaktan çıkarıldı
        public DateTime? RefreshTokenExpireDate { get; set; }

        // Siparişler koleksiyonu
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
