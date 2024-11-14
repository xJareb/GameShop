import {Component, OnInit} from '@angular/core';
import {RouterLink} from "@angular/router";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {Zanr} from "../../Servis/ZanrService/zanr";
import {Kupovine, KupovineResponse} from "../../Servis/KupovineService/kupovine-response";
import {NgForOf, NgIf} from "@angular/common";
import {KupnjeIgriceComponent} from "./kupnje-igrice/kupnje-igrice.component";
import {MyAuthServiceService} from "../../Servis/AuthService/my-auth-service.service";

@Component({
  selector: 'app-kupnje',
  standalone: true,
  imports: [
    RouterLink,
    HttpClientModule,
    NgForOf,
    KupnjeIgriceComponent,
    NgIf
  ],
  templateUrl: './kupnje.component.html',
  styleUrl: './kupnje.component.css'
})
export class KupnjeComponent implements OnInit{

    public kupovineResponse:Kupovine[] = [];
    public prikazPregledaj: boolean = false;
    public listaIgrica:any;
    constructor(public httpClient:HttpClient,public authService:MyAuthServiceService) {
    }
    ngOnInit(): void {
        this.izlistajKupovine();
    }
    izlistajKupovine(){
      let url = MojConfig.adresa_servera + `/IzlistajKupovine`;
      this.httpClient.get<KupovineResponse>(url,{
        headers:{
          "my-auth-token":this.authService.vratiToken()
        }
      }).subscribe(x=>{
        this.kupovineResponse = x.kupovine;
      })
    }

    pripremiPodatke(k: Kupovine) {
      this.listaIgrica = k.igrice;

    }
    otvaranjePregled($event : boolean)
    {
    this.prikazPregledaj = $event;
    }
}
