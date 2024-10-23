using GameShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.GeneratorPodataka
{
    [ApiController]
    [Tags("GeneratorPodataka")]
    public class GenerisiPodatke : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GenerisiPodatke(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("Izbroji")]
        public ActionResult Count()
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("zanr", _applicationDbContext.Zanr.Count());
            data.Add("igrice",_applicationDbContext.Igrice.Count());

            return Ok(data);
        }
        [HttpPost("Podaci")]
        public ActionResult Genersi()
        {
            var zanrovi = new List<Data.Models.Zanr>();
            var igrice = new List<Data.Models.Igrice>();

            zanrovi.Add(new Data.Models.Zanr { Naziv = "Akcija" });
            zanrovi.Add(new Data.Models.Zanr { Naziv = "Avantura" });
            zanrovi.Add(new Data.Models.Zanr { Naziv = "RPG" });
            zanrovi.Add(new Data.Models.Zanr { Naziv = "Strategija" });
            zanrovi.Add(new Data.Models.Zanr { Naziv = "Sportske igre" });
            zanrovi.Add(new Data.Models.Zanr { Naziv = "Simulacija" });
            zanrovi.Add(new Data.Models.Zanr { Naziv = "FPS" });
            zanrovi.Add(new Data.Models.Zanr { Naziv = "Zagonetke" });
            zanrovi.Add(new Data.Models.Zanr { Naziv = "Horor" });
            zanrovi.Add(new Data.Models.Zanr { Naziv = "Trke" });

            igrice.Add(new Data.Models.Igrice { Naziv = "Call of Duty", ZanrID = 11, DatumIzlaska = new DateTime(2003, 10, 29), Slika = "https://th.bing.com/th/id/OIP.A5A4gd1XSOKS3mRQcdsGSgHaEo?rs=1&pid=ImgDetMain", Izdavac = "Activision", Opis = "Akciona pucačina.", Cijena = 60, PostotakAkcije = 10, AkcijskaCijena = 54, Izdvojeno = true });
            igrice.Add(new Data.Models.Igrice { Naziv = "The Legend of Zelda", ZanrID = 12, DatumIzlaska = new DateTime(1986, 2, 21), Slika = "https://wallpapercave.com/wp/wp3277308.jpg", Izdavac = "Nintendo", Opis = "Avanturistička igra.", Cijena = 50, PostotakAkcije = 5, AkcijskaCijena = 48, Izdvojeno = true });
            igrice.Add(new Data.Models.Igrice { Naziv = "The Witcher 3", ZanrID = 13, DatumIzlaska = new DateTime(2015, 5, 19), Slika = "https://wallpapercave.com/wp/wp1854644.jpg", Izdavac = "CD Projekt", Opis = "RPG igra u otvorenom svijetu.", Cijena = 40, PostotakAkcije = 15, AkcijskaCijena = 34, Izdvojeno = false });
            igrice.Add(new Data.Models.Igrice { Naziv = "Age of Empires IV", ZanrID = 14, DatumIzlaska = new DateTime(2021, 10, 28), Slika = "https://wallpapercave.com/wp/wp9682062.jpg", Izdavac = "Microsoft", Opis = "Strategija u realnom vremenu.", Cijena = 30, PostotakAkcije = 20, AkcijskaCijena = 24, Izdvojeno = false });
            igrice.Add(new Data.Models.Igrice { Naziv = "FIFA 24", ZanrID = 15, DatumIzlaska = new DateTime(2023, 9, 29), Slika = "https://cdn.wallpapersafari.com/75/27/gMRXKZ.jpeg", Izdavac = "EA Sports", Opis = "Sportska simulacija fudbala.", Cijena = 70, PostotakAkcije = 5, AkcijskaCijena = 67, Izdvojeno = true });
            igrice.Add(new Data.Models.Igrice { Naziv = "The Sims 4", ZanrID = 16, DatumIzlaska = new DateTime(2014, 9, 2), Slika = "https://th.bing.com/th/id/OIP.UZa4NWtYk26PJ-JKl7xhNAHaEK?rs=1&pid=ImgDetMain", Izdavac = "EA", Opis = "Simulacija života.", Cijena = 20, PostotakAkcije = 10, AkcijskaCijena = 18, Izdvojeno = false });
            igrice.Add(new Data.Models.Igrice { Naziv = "Battlefield 2042", ZanrID = 17, DatumIzlaska = new DateTime(2021, 11, 19), Slika = "https://images.hdqwalls.com/download/battlefield-2042-7e-1920x1080.jpg", Izdavac = "EA", Opis = "Pucačina iz prvog lica.", Cijena = 60, PostotakAkcije = 15, AkcijskaCijena = 51, Izdvojeno = true });
            igrice.Add(new Data.Models.Igrice { Naziv = "Portal 2", ZanrID = 18, DatumIzlaska = new DateTime(2011, 4, 19), Slika = "https://th.bing.com/th/id/R.7ecc8942720ef3fa55fc70bd34321bcd?rik=bltC5sgC6vSzTw&riu=http%3a%2f%2fwallpapercave.com%2fwp%2fp0wD9UF.jpg&ehk=CBpz84ISdEFu3hcUo3zCBIkFKC1%2fZNbPzl8Gdibgkb4%3d&risl=&pid=ImgRaw&r=0", Izdavac = "Valve", Opis = "Logička igra sa zagonetkama.", Cijena = 10, PostotakAkcije = 20, AkcijskaCijena = 8, Izdvojeno = false });
            igrice.Add(new Data.Models.Igrice { Naziv = "Resident Evil Village", ZanrID = 19, DatumIzlaska = new DateTime(2021, 5, 7), Slika = "https://wallpaperboat.com/wp-content/uploads/2021/05/09/77203/resident-evil-village-13.jpg", Izdavac = "Capcom", Opis = "Horor preživljavanja.", Cijena = 50, PostotakAkcije = 10, AkcijskaCijena = 45, Izdvojeno = false });
            igrice.Add(new Data.Models.Igrice { Naziv = "Need for Speed: Heat", ZanrID = 20, DatumIzlaska = new DateTime(2019, 11, 8), Slika = "https://th.bing.com/th/id/OIP.JTDySuE8xcBCXf7SXuEAdQHaEK?rs=1&pid=ImgDetMain", Izdavac = "EA", Opis = "Igra trka.", Cijena = 30, PostotakAkcije = 5, AkcijskaCijena = 28, Izdvojeno = true });



           _applicationDbContext.AddRange(zanrovi);
           _applicationDbContext.AddRange(igrice);
           _applicationDbContext.SaveChanges();

            return Count();
        }
    }
}
