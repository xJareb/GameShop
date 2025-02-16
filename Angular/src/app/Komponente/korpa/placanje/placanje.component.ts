import {AfterViewInit, Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MyAuthServiceService} from "../../../Servis/AuthService/my-auth-service.service";
import {Router, RouterLink} from "@angular/router";
import {MojConfig} from "../../../moj-config";
import {NgForOf, NgIf, NgStyle} from "@angular/common";
import {KorpaComponent} from "../korpa.component"
import {CardAddRequest} from "../../../Servis/KorpaService/card-add-request";
import {PurchaseRequest} from "../../../Servis/KorpaService/purchase-request";
import {Card, CardsGetByUser} from "../../../Servis/KorpaService/cards-get-by-user";
import {PaypalComponent} from "./paypal/paypal.component";

declare var paypal: any;

@Component({
  selector: 'app-placanje',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    NgStyle,
    HttpClientModule,
    NgIf,
    RouterLink,
    NgForOf,
    PaypalComponent
  ],
  templateUrl: './placanje.component.html',
  styleUrl: './placanje.component.css'
})
export class PlacanjeComponent implements OnInit{

  public cardsGetByUser:Card[] = [];
  public cardRequest :CardAddRequest | null = null;
  public userPurchase: PurchaseRequest | null = null;
  ngCardNumber:any;
  ngExpirationDate:any;
  public total:number = 0;
  selectedIndex: number | null = null;
  rememberStatus: boolean = false;

  @Input() amount: number = 0;

  constructor(public httpClient:HttpClient,public authService:MyAuthServiceService,public route:Router) {
    
  }

  ngOnInit(): void {
      this.total = Number((window.localStorage.getItem("cijena")));
      this.loadUserCards();

  }

  checkedRemember():boolean{
    let checkbox = document.getElementById('exampleCheck1') as HTMLInputElement;
    if(checkbox.checked){
      return true;
    }
    return false;
  }
  createPurchase(){
    let url = MojConfig.adresa_servera + `/PurchaseAdd`;
    let userID = this.authService.userID();

    this.userPurchase = {
      userID:userID
    }

   this.httpClient.post(url,this.userPurchase,{
     headers:{
       "my-auth-token": this.authService.returnToken()
     }
   }).subscribe(x=>{
      this.deleteCartRange();
      this.route.navigate(['/aktivacija']);
      window.localStorage.setItem("cijena","");
    })
  }
  deleteCartRange(){
    let userID = this.authService.userID();
    let url = MojConfig.adresa_servera + `/CartDeleteRange?UserID=${userID}`;

    this.httpClient.delete(url,{
      headers:{
        "my-auth-token":this.authService.returnToken()
      }
    }).subscribe(x=>{

    })
  }
  loadUserCards(){
    let userID = this.authService.userID();
    let url = MojConfig.adresa_servera + `/CardGetBy?UserID=${userID}`

    this.httpClient.get<CardsGetByUser>(url,{
      headers:{
        "my-auth-token": this.authService.returnToken()
      }
    }).subscribe(x=>{
      this.cardsGetByUser = x.cards;
      console.log(this.cardsGetByUser);
    })
  }


  protected readonly Number = Number;
}
