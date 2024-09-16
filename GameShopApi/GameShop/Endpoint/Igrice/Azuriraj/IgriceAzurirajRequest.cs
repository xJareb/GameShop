namespace GameShop.Endpoint.Igrice.Azuriraj
{
    public class IgriceAzurirajRequest
    {
        public int IgricaID { get; set; }
        public string Naziv {  get; set; }
        public int ZanrID { get; set; }
        public DateTime DatumIzlaska { get; set; }
        public string Slika { get; set; }
        public string Izdavac {  get; set; }
        public string Opis { get; set; }
        public float Cijena { get; set; }
        public float PostotakAkcije { get; set; }
    }
}
