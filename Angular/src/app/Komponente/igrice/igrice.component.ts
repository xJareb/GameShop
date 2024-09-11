import {Component, OnInit} from '@angular/core';
import {Router, RouterLink} from "@angular/router";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {Igrice, ListaIgrica} from "./lista-igrica";
import {NgForOf, NgIf} from "@angular/common";
import {Zanr} from "./zanr";
import {FormsModule} from "@angular/forms";
import {filter} from "rxjs";

@Component({
  selector: 'app-igrice',
  standalone: true,
  imports: [
    RouterLink,
    HttpClientModule,
    NgForOf,
    NgIf,
    FormsModule
  ],
  templateUrl: './igrice.component.html',
  styleUrl: './igrice.component.css'
})
export class IgriceComponent implements OnInit{

  listaIgrica:any;
  listaZanrova:any;

  pocetnaCijena:number = 1;
  zavrsnaCijena:number = 100;
  zanr:any;
  constructor(public httpClient:HttpClient,private router:Router) {
  }
  ngOnInit(): void {
    this.filter(0,1,250);
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

  filter(zanrid:any,pocetnacijena:any,zavrsnacijena:any) {
    this.zanr = zanrid
    let url = MojConfig.adresa_servera +
      `/ByKategorija?ZanrID=${this.zanr}&PocetnaCijena=${this.pocetnaCijena}&KrajnjaCijena=${this.zavrsnaCijena}`

    this.httpClient.get<ListaIgrica>(url).subscribe(x=>{
      this.listaIgrica = x.igrice;
    })

  }
  uzmiZanr(lz: any) {
    this.zanr = lz.id;
  }
}
