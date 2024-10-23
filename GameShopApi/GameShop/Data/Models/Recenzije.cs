using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameShop.Data.Models
{
    public class Recenzije
    {
        [Key]
        public int Id { get; set; }
        public string Sadrzaj { get; set; }
        public int Ocjena { get; set; }
        [ForeignKey(nameof(Korisnik))]
        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }
        [ForeignKey(nameof(Igrice))]
        public int IgricaID { get; set; }
        public Igrice Igrice { get; set; }
    }
}
