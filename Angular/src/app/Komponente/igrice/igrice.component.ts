import {Component, OnInit} from '@angular/core';
import {RouterLink} from "@angular/router";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {Igrice, ListaIgrica} from "./lista-igrica";
import {NgForOf} from "@angular/common";

@Component({
  selector: 'app-igrice',
  standalone: true,
  imports: [
    RouterLink,
    HttpClientModule,
    NgForOf
  ],
  templateUrl: './igrice.component.html',
  styleUrl: './igrice.component.css'
})
export class IgriceComponent implements OnInit{

  listaIgrica:any;
  constructor(public httpClient:HttpClient) {
  }
  ngOnInit(): void {
    let url = MojConfig.adresa_servera + `/Pretrazi`;

    this.httpClient.get<ListaIgrica>(url).subscribe(x=>{
      this.listaIgrica = x.igrice;
      console.log(this.listaIgrica);
    })
  }

}
