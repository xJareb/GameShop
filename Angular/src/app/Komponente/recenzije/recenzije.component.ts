import {Component, OnInit} from '@angular/core';
import {NgbRating, NgbRatingModule} from "@ng-bootstrap/ng-bootstrap";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {Recenzije, RecenzijeResponse} from "../../Servis/RecenzijeService/recenzije-response";
import {NgForOf} from "@angular/common";

@Component({
  selector: 'app-recenzije',
  standalone: true,
  imports: [
    NgbRating, NgbRatingModule, HttpClientModule, NgForOf
  ],
  templateUrl: './recenzije.component.html',
  styleUrl: './recenzije.component.css'
})
export class RecenzijeComponent implements OnInit{

  public listaRecenzija:Recenzije[] = [];
  public rating = 1;

  constructor(public httpClient: HttpClient) {
  }
  ngOnInit(): void {
    this.prikaziRecenzije();
  }
  prikaziRecenzije(){
    let url = MojConfig.adresa_servera + `/PrikaziRecenzije`;

    this.httpClient.get<RecenzijeResponse>(url).subscribe((x:RecenzijeResponse)=>{
      this.listaRecenzija = x.recenzije;
      console.log(this.listaRecenzija);
    })
  }


}
