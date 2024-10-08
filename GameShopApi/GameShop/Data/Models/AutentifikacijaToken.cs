using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameShop.Data.Models
{
    public class AutentifikacijaToken
    {
        [Key]
        public int ID { get; set; }
        public string vrijednost {  get; set; }
        public int KorisnikID { get; set; }
        [ForeignKey(nameof(korisnickiNalog))]
        public int KorisnickiNalogID { get; set; }
        public KorisnickiNalog korisnickiNalog { get; set; }
        public DateTime vrijemeEvidentiranja { get; set; }
        public string? ipAdresa { get; set; }
    }
}
