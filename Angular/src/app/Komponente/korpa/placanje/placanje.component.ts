import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MyAuthServiceService} from "../../../Servis/AuthService/my-auth-service.service";
import {Router, RouterLink} from "@angular/router";
import {MojConfig} from "../../../moj-config";
import {NgIf, NgStyle} from "@angular/common";
import {KorpaComponent} from "../korpa.component"
import {CardAddRequest} from "../../../Servis/KorpaService/card-add-request";
import {PurchaseRequest} from "../../../Servis/KorpaService/purchase-request";

@Component({
  selector: 'app-placanje',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    NgStyle,
    HttpClientModule,
    NgIf,
    RouterLink
  ],
  templateUrl: './placanje.component.html',
  styleUrl: './placanje.component.css'
})
export class PlacanjeComponent implements OnInit{


  public cardRequest :CardAddRequest | null = null;
  public userPurchase: PurchaseRequest | null = null;
  ngCardNumber:any;
  ngExpirationDate:any;
  public total:number = 0;

  userPayment : FormGroup;

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
    const validnaForma = this.userPayment.valid;
    if(validnaForma){
      if(this.checkedRemember()){
        this.saveCard()
      }
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
    let userID = this.authService.dohvatiAutorzacijskiToken()?.autentifikacijaToken.korisnikID;

    this.userPurchase = {
      userID:userID
    }

   this.httpClient.post(url,this.userPurchase,{
     headers:{
       "my-auth-token": this.authService.vratiToken()
     }
   }).subscribe(x=>{
      this.deleteCartRange();
      this.route.navigate(['/aktivacija']);
      window.localStorage.setItem("cijena","");
    })
  }
  saveCard(){
    let url = MojConfig.adresa_servera + `/CardAdd`;
    let userID = this.authService.dohvatiAutorzacijskiToken()?.autentifikacijaToken.korisnikID;

    this.cardRequest = {
      cardNumber: this.ngCardNumber,
      expirationDate: this.ngExpirationDate,
      userID: userID
    }
    this.httpClient.post(url,this.cardRequest,{
      headers:{
        "my-auth-token":this.authService.vratiToken()
      }
    }).subscribe(x=>{
    })
  }
  deleteCartRange(){
    let userID = this.authService.dohvatiAutorzacijskiToken()?.autentifikacijaToken.korisnikID;
    let url = MojConfig.adresa_servera + `/CartDeleteRange?UserID=${userID}`;

    this.httpClient.delete(url,{
      headers:{
        "my-auth-token":this.authService.vratiToken()
      }
    }).subscribe(x=>{

    })
  }
}
