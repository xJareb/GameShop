import { Injectable } from '@angular/core';
import {AutentifikacijaToken, PrijavaResponse} from "../PrijavaService/prijava-response";
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class MyAuthServiceService {

  constructor(public router:Router) { }

  dohvatiAutorzacijskiToken():any | null{
    let tokenString = window.localStorage.getItem("korisnik")??"";
    try {
      return JSON.parse(tokenString);
    }
    catch (e){
      return null;
    }
  }
  korisnikID():number{
    return this.dohvatiAutorzacijskiToken()?.autentifikacijaToken.korisnikID ?? 0;
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
  jelGoogleProvider():boolean{
    return this.dohvatiAutorzacijskiToken()?.autentifikacijaToken.korisnickiNalog.isGoogleProvider ?? false;
  }
  vratiToken():string{
    return this.dohvatiAutorzacijskiToken()?.autentifikacijaToken.vrijednost;
  }
  onemoguciKorpu(){
    if(!this.jelLogiran()){
      this.router.navigate(["/prijava"]);
    }
  }
}
