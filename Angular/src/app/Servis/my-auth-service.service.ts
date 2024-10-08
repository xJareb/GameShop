import { Injectable } from '@angular/core';
import {AutentifikacijaToken, PrijavaResponse} from "../Komponente/prijava/prijava-response";

@Injectable({
  providedIn: 'root'
})
export class MyAuthServiceService {

  constructor() { }

  dohvatiAutorzacijskiToken():any | null{
    let tokenString = window.localStorage.getItem("korisnik")??"";
    try {
      return JSON.parse(tokenString);
    }
    catch (e){
      return null;
    }
  }
  jelAdmin():boolean{
    return this.dohvatiAutorzacijskiToken()?.autentifikacijaToken.korisnickiNalog.isAdmin ?? false;
  }
  jelLogiran():boolean{
    return this.dohvatiAutorzacijskiToken()?.isLogiran ?? false;
  }
  prikazImena():string {
    return this.dohvatiAutorzacijskiToken()?.autentifikacijaToken.korisnickiNalog.korisnickoIme ?? "Prijavi se";
  }
  jelKorisnik():boolean{
    return this.dohvatiAutorzacijskiToken()?.autentifikacijaToken.korisnickiNalog.isKorisnik ?? false;
  }
}
