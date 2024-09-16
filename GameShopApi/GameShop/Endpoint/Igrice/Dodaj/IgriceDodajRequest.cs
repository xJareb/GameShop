namespace GameShop.Endpoint.Igrice.Dodaj
{
    public class IgriceDodajRequest
    {
        public string Naziv { get; set; }
        public int ZanrID { get; set; }
        public DateTime DatumIzlaska { get; set; }
        public string Slika { get; set; }
        public string Izdavac {  get; set; }
        public string Opis { get; set; }
        public float Cijena { get; set; }
        public float PostotakAkcije {  get; set; }
    }
}
