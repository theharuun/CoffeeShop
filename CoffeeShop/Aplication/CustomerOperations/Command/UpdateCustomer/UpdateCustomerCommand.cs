using AutoMapper;
using CoffeeShop.DbOperations;
using CoffeeShop.Entities;
using System.Linq;

namespace CoffeeShop.Aplication.CustomerOperations.Command.UpdateCustomer
{
    public class UpdateCustomerCommand
    {
        public readonly ICoffeeShopDbContext _context;
        public readonly IMapper _mapper;
        public string email;
        public UpdateCustomerModel Model { get; set; }
        public UpdateCustomerCommand(ICoffeeShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Email == email);
            if (customer == null)
                throw new InvalidOperationException("This EMAIL has not customer - Bu EMAİL musteri yok");

            // Satin aldigi filmleri ekle
            if (Model.purchasedCoffeeIDs != null && Model.purchasedCoffeeIDs.Any())
            {
                customer.purchasedCoffees = Model.purchasedCoffeeIDs.Select(purchasedCoffeId => new PurchasedCoffee
                {
                    CoffeeID = purchasedCoffeId
                }).ToList();
            }
            // favori türleri ekle 
            if (Model.favoriteGenresIDs != null && Model.favoriteGenresIDs.Any())
            {
                customer.favoriteGenres = Model.favoriteGenresIDs.Select(favGenreId => new FavoriteGenre
                {
                    GenreID = favGenreId
                }).ToList();
            }

            UpdatePurchasedMovies(Model.purchasedCoffeeIDs, customer.purchasedCoffees);
            UpdateFavoriteGenres(Model.favoriteGenresIDs, customer.favoriteGenres);

            _context.SaveChanges();
        }

        public void UpdatePurchasedMovies(List<int> purchasedCoffeeIDs, ICollection<PurchasedCoffee> purchasedCoffees)
        {
            // Mevcut ID'lerin bir kümesini oluştur
            var existingIds = purchasedCoffees.Select(pm => pm.CoffeeID).ToHashSet();

            // Yeni ID'lerin bir kümesini oluştur
            var newIds = new HashSet<int>(purchasedCoffeeIDs);

            // Mevcut listeyi güncelle
            foreach (var id in existingIds)
            {
                if (!newIds.Contains(id))
                {
                    // Mevcut ID listeyi güncellemek istiyorsak, bu ID'yi sil
                    var itemToRemove = purchasedCoffees.First(pm => pm.CoffeeID == id);
                    purchasedCoffees.Remove(itemToRemove);
                }
            }

            // Yeni ID'leri listeye ekle
            foreach (var id in newIds)
            {
                if (!existingIds.Contains(id))
                {
                    purchasedCoffees.Add(new PurchasedCoffee { CoffeeID = id });
                }
            }
        }
        public void UpdateFavoriteGenres(List<int> favoriteGenresIDs, ICollection<FavoriteGenre> favoriteGenres)
        {
            // Mevcut ID'lerin bir kümesini oluştur
            var existingIds = favoriteGenres.Select(pm => pm.GenreID).ToHashSet();

            // Yeni ID'lerin bir kümesini oluştur
            var newIds = new HashSet<int>(favoriteGenresIDs);

            // Mevcut listeyi güncelle
            foreach (var id in existingIds)
            {
                if (!newIds.Contains(id))
                {
                    // Mevcut ID listeyi güncellemek istiyorsak, bu ID'yi sil
                    var itemToRemove = favoriteGenres.First(pm => pm.GenreID == id);
                    favoriteGenres.Remove(itemToRemove);
                }
            }

            // Yeni ID'leri listeye ekle
            foreach (var id in newIds)
            {
                if (!existingIds.Contains(id))
                {
                    favoriteGenres.Add(new FavoriteGenre { GenreID = id });
                }
            }
        }


    }
    public class UpdateCustomerModel
    {

        // Satın aldığı filmler
        public List<int> purchasedCoffeeIDs { get; set; } // Movie ID'lerini tutacak liste

        // Favori türler
        public List<int> favoriteGenresIDs { get; set; } // favori genre ID'lerini tutacak liste
    }
}
