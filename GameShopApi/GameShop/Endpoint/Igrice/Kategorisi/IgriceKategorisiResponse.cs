namespace GameShop.Endpoint.Igrice.Kategorisi
{
    public class IgriceKategorisiResponse
    {
        public List<IgriceKategorisiResponseIgrica> Igrice {  get; set; }
    }
    public class IgriceKategorisiResponseIgrica
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int ZanrID { get; set; }
        public string Zanr { get; set; }
        public DateTime DatumIzlaska { get; set; }
        public string Slika { get; set; }
        public string Izdavac { get; set; }
        public string Opis { get; set; }
        public float Cijena { get; set; }
        public float PostotakAkcije { get; set; }
        public float AkcijskaCijena { get; set; }
    }
}
