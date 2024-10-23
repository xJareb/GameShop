using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Endpoint.Recenzije.Dodaj
{
    [Tags("Recenzije")]
    public class RecenzijeDodajEndpoint : MyBaseEndpoint<RecenzijeDodajRequest,NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RecenzijeDodajEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPost("DodajRecenziju")]
        public override async Task<NoResponse> Obradi([FromBody]RecenzijeDodajRequest request, CancellationToken cancellationToken = default)
        {
            var provjeraDuplikata = _applicationDbContext.Recenzije.Where(r => request.KorisnikID == r.KorisnikID && request.IgricaID == r.IgricaID).FirstOrDefault();
            if (provjeraDuplikata != null)
                throw new Exception("Recenzija od strane korisnika " + request.KorisnikID + " za igricu " + request.IgricaID + " vec postoji");
            var novaRecenzija = new Data.Models.Recenzije()
            {
                Sadrzaj = request.Sadrzaj,
                Ocjena = request.Ocjena,
                KorisnikID = request.KorisnikID,
                IgricaID = request.IgricaID,
            };

            _applicationDbContext.Recenzije.Add(novaRecenzija);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
