import { Component } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {Route, Router, RouterLink} from "@angular/router";
import {MojConfig} from "../../moj-config";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {PrijavaRequest} from "./prijava-request";
import {PrijavaResponse} from "./prijava-response";

@Component({
  selector: 'app-prijava',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterLink,
    HttpClientModule,
    FormsModule
  ],
  templateUrl: './prijava.component.html',
  styleUrl: './prijava.component.css'
})
export class PrijavaComponent {

  public loginPodaci: PrijavaRequest|null = null;
  lozinka: any;
  korisnickoIme: any;

  constructor(public httpClient:HttpClient, private route:Router) {
  }

  prijaviSe() {
    let url = MojConfig.adresa_servera + `/Prijavi-se`;

    this.loginPodaci = {
      korisnickoIme: this.korisnickoIme,
      lozinka: this.lozinka
    };


    if(this.loginPodaci.korisnickoIme != null && this.loginPodaci.lozinka != null){
    this.httpClient.post<PrijavaResponse>(url, this.loginPodaci).subscribe(x => {
        if (!x.isLogiran) {
          alert('Pogrešni podaci');
        } else {
          let token = x.autentifikacijaToken.vrijednost;
          window.localStorage.setItem('my-auth-token', token);
          window.localStorage.setItem('korisnik', JSON.stringify(x));
          window.localStorage.setItem('ime', JSON.stringify(x.autentifikacijaToken.korisnickiNalog.korisnickoIme));
          alert('Uspješno logiran korisnik');

          this.route.navigate(["/"]);
        }
      })
    }else{
      alert('Pogrešni podaci');
    }

  }
}
