import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {NgForOf, NgIf, NgStyle} from "@angular/common";
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {Route, Router, RouterLink} from "@angular/router";
import {MojConfig} from "../../../moj-config";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MyAuthServiceService} from "../../../Servis/my-auth-service.service";
import {KreditnaKarticaRequest} from "./kreditna-kartica-request";
import {KupovinaRequest} from "./kupovina-request";

@Component({
  selector: 'app-korpa-placanje',
  standalone: true,
  imports: [
    NgForOf,
    NgIf,
    ReactiveFormsModule,
    RouterLink,
    NgStyle,
    HttpClientModule
  ],
  templateUrl: './korpa-placanje.component.html',
  styleUrl: './korpa-placanje.component.css'
})
export class KorpaPlacanjeComponent implements OnInit{

    @Input() ukupno:number = 0;
    @Output() otvori = new EventEmitter<boolean>();
    private prikaz: boolean = true;

    public kreditnaKartica :KreditnaKarticaRequest | null = null;
    public korisnikKupovina: KupovinaRequest | null = null;
    ngBrojKartice:any;
    ngDatumIsteka:any;

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

    }
    zatvori() {
    this.prikaz = !this.prikaz;
    this.otvori.emit(this.prikaz);
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
        // TODO :: pozvati endpoint za spremanje kartice u bazu (sifrirano)
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

      this.httpClient.post(url,this.korisnikKupovina).subscribe(x=>{
        this.obrisiOpsegKorpe();
        this.route.navigate(['/']);
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

      this.httpClient.post(url,this.kreditnaKartica).subscribe(x=>{
        alert('Sacuvana kartica');
      })
  }
  obrisiOpsegKorpe(){
      let korisnikID = this.authService.dohvatiAutorzacijskiToken()?.autentifikacijaToken.korisnikID;
      let url = MojConfig.adresa_servera + `/ObrisiKorpuKorisnika?KorisnikID=${korisnikID}`;

      this.httpClient.delete(url).subscribe(x=>{

      })
  }
}
