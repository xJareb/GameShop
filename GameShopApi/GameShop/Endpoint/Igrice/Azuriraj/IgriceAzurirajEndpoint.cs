 using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Endpoint.Igrice.Azuriraj
{
    [Tags("Igrice")]
    public class IgriceAzurirajEndpoint : MyBaseEndpoint <IgriceAzurirajRequest, IgriceAzurirajResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IgriceAzurirajEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPut("AzurirajIgricu")]
        public override async Task<IgriceAzurirajResponse> Obradi([FromBody]IgriceAzurirajRequest request, CancellationToken cancellationToken = default)
        {
            var igrica = _applicationDbContext.Igrice.Where(i=>i.Id == request.IgricaID).FirstOrDefault();
            if(igrica == null)
                throw new Exception("Igrica nije pronađena za id: " +  request.IgricaID);

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
