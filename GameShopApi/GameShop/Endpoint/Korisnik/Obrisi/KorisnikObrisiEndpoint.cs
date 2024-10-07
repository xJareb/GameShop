using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.Korisnik.Obrisi
{
    [Tags("Korisnik")]
    public class KorisnikObrisiEndpoint : MyBaseEndpoint<KorisnikObrisiRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public KorisnikObrisiEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPut("ObrisiKorisnika")]
        public override async Task<NoResponse> Obradi([FromQuery]KorisnikObrisiRequest request, CancellationToken cancellationToken = default)
        {
            var korisnik = _applicationDbContext.Korisnik.Include(kn=>kn.KNalog).Where(k=>k.Id == request.ID).FirstOrDefault();
            if(korisnik == null)
                throw new Exception("Korisnik nije pronadjen za id: " + request.ID);

            if(request.isBlackList == false){
                korisnik.KNalog.isDeleted = true;
            }
            else{
                korisnik.KNalog.isBlackList = true;
            }
            

            _applicationDbContext.Update(korisnik);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
