using GameShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<KorisnickiNalog> KorisnickiNalog { get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Zanr> Zanr { get; set; }
        public DbSet<Igrice> Igrice { get; set; }
        public DbSet<AutentifikacijaToken> AutentifikacijaToken { get; set; }
        public DbSet<Korpa> Korpa { get; set; }
        public DbSet<Kartica> Kartica { get; set; }
        public DbSet<Kupovine> Kupovine { get; set; }
        public DbSet<Recenzije> Recenzije { get; set; }

        public ApplicationDbContext(
            DbContextOptions options) : base(options)
        {
        }
    }
    
}
