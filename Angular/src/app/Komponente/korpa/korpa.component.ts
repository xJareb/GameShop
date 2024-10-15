import {Component, OnInit} from '@angular/core';
import {RouterLink} from "@angular/router";
import {NgForOf, NgIf} from "@angular/common";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {MyAuthServiceService} from "../../Servis/my-auth-service.service";
import {IzlistajKorpu, Korpa} from "./Izlistaj-korpu";
import {FormsModule} from "@angular/forms";
import {AzurirajKolicinu} from "./azuriraj-kolicinu";

@Component({
  selector: 'app-korpa',
  standalone: true,
  imports: [
    RouterLink,
    NgIf,
    HttpClientModule,
    NgForOf,
    FormsModule
  ],
  templateUrl: './korpa.component.html',
  styleUrl: './korpa.component.css'
})
export class KorpaComponent implements OnInit{

    public listaKorpe:Korpa[] = [];
    public pocetnoStanje:boolean = true;
    public ukupnaAkcijskaCijena:number = 0;
    public ukupnaPravaCijena:number = 0;
    public razlikaCijena:number = 0;
    public kolicinaRequest:AzurirajKolicinu | null = null;

    constructor(public httpClient:HttpClient, public authService:MyAuthServiceService) {

    }
    ngOnInit(): void {
        this.ucitajKorpu();
    }
    ucitajKorpu(){
      let korisnikID = this.authService.dohvatiAutorzacijskiToken()?.autentifikacijaToken.korisnikID;
      let url = MojConfig.adresa_servera + `/PretraziKorpu?Id=${korisnikID}`;

      this.httpClient.get<IzlistajKorpu>(url).subscribe((x:IzlistajKorpu) => {
        this.listaKorpe = x.korpa;
        if(this.listaKorpe.length > 0){
          this.pocetnoStanje = false;
          this.ukupnaAkcijskaCijena = this.listaKorpe.reduce((total, item) => total + (item.akcijskaCijena * item.kolicina), 0)
          this.ukupnaPravaCijena = this.listaKorpe.reduce((total, item) => total + (item.pravaCijena * item.kolicina), 0)
          this.razlikaCijena = this.ukupnaPravaCijena - this.ukupnaAkcijskaCijena;
        }else{
          this.pocetnoStanje = true;
          this.ukupnaAkcijskaCijena = 0;
          this.ukupnaPravaCijena = 0;
          this.razlikaCijena = 0;
        }
      })
    }

  obrisiIzKorpe(lk: Korpa) {
    let zapisID = lk.id;

    let url = MojConfig.adresa_servera + `/KorpaObrisiIgircu?ID=${zapisID}`;

    this.httpClient.delete(url).subscribe((x) => {
      this.ucitajKorpu();
    })
  }

  azurirajKolicinu(lk: Korpa, $event: Event) {
    let zapisID = lk.id;
    let kolicina = ($event.target as HTMLSelectElement).value;
    let url = MojConfig.adresa_servera + `/AzurirajKolicinu`;

    this.kolicinaRequest = {
      id:zapisID,
      kolicina: Number(kolicina),
    }

    this.httpClient.put(url,this.kolicinaRequest).subscribe((x) => {
      this.ucitajKorpu();
    })
  }
}
