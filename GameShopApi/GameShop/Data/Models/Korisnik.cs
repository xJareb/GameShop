using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameShop.Data.Models
{
    public class Korisnik
    {
        [Key]
        public int Id { get; set; }
        public string Ime { get; set; }
        public string? Prezime { get; set; }
        public byte[]? Slika {  get; set; }
        public string? GoogleSlika {  get; set; } 
        [ForeignKey(nameof(KNalog))]
        public int KorisnickiNalogID { get; set; }
        public KorisnickiNalog KNalog { get; set; }

    }
}
