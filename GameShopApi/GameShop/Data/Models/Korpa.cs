using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameShop.Data.Models
{
    public class Korpa
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Korisnik))]
        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }
        [ForeignKey(nameof(Igrica))]
        public int IgricaID { get; set; }
        public Igrice Igrica { get; set; }
        public int Kolicina {  get; set; }
    }
}
