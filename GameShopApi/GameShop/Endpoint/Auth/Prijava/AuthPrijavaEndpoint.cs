﻿using GameShop.Data;
using GameShop.Data.Models;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.Auth.Prijava
{
    [Tags("Auth")]
    public class AuthPrijavaEndpoint : MyBaseEndpoint<AuthPrijavaRequest, MyAuthInfo>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AuthPrijavaEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPost("Prijavi-se")]
        public override async Task<MyAuthInfo> Obradi([FromBody]AuthPrijavaRequest request, CancellationToken cancellationToken = default)
        {
            //1- provjera logina
            var logiraniKorisnik = await _applicationDbContext.Korisnik.Include(kn=>kn.KNalog)
                .FirstOrDefaultAsync(k =>
                    k.KNalog.KorisnickoIme == request.KorisnickoIme, cancellationToken);

            if (!LozinkaHasher.VerifikujLozinku(request.Lozinka, logiraniKorisnik.KNalog.Lozinka)){
                throw new Exception("Korisnik ne postoji");
            }

            if (logiraniKorisnik == null)
            {
                //pogresan username i password
                return new MyAuthInfo(null);
            }
            //2- generisati random string
            string randomString = TokenGenerator.Generate(10);

            //3- dodati novi zapis u tabelu AutentifikacijaToken za logiraniKorisnikId i randomString
            var noviToken = new AutentifikacijaToken()
            {
                ipAdresa = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                vrijednost = randomString,
                korisnickiNalog = logiraniKorisnik.KNalog,
                vrijemeEvidentiranja = DateTime.Now,
                KorisnikID = logiraniKorisnik.Id
            };
            _applicationDbContext.Add(noviToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            //4- vratiti token string
            return new MyAuthInfo(noviToken);
        }
    }
}
