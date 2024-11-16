import {Component, OnInit} from '@angular/core';
import {RouterLink} from "@angular/router";
import {NgForOf, NgIf} from "@angular/common";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {MyAuthServiceService} from "../../Servis/AuthService/my-auth-service.service";
import {FormsModule} from "@angular/forms";
import {Cart, CartListResponse} from "../../Servis/KorpaService/cart-list-response";
import {QuantityRequest} from "../../Servis/KorpaService/quantity-request";

@Component({
  selector: 'app-korpa',
  standalone: true,
  imports: [
    RouterLink,
    NgIf,
    HttpClientModule,
    NgForOf,
    FormsModule,
  ],
  templateUrl: './korpa.component.html',
  styleUrl: './korpa.component.css'
})
export class KorpaComponent implements OnInit{

    public cartList:Cart[] = [];
    public firstState:boolean = true;
    public totalActionPrice:number = 0;
    public totalPrice:number = 0;
    public priceDifference:number = 0;
    public quantityRequest:QuantityRequest | null = null;

    constructor(public httpClient:HttpClient, public authService:MyAuthServiceService) {

    }
    ngOnInit(): void {
        this.loadCart();
    }
    loadCart(){
      let userID = this.authService.userID();
      let url = MojConfig.adresa_servera + `/CartGet?ID=${userID}`;

      this.httpClient.get<CartListResponse>(url,{
        headers:{
          "my-auth-token": this.authService.returnToken()
        }
      }).subscribe((x:CartListResponse) => {
        this.cartList = x.cart;
        if(this.cartList.length > 0){
          this.firstState = false;
          this.totalActionPrice = this.cartList.reduce((total, item) => total + (item.actionPrice * item.quantity), 0)
          this.totalPrice = this.cartList.reduce((total, item) => total + (item.price * item.quantity), 0)
          this.priceDifference = this.totalPrice - this.totalActionPrice;
        }else{
          this.firstState = true;
          this.totalActionPrice = 0;
          this.totalPrice = 0;
          this.priceDifference = 0;
        }
      })
    }

  deleteFromCart(lk: Cart) {
    let recordID = lk.id;

    let url = MojConfig.adresa_servera + `/CartDelete?ID=${recordID}`;

    this.httpClient.delete(url).subscribe((x) => {
      this.loadCart();
    })
  }
  updateQuantity(lk: Cart, $event: Event) {
    let recordID = lk.id;
    let quantity = ($event.target as HTMLSelectElement).value;
    let url = MojConfig.adresa_servera + `/CartUpdate`;

    this.quantityRequest = {
      id:recordID,
      quantity: Number(quantity),
    }

    this.httpClient.put(url,this.quantityRequest).subscribe((x) => {
      this.loadCart();
    })
  }

  handlePrice() {
    window.localStorage.setItem("cijena",this.totalActionPrice.toString())
  }
}
