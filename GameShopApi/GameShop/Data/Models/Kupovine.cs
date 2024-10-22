using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameShop.Data.Models
{
    public class Kupovine
    {
        [Key]
        public int Id { get; set; }
        public DateTime DatumKupovine { get; set; }
        public List<Igrice> Igrice { get; set; } = new List<Igrice>();
        [ForeignKey(nameof(Korisnik))]
        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }
    }
}

