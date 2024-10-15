using System.ComponentModel.DataAnnotations.Schema;

namespace GameShop.Data.Models
{
    public class Kartica
    {
        public int Id { get; set; }
        public string BrojKartice { get; set; }
        public string Istek { get; set; }
        [ForeignKey(nameof(Korisnik))]
        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }
    }
}
