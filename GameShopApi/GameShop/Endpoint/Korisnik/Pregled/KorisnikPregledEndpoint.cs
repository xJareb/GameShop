using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GameShop.Endpoint.Korisnik.Pregled
{
    [Tags("Korisnik")]
    public class KorisnikPregledEndpoint : MyBaseEndpoint<KorisnikPregledRequest, KorisnikPregledResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;

        public KorisnikPregledEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }
        [HttpGet("PregledSvih")]
        public override async Task<KorisnikPregledResponse> Obradi([FromQuery] KorisnikPregledRequest request, CancellationToken cancellationToken = default)
        {
            if (!_authService.jelLogiran())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var korisnici = await _applicationDbContext.Korisnik.Include(kn => kn.KNalog).Where(k => k.KNalog.isKorisnik == true && k.KNalog.isDeleted == false && k.KNalog.isBlackList == request.isBlackList).Select(x => new KorisnikPregledResponseKorisnik()
            {
                ID = x.Id,
                Ime = x.Ime,
                Prezime = x.Prezime,
                KorisnickoIme = x.KNalog.KorisnickoIme,
                Email = x.KNalog.Email
            }).ToListAsync();


            return new KorisnikPregledResponse()
            {
                Korisnici = korisnici
            };
        }
    }
}
