using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameShop.Data.Models
{
    public class Igrice
    {
        [Key]
        public int Id { get; set; }
        public string Naziv {  get; set; }
        [ForeignKey(nameof(Zanr))]
        public int ZanrID { get; set; }
        public Zanr Zanr { get; set; }
        public DateTime DatumIzlaska {  get; set; }
        public string Slika { get; set; }
        public string Izdavac {  get; set; }
        public string Opis { get; set; }
        public float Cijena { get; set; }
        public float PostotakAkcije { get; set; }
        public float? AkcijskaCijena {  get; set; }
        public bool Izdvojeno { get; set; } = false;
    }
}
