import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, RouterLink} from "@angular/router";
import {MojConfig} from "../../moj-config";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {DetaljiIgrice} from "./detalji-igrice";
import {NgForOf} from "@angular/common";

@Component({
  selector: 'app-detalji-igrice',
  standalone: true,
  imports: [HttpClientModule, NgForOf, RouterLink],
  templateUrl: './detalji-igrice.component.html',
  styleUrl: './detalji-igrice.component.css'
})
export class DetaljiIgriceComponent implements OnInit{

  igricaID:any;
  detaljiIgrice:any;

  constructor(public activatedRoute:ActivatedRoute,public httpClient:HttpClient) {
  }
  ngOnInit(): void {
    this.igricaID = this.activatedRoute.snapshot.params["id"];
    let url = MojConfig.adresa_servera + `/Pretrazi?IgricaID=${this.igricaID}`;

    this.httpClient.get<DetaljiIgrice>(url).subscribe(x=>{
      this.detaljiIgrice = x.igrice;
    })

  }

}
