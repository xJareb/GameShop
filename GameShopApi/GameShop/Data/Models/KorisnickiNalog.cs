using System.ComponentModel.DataAnnotations;

namespace GameShop.Data.Models
{
    public class KorisnickiNalog
    {
        [Key]
        public int Id { get; set; }
        public string? KorisnickoIme { get; set; }
        public string Email {  get; set; }
        public string? Lozinka { get; set; }
        public DateTime? DatumRodjenja {  get; set; }
        public bool isAdmin { get; set; }
        public bool isKorisnik { get; set; }
        public bool isDeleted {  get; set; } = false;
        public bool isBlackList { get; set; } = false;
        public bool isGoogleProvider {  get; set; }

    }
}
