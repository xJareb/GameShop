import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router, RouterLink} from "@angular/router";
import {MojConfig} from "../../moj-config";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {NgForOf, NgIf} from "@angular/common";
import {MyAuthServiceService} from "../../Servis/AuthService/my-auth-service.service";
import {RecenzijaComponent} from "./recenzija/recenzija.component";
import {NgbRatingModule} from "@ng-bootstrap/ng-bootstrap";
import {GameDetailsData} from "../../Servis/DetaljiIgriceService/game-details-data";
import {Review, ReviewResponse} from "../../Servis/RecenzijeService/reviews-response";
import {Purchase, PurchasesResponse} from "../../Servis/KupovineService/purchases-response";

@Component({
  selector: 'app-detalji-igrice',
  standalone: true,
  imports: [HttpClientModule, NgForOf, RouterLink, RecenzijaComponent, NgIf, NgbRatingModule],
  templateUrl: './detalji-igrice.component.html',
  styleUrl: './detalji-igrice.component.css'
})
export class DetaljiIgriceComponent implements OnInit{

  gameID:any;
  gameDetails:any;

  public listOfReviews:Review[] = [];
  public kupovineResponse:Purchase[] = [];
  public conditionReview:boolean = false;


  public showReviewModal:boolean = false;

  constructor(public activatedRoute:ActivatedRoute,public httpClient:HttpClient, public authService:MyAuthServiceService, public route:Router) {
  }
  ngOnInit(): void {
    this.gameID = this.activatedRoute.snapshot.params["id"];
    let url = MojConfig.adresa_servera + `/GamesGet?GameID=${this.gameID}`;

    this.httpClient.get<GameDetailsData>(url).subscribe(x=>{
      this.gameDetails = x.game;
    })
    this.listReviews();
    this.checkConditionToLeaveReview();
  }

  addToCart(di: any) {
    let gameID = di.id;
    let userID = this.authService.dohvatiAutorzacijskiToken().autentifikacijaToken.korisnikID;

    let url = MojConfig.adresa_servera + `/ShoppingCartAdd`;

    let requestBody={
      "userID": userID,
      "gameID": gameID,
      "quantity": 1
    }

    this.httpClient.post(url,requestBody,{
      headers:{
        "my-auth-token":this.authService.vratiToken()
      }
    }).subscribe(x=>{
      this.route.navigate(['/igrice']);
    })
  }
  openReviewModal($event : boolean)
  {
    this.showReviewModal = $event;
  }
  prepareData(di: any) {
    this.gameID = di.id;
  }
  listReviews(){
    let url = MojConfig.adresa_servera + `/ReviewsGetBy?GameID=${this.gameID}`;

    this.httpClient.get<ReviewResponse>(url).subscribe(x=>{
      this.listOfReviews = x.reviews
      this.listOfReviews.some(x=>{
        if(x.gameID == this.gameID && x.userID == this.authService.korisnikID()){
          this.conditionReview = false;
        }
      })
    })
  }
  checkConditionToLeaveReview(){
    let url = MojConfig.adresa_servera + `/PurchaseGet`;
    this.httpClient.get<PurchasesResponse>(url,{
      headers:{
        "my-auth-token":this.authService.vratiToken()
      }
    }).subscribe(x=>{
      this.kupovineResponse = x.purchases;
      this.kupovineResponse.some(x=>{
        if(this.authService.korisnikID() == x.userID){
          x.games.some(i=>{
            if(i.id == this.gameID){
              this.conditionReview = true;
            }
          })
        }
      })
    })
  }

}
