namespace GameShop.Endpoint.Igrice.Kategorisi
{
    public class IgriceKategorisiRequest
    {
        public int ZanrID {  get; set; }
        public int PocetnaCijena { get; set; } = 1;
        public int KrajnjaCijena { get; set; } = 250;
    }
}
