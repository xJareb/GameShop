using GameShop.Data;
using GameShop.Data.Models;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Endpoint.Igrice.Dodaj
{
    [Tags("Igrice")]
    public class IgriceDodajEndpoint : MyBaseEndpoint<IgriceDodajRequest, IgriceDodajResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IgriceDodajEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost("DodajIgricu")]
        public override async Task<IgriceDodajResponse> Obradi([FromBody]IgriceDodajRequest request, CancellationToken cancellationToken = default)
        {
            var novaIgrica = new Data.Models.Igrice()
            {
                Naziv = request.Naziv,
                ZanrID = request.ZanrID,
                DatumIzlaska = request.DatumIzlaska,
                Slika = request.Slika,
                Izdavac = request.Izdavac,
                Opis = request.Opis,
                Cijena = request.Cijena,
                PostotakAkcije = request.PostotakAkcije,
                AkcijskaCijena = request.Cijena - (request.Cijena * (request.PostotakAkcije / 100))
            };
            _applicationDbContext.Add(novaIgrica);
            await _applicationDbContext.SaveChangesAsync();

            return new IgriceDodajResponse()
            {

            };
        }
    }
}
