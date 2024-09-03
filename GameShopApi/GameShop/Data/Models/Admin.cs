using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameShop.Data.Models
{
    public class Admin
    {
        [Key]
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        [ForeignKey(nameof(KNalog))]
        public int KorisnickiNalogID { get; set; }
        public KorisnickiNalog KNalog { get; set; }
    }
}
