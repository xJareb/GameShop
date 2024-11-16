import {Component, OnInit} from '@angular/core';
import {RouterLink} from "@angular/router";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {NgForOf, NgIf} from "@angular/common";
import {KupnjeIgriceComponent} from "./kupnje-igrice/kupnje-igrice.component";
import {MyAuthServiceService} from "../../Servis/AuthService/my-auth-service.service";
import {Purchase, PurchasesResponse} from "../../Servis/KupovineService/purchases-response";

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

    public purchaseResponse:Purchase[] = [];
    public showModalOverview: boolean = false;
    public gameList:any;
    constructor(public httpClient:HttpClient,public authService:MyAuthServiceService) {
    }
    ngOnInit(): void {
        this.listPurchase();
    }
    listPurchase(){
      let url = MojConfig.adresa_servera + `/PurchaseGet`;
      this.httpClient.get<PurchasesResponse>(url,{
        headers:{
          "my-auth-token":this.authService.returnToken()
        }
      }).subscribe(x=>{
        this.purchaseResponse = x.purchases;
      })
    }

    prepareData(k: Purchase) {
      this.gameList = k.games;

    }
    openModalOverview($event : boolean)
    {
    this.showModalOverview = $event;
    }
}
