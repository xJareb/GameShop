 using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameShop.Endpoint.Igrice.Azuriraj
{
    [Tags("Igrice")]
    public class IgriceAzurirajEndpoint : MyBaseEndpoint <IgriceAzurirajRequest, IgriceAzurirajResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;

        public IgriceAzurirajEndpoint(ApplicationDbContext applicationDbContext, MyAuthService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }
        [HttpPut("AzurirajIgricu")]
        public override async Task<IgriceAzurirajResponse> Obradi([FromBody]IgriceAzurirajRequest request, CancellationToken cancellationToken = default)
        {
            if (!_myAuthService.jelAdmin())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var igrica = _applicationDbContext.Igrice.Where(i=>i.Id == request.IgricaID).FirstOrDefault();
            if(igrica == null)
                throw new Exception($"{HttpStatusCode.NotFound}");

            igrica.Naziv = request.Naziv;
            igrica.ZanrID = request.ZanrID;
            igrica.DatumIzlaska = request.DatumIzlaska;
            igrica.Slika = request.Slika;
            igrica.Izdavac = request.Izdavac;
            igrica.Opis = request.Opis;
            igrica.Cijena = request.Cijena;
            igrica.PostotakAkcije = request.PostotakAkcije;
            igrica.AkcijskaCijena = request.Cijena - (request.Cijena * (request.PostotakAkcije / 100));

            _applicationDbContext.Update(igrica);
            await _applicationDbContext.SaveChangesAsync();

            return new IgriceAzurirajResponse()
            {

            };
        }
    }
}
