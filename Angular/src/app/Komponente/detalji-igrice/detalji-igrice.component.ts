import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, RouterLink} from "@angular/router";
import {MojConfig} from "../../moj-config";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {DetaljiIgrice} from "./detalji-igrice";
import {NgForOf, NgIf} from "@angular/common";
import {MyAuthServiceService} from "../../Servis/my-auth-service.service";
import {RecenzijaComponent} from "./recenzija/recenzija.component";

@Component({
  selector: 'app-detalji-igrice',
  standalone: true,
  imports: [HttpClientModule, NgForOf, RouterLink, RecenzijaComponent, NgIf],
  templateUrl: './detalji-igrice.component.html',
  styleUrl: './detalji-igrice.component.css'
})
export class DetaljiIgriceComponent implements OnInit{

  igricaID:any;
  detaljiIgrice:any;


  public prikazFormeZaRecenziju:boolean = false;

  constructor(public activatedRoute:ActivatedRoute,public httpClient:HttpClient, public authService:MyAuthServiceService) {
  }
  ngOnInit(): void {
    this.igricaID = this.activatedRoute.snapshot.params["id"];
    let url = MojConfig.adresa_servera + `/Pretrazi?IgricaID=${this.igricaID}`;

    this.httpClient.get<DetaljiIgrice>(url).subscribe(x=>{
      this.detaljiIgrice = x.igrice;
    })

  }

  dodajUKorpu(di: any) {

    //TODO :: preformulisati endpoint
    let igricaID = di.id;
    let korisnikID = this.authService.dohvatiAutorzacijskiToken().autentifikacijaToken.korisnikID;

    let url = MojConfig.adresa_servera + `/DodajUKorpu`;

    let requestBody={
      "korisnikID": korisnikID,
      "igricaID": igricaID,
      "kolicina": 1
    }

    this.httpClient.post(url,requestBody).subscribe(x=>{
      alert('Uspješno dodano u korpu');
    })
  }
  otvaranjeFormeZaRecenziju($event : boolean)
  {
    this.prikazFormeZaRecenziju = $event;
  }

  pripremiPodatke(di: any) {
    this.igricaID = di.id;
  }
}
