namespace GameShop.Endpoint.Korisnik.Azuriraj
{
    public class KorisnikAzurirajRequest
    {
        public int KorisnikID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Lozinka {  get; set; }  
    }
}
