namespace GameShop.Endpoint.Korisnik.Dodaj
{
    public class KorisnikDodajRequest
    {
        public string Ime { get; set; }
        public string? Prezime { get; set; }
        public string? KorisnickoIme { get; set; }
        public string Email { get; set; }
        public DateTime? DatumRodjenja { get; set; }
        public string? Lozinka {  get; set; }
  
    }
}
