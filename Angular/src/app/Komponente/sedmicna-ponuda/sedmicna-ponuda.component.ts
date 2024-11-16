  import {Component, OnInit} from '@angular/core';
import {NgForOf, NgIf} from "@angular/common";
import {MojConfig} from "../../moj-config";
import {HttpClient} from "@angular/common/http";
import {MyAuthServiceService} from "../../Servis/AuthService/my-auth-service.service";
import {Router} from "@angular/router";
  import {OfferResponse} from "../../Servis/SedmicnaPonudaService/offer-response";

@Component({
  selector: 'app-sedmicna-ponuda',
  standalone: true,
  imports: [
    NgForOf,
    NgIf
  ],
  templateUrl: './sedmicna-ponuda.component.html',
  styleUrl: './sedmicna-ponuda.component.css'
})
export class SedmicnaPonudaComponent implements OnInit{

    specialOffer:any;

    ngOnInit(): void {
      this.listOffer();
    }
    constructor(public httpClient:HttpClient, public authService:MyAuthServiceService, private router:Router) {
    }
    listOffer(){
      let url = MojConfig.adresa_servera + `/GameOffer`;

      this.httpClient.get<OfferResponse>(url).subscribe(x=>{
        this.specialOffer = x.games;
      })
    }

  deleteGame(sp: any) {
    let gameID = sp.id;
    let url = MojConfig.adresa_servera + `/GameHighlight?GameID=${gameID}&Highlighted=false`;
    this.httpClient.put(url,{},{
      headers:{
        "my-auth-token":this.authService.vratiToken()
      }
    }).subscribe(x=>{
      this.listOffer();
    })
  }

  goToDetails(sp: any) {
    let gameID = sp.id;
    this.router.navigate([`/detalji-igrice/${gameID}`])
  }
}
