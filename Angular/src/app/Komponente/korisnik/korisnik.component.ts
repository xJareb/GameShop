import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {FormsModule} from "@angular/forms";
import {NgForOf, NgIf} from "@angular/common";
import {Korisnik, LogiraniKorisnik} from "./logirani-korisnik";
import {DetaljiIgrice} from "../detalji-igrice/detalji-igrice";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-korisnik',
  standalone: true,
  imports: [HttpClientModule, FormsModule, NgForOf, NgIf, RouterLink],
  templateUrl: './korisnik.component.html',
  styleUrl: './korisnik.component.css'
})
export class KorisnikComponent implements OnInit{

    public podaciLogKorisnik:Korisnik [] = [];

    constructor(public httpClient:HttpClient) {
    }
    ngOnInit(): void {

        let id = this.dohvatiKorisnika().autentifikacijaToken.korisnickiNalog.id;
        let url = MojConfig.adresa_servera + `/PregledLog?LogiraniKorisnikID=${id}`;

        this.httpClient.get<LogiraniKorisnik>(url).subscribe((x:LogiraniKorisnik)=>{
          this.podaciLogKorisnik = x.korisnik;
        })
    }
    dohvatiKorisnika(){
      return JSON.parse(window.localStorage.getItem("korisnik")??"");
    }
}
