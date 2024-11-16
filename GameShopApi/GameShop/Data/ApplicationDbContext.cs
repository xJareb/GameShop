using GameShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Games> Game { get; set; }
        public DbSet<AuthenticationToken> AuthenticationToken { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<Card> Card { get; set; }
        public DbSet<Purchases> Purchases { get; set; }
        public DbSet<Reviews> Reviews { get; set; }

        public ApplicationDbContext(
            DbContextOptions options) : base(options)
        {
        }
    }
    
}
