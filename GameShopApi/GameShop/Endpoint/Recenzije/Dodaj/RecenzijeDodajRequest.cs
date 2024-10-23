namespace GameShop.Endpoint.Recenzije.Dodaj
{
    public class RecenzijeDodajRequest
    {
        public int KorisnikID { get; set; }
        public int IgricaID { get; set; }
        public int Ocjena {  get; set; }
        public string Sadrzaj { get; set; }
    }
}
