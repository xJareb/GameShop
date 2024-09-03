using GameShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<KorisnickiNalog> KorisnickiNalog { get; set; }
        public DbSet<Admin> Admin {  get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }

        public ApplicationDbContext(
            DbContextOptions options) : base(options)
        {
        }
    }
    
}
