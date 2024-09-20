namespace GameShop.Endpoint.Igrice.Ponuda
{
    public class IgricePonudaResponse
    {
        public List<IgricePonudaResponseIgrice> igrice { get; set; }
    }
    public class IgricePonudaResponseIgrice
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Slika { get; set; }
        public float PostotakAkcije { get; set; }
        public float? AkcijskaCijena { get; set; }
    }
}
