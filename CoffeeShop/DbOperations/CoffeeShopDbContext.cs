using CoffeeShop.Entities;
using Microsoft.EntityFrameworkCore;


namespace CoffeeShop.DbOperations
{
    public class CoffeeShopDbContext : DbContext, ICoffeeShopDbContext
    {
        public CoffeeShopDbContext(DbContextOptions<CoffeeShopDbContext> options) : base(options){}

        public DbSet<GenreCoffee> GenreCoffees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Coffee> Coffees { get; set; }
        public DbSet<PurchasedCoffee> purchasedCoffees { get; set; }
        public DbSet<FavoriteGenre> favoriteGenres { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Coffee - GenreCoffee (Birden çoğa ilişki)
            modelBuilder.Entity<Coffee>()
                .HasOne(c => c.GenreCoffee)
                .WithMany(g => g.Coffees)
                .HasForeignKey(c => c.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            // Customer - PurchasedCoffee (Çoktan çoğa ilişki)
            modelBuilder.Entity<PurchasedCoffee>()
                .HasKey(pc => new { pc.CustomerID, pc.CoffeeID });

            modelBuilder.Entity<PurchasedCoffee>()
                .HasOne(pc => pc.Customer)
                .WithMany(c => c.purchasedCoffees)
                .HasForeignKey(pc => pc.CustomerID);

            modelBuilder.Entity<PurchasedCoffee>()
                .HasOne(pc => pc.Coffee)
                .WithMany()
                .HasForeignKey(pc => pc.CoffeeID);

            // Customer - FavoriteGenre (Çoktan çoğa ilişki)
            modelBuilder.Entity<FavoriteGenre>()
                .HasKey(fg => new { fg.CustomerID, fg.GenreID });

            modelBuilder.Entity<FavoriteGenre>()
                .HasOne(fg => fg.Customer)
                .WithMany(c => c.favoriteGenres)
                .HasForeignKey(fg => fg.CustomerID);

            modelBuilder.Entity<FavoriteGenre>()
                .HasOne(fg => fg.GenreCoffee)
                .WithMany()
                .HasForeignKey(fg => fg.GenreID);

            // Customer - Order (Birden çoğa ilişki)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders) // Not: Orders listesi eklemen gerekiyor Customer sınıfına
                .HasForeignKey(o => o.CustomerID);


 
        }


        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
