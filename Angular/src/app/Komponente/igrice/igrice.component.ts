import {Component, OnInit} from '@angular/core';
import {Router, RouterLink} from "@angular/router";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {Igrice, ListaIgrica} from "./lista-igrica";
import {NgForOf, NgIf} from "@angular/common";
import {Zanr} from "./zanr";

@Component({
  selector: 'app-igrice',
  standalone: true,
  imports: [
    RouterLink,
    HttpClientModule,
    NgForOf,
    NgIf
  ],
  templateUrl: './igrice.component.html',
  styleUrl: './igrice.component.css'
})
export class IgriceComponent implements OnInit{

  listaIgrica:any;
  listaZanrova:any;
  constructor(public httpClient:HttpClient,private router:Router) {
  }
  ngOnInit(): void {

    this.izlistajSveIgrice();
    this.izlistajZanrove();

  }

  idiUDetalje(li: any) {
    let igricaID = li.id;
    this.router.navigate([`/detalji-igrice/${igricaID}`])
  }
  izlistajZanrove(){
    let url = MojConfig.adresa_servera + `/Izlistaj`;
    this.httpClient.get<Zanr>(url).subscribe(x=>{
      this.listaZanrova = x.zanrovi;
    })
  }

  filtriraj(lz:any) {
    let zanrID = lz.id
    let url = MojConfig.adresa_servera + `/ByKategorija?ZanrID=${zanrID}`;
    this.httpClient.get<ListaIgrica>(url).subscribe(x=>{
     this.listaIgrica = x.igrice;
   })
  }
  izlistajSveIgrice(){
    let url = MojConfig.adresa_servera + `/Pretrazi`;

    this.httpClient.get<ListaIgrica>(url).subscribe(x=>{
      this.listaIgrica = x.igrice;
    })
  }

}

