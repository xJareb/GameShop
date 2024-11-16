namespace GameShop.Endpoint.Genres.Get
{
    public class GenresGetResponse
    {
        public List<GenresGetResponseGenre> Genres { get; set; }
    }
    public class GenresGetResponseGenre
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
