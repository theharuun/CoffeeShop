using CoffeeShop.DbOperations;
using CoffeeShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopTests.TestsSetup
{
    public static class FavoriteGenres 
    {
        public static void AddFavoriteGenres(this CoffeeShopDbContext context)
        {
            var genreIds = context.GenreCoffees.ToDictionary(g => g.Name, g => g.Id);
            context.favoriteGenres.AddRange(
                    new FavoriteGenre { CustomerID = 1, GenreID = genreIds["Etiyopya"] },
                    new FavoriteGenre { CustomerID = 1, GenreID = genreIds["Kolombiya"] },
                    new FavoriteGenre { CustomerID = 1, GenreID = 4 },


                    new FavoriteGenre { CustomerID = 2, GenreID = genreIds["Brezilya"] },
                    new FavoriteGenre { CustomerID = 2, GenreID = genreIds["Jamaika"] }


                );
        }
    }
}
