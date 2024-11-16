using GameShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.GenerateData
{
    [ApiController]
    [Tags("GenerateData")]
    public class GenerateData : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GenerateData(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("Count")]
        public ActionResult Count()
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("genres", _applicationDbContext.Genre.Count());
            data.Add("games", _applicationDbContext.Game.Count());

            return Ok(data);
        }
        [HttpPost("Data")]
        public ActionResult Generate()
        {
            var genres = new List<Data.Models.Genre>();
            var games = new List<Data.Models.Games>();

            genres.Add(new Data.Models.Genre { Name = "Akcija" });
            genres.Add(new Data.Models.Genre { Name = "Avantura" });
            genres.Add(new Data.Models.Genre { Name = "RPG" });
            genres.Add(new Data.Models.Genre { Name = "Strategija" });
            genres.Add(new Data.Models.Genre { Name = "Sportske igre" });
            genres.Add(new Data.Models.Genre { Name = "Simulacija" });
            genres.Add(new Data.Models.Genre { Name = "FPS" });
            genres.Add(new Data.Models.Genre { Name = "Zagonetke" });
            genres.Add(new Data.Models.Genre { Name = "Horor" });
            genres.Add(new Data.Models.Genre { Name = "Trke" });

            games.Add(new Data.Models.Games { Name = "Call of Duty", Genre = genres.FirstOrDefault(z => z.Name == "Akcija"), ReleaseDate = new DateTime(2003, 10, 29), Photo = "https://th.bing.com/th/id/OIP.A5A4gd1XSOKS3mRQcdsGSgHaEo?rs=1&pid=ImgDetMain", Publisher = "Activision", Description = "Akciona pucačina.", Price = 60, PercentageDiscount = 10, ActionPrice = 54, Highlighted = true });
            games.Add(new Data.Models.Games { Name = "The Legend of Zelda", Genre = genres.FirstOrDefault(z => z.Name == "Avantura"), ReleaseDate = new DateTime(1986, 2, 21), Photo = "https://wallpapercave.com/wp/wp3277308.jpg", Publisher = "Nintendo", Description = "Avanturistička igra.", Price = 50, PercentageDiscount = 5, ActionPrice = 48, Highlighted = true });
            games.Add(new Data.Models.Games { Name = "The Witcher 3", Genre = genres.FirstOrDefault(z => z.Name == "RPG"), ReleaseDate = new DateTime(2015, 5, 19), Photo = "https://wallpapercave.com/wp/wp1854644.jpg", Publisher = "CD Projekt", Description = "RPG igra u otvorenom svijetu.", Price = 40, PercentageDiscount = 15, ActionPrice = 34, Highlighted = false });
            games.Add(new Data.Models.Games { Name = "Age of Empires IV", Genre = genres.FirstOrDefault(z => z.Name == "Strategija"), ReleaseDate = new DateTime(2021, 10, 28), Photo = "https://wallpapercave.com/wp/wp9682062.jpg", Publisher = "Microsoft", Description = "Strategija u realnom vremenu.", Price = 30, PercentageDiscount = 20, ActionPrice = 24, Highlighted = false });
            games.Add(new Data.Models.Games { Name = "FIFA 24", Genre = genres.FirstOrDefault(z => z.Name == "Sportske igre"), ReleaseDate = new DateTime(2023, 9, 29), Photo = "https://cdn.wallpapersafari.com/75/27/gMRXKZ.jpeg", Publisher = "EA Sports", Description = "Sportska simulacija fudbala.", Price = 70, PercentageDiscount = 5, ActionPrice = 67, Highlighted = true });
            games.Add(new Data.Models.Games { Name = "The Sims 4", Genre = genres.FirstOrDefault(z => z.Name == "Simulacija"), ReleaseDate = new DateTime(2014, 9, 2), Photo = "https://th.bing.com/th/id/OIP.UZa4NWtYk26PJ-JKl7xhNAHaEK?rs=1&pid=ImgDetMain", Publisher = "EA", Description = "Simulacija života.", Price = 20, PercentageDiscount = 10, ActionPrice = 18, Highlighted = false });
            games.Add(new Data.Models.Games { Name = "Battlefield 2042", Genre = genres.FirstOrDefault(z => z.Name == "FPS"), ReleaseDate = new DateTime(2021, 11, 19), Photo = "https://images.hdqwalls.com/download/battlefield-2042-7e-1920x1080.jpg", Publisher = "EA", Description = "Pucačina iz prvog lica.", Price = 60, PercentageDiscount = 15, ActionPrice = 51, Highlighted = true });
            games.Add(new Data.Models.Games { Name = "Portal 2", Genre = genres.FirstOrDefault(z => z.Name == "Zagonetke"), ReleaseDate = new DateTime(2011, 4, 19), Photo = "https://th.bing.com/th/id/R.7ecc8942720ef3fa55fc70bd34321bcd?rik=bltC5sgC6vSzTw&riu=http%3a%2f%2fwallpapercave.com%2fwp%2fp0wD9UF.jpg&ehk=CBpz84ISdEFu3hcUo3zCBIkFKC1%2fZNbPzl8Gdibgkb4%3d&risl=&pid=ImgRaw&r=0", Publisher = "Valve", Description = "Logička igra sa zagonetkama.", Price = 10, PercentageDiscount = 20, ActionPrice = 8, Highlighted = false });
            games.Add(new Data.Models.Games { Name = "Resident Evil Village", Genre = genres.FirstOrDefault(z => z.Name == "Horor"), ReleaseDate = new DateTime(2021, 5, 7), Photo = "https://wallpaperboat.com/wp-content/uploads/2021/05/09/77203/resident-evil-village-13.jpg", Publisher = "Capcom", Description = "Horor preživljavanja.", Price = 50, PercentageDiscount = 10, ActionPrice = 45, Highlighted = false });
            games.Add(new Data.Models.Games { Name = "Need for Speed: Heat", Genre = genres.FirstOrDefault(z => z.Name == "Trke"), ReleaseDate = new DateTime(2019, 11, 8), Photo = "https://th.bing.com/th/id/OIP.JTDySuE8xcBCXf7SXuEAdQHaEK?rs=1&pid=ImgDetMain", Publisher = "EA", Description = "Igra trka.", Price = 30, PercentageDiscount = 5, ActionPrice = 28, Highlighted = true });



            _applicationDbContext.AddRange(genres);
            _applicationDbContext.AddRange(games);
            _applicationDbContext.SaveChanges();

            return Count();
        }
    }
}
