namespace GameShop.Endpoint.Kartica.Dodaj
{
    public class KarticaDodajRequest
    {
        public string BrojKartice {  get; set; }
        public DateTime DatumIsteka { get; set; }
        public int KorisnikID { get; set; }
    }
}
