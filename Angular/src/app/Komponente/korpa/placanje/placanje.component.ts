import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
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
    NgForOf
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
  userPayment : FormGroup;
  rememberStatus: boolean = false;

  constructor(public httpClient:HttpClient,public authService:MyAuthServiceService,public route:Router) {
    this.userPayment = new FormGroup({
      brojKartice: new FormControl("",[Validators.required,Validators.pattern(/^\d{16}$/)]),
      imePrezime: new FormControl("",[Validators.required,Validators.pattern(/^[A-Z][a-z]+ [A-Z][a-z]+$/)]),
      datumIsteka: new FormControl("",[Validators.required]),
      sigurnosniBroj: new FormControl("",[Validators.required,Validators.pattern(/^\d{3}$/)]),
    })
  }
  ngOnInit(): void {
      this.total = Number((window.localStorage.getItem("cijena")));
      this.loadUserCards();
  }
  setStyle(control:string){
    if (this.userPayment.controls[control].invalid && !this.userPayment.controls[control].untouched) {
      return {
        'background-color': 'red',
        'color': 'white'
      }
    } else {
      return {}
    }
  }
  checkedRemember():boolean{
    let checkbox = document.getElementById('exampleCheck1') as HTMLInputElement;
    if(checkbox.checked){
      return true;
    }
    return false;
  }

  payment() {
    const validForm = this.userPayment.valid;
    if(this.selectedIndex == null) {
      if (validForm) {
        if (this.checkedRemember()) {
          this.saveCard()
        }
        this.createPurchase();
      }
    }else{
      this.createPurchase();
    }
  }
  disableLetters(event:Event){
    const input = event.target as HTMLInputElement;
    input.value = input.value.replace(/[^0-9]/g, '')
  }
  disableNumbers(event:Event){
    const input = event.target as HTMLInputElement;
    input.value = input.value.replace(/[^a-zA-Z ]/g, '')
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
  saveCard(){
    let url = MojConfig.adresa_servera + `/CardAdd`;
    let userID = this.authService.userID();

    this.cardRequest = {
      cardNumber: this.ngCardNumber,
      expirationDate: this.ngExpirationDate,
      userID: userID
    }
    this.httpClient.post(url,this.cardRequest,{
      headers:{
        "my-auth-token":this.authService.returnToken()
      }
    }).subscribe(x=>{
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

  onCheckboxChange(index: number) {
    this.selectedIndex = this.selectedIndex === index ? null : index;
    if(this.selectedIndex != null){
      this.rememberStatus = true;
      this.userPayment.disable()
    }else{
      this.rememberStatus = false;
      this.userPayment.enable();
    }
  }
}
