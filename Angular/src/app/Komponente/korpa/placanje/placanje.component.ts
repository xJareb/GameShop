import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {KreditnaKarticaRequest} from "../../../Servis/KorpaService/kreditna-kartica-request";
import {KupovinaRequest} from "../../../Servis/KorpaService/kupovina-request";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MyAuthServiceService} from "../../../Servis/AuthService/my-auth-service.service";
import {Router, RouterLink} from "@angular/router";
import {MojConfig} from "../../../moj-config";
import {NgIf, NgStyle} from "@angular/common";
import {KorpaComponent} from "../korpa.component"

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


  public kreditnaKartica :KreditnaKarticaRequest | null = null;
  public korisnikKupovina: KupovinaRequest | null = null;
  ngBrojKartice:any;
  ngDatumIsteka:any;
  public ukupno:number = 0;

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
      this.ukupno = Number((window.localStorage.getItem("cijena")));
  }
  postaviStil(kontrola:string){
    if (this.userPayment.controls[kontrola].invalid && !this.userPayment.controls[kontrola].untouched) {
      return {
        'background-color': 'red',
        'color': 'white'
      }
    } else {
      return {}
    }
  }
  cekiranoPamcenje():boolean{
    let checkbox = document.getElementById('exampleCheck1') as HTMLInputElement;
    if(checkbox.checked){
      return true;
    }
    return false;
  }

  placanje() {
    const validnaForma = this.userPayment.valid;
    if(validnaForma){
      if(this.cekiranoPamcenje()){
        this.sacuvajKreditnuKarticu()
      }
      this.napraviKupnju();
    }
  }
  restrikcijaUnosaSlova(event:Event){
    const unos = event.target as HTMLInputElement;
    unos.value = unos.value.replace(/[^0-9]/g, '')
  }
  restrikcijaUnosaBrojeva(event:Event){
    const unos = event.target as HTMLInputElement;
    unos.value = unos.value.replace(/[^a-zA-Z ]/g, '')
  }
  napraviKupnju(){
    let url = MojConfig.adresa_servera + `/DodajKupovinu`;
    let korisnikID = this.authService.dohvatiAutorzacijskiToken()?.autentifikacijaToken.korisnikID;

    this.korisnikKupovina = {
      korisnikID:korisnikID
    }

   this.httpClient.post(url,this.korisnikKupovina,{
     headers:{
       "my-auth-token": this.authService.vratiToken()
     }
   }).subscribe(x=>{
      this.obrisiOpsegKorpe();
      this.route.navigate(['/aktivacija']);
      window.localStorage.setItem("cijena","");
    })
  }
  sacuvajKreditnuKarticu(){
    let url = MojConfig.adresa_servera + `/DodajKarticu`;
    let korisnikID = this.authService.dohvatiAutorzacijskiToken()?.autentifikacijaToken.korisnikID;

    this.kreditnaKartica = {
      brojKartice: this.ngBrojKartice,
      datumIsteka: this.ngDatumIsteka,
      korisnikID: korisnikID
    }

    this.httpClient.post(url,this.kreditnaKartica,{
      headers:{
        "my-auth-token":this.authService.vratiToken()
      }
    }).subscribe(x=>{
    })
  }
  obrisiOpsegKorpe(){
    let korisnikID = this.authService.dohvatiAutorzacijskiToken()?.autentifikacijaToken.korisnikID;
    let url = MojConfig.adresa_servera + `/ObrisiKorpuKorisnika?KorisnikID=${korisnikID}`;

    this.httpClient.delete(url,{
      headers:{
        "my-auth-token":this.authService.vratiToken()
      }
    }).subscribe(x=>{

    })
  }
}
