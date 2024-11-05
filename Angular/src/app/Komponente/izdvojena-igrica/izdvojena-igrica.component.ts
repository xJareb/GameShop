import {Component, OnInit} from '@angular/core';
import {FormsModule} from "@angular/forms";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {Igrice, ListaIgrica} from "../../Servis/IgriceService/lista-igrica";
import {MojConfig} from "../../moj-config";
import {KupovineResponse} from "../../Servis/KupovineService/kupovine-response";
import {NgForOf, NgIf} from "@angular/common";
import {MyAuthServiceService} from "../../Servis/AuthService/my-auth-service.service";

@Component({
  selector: 'app-izdvojena-igrica',
  standalone: true,
  imports: [
    FormsModule, HttpClientModule, NgForOf, NgIf
  ],
  templateUrl: './izdvojena-igrica.component.html',
  styleUrl: './izdvojena-igrica.component.css'
})
export class IzdvojenaIgricaComponent implements OnInit{
    public listaIgrica:Igrice[]=[];
    public odabranaIgrica: Igrice[] = [];

    public ngIgricaID:number = 4;
    constructor(public httpClient:HttpClient, public authService:MyAuthServiceService) {
    }
    ngOnInit(): void {
        this.izlistajSveIgre();
        this.ucitajOabranuIgricu();
    }
    izlistajSveIgre(){
      let url = MojConfig.adresa_servera + `/Pretrazi`;

      this.httpClient.get<ListaIgrica>(url).subscribe(x=>{
        this.listaIgrica = x.igrice;
      })
    }
    ucitajOabranuIgricu(){
      let url = MojConfig.adresa_servera + `/Pretrazi?IgricaID=${this.ngIgricaID}`;

      this.httpClient.get<ListaIgrica>(url).subscribe(x=>{
        this.odabranaIgrica = x.igrice;
      })
    }

}
