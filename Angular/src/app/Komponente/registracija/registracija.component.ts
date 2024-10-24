import {Component, input, OnInit} from '@angular/core';
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {Router, RouterLink} from "@angular/router";
import { NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {MyAuthServiceService} from "../../Servis/AuthService/my-auth-service.service";
import {RegistracijaRequest} from "../../Servis/RegistracijaService/registracija-request";

@Component({
  selector: 'app-registracija',
  standalone: true,
  imports: [
    NgOptimizedImage,
    RouterLink,
    NgbTooltipModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule
  ],
  templateUrl: './registracija.component.html',
  styleUrl: './registracija.component.css',
  host: { class: 'd-block' }
})
export class RegistracijaComponent implements OnInit {

  public noviKorisnik:RegistracijaRequest | null = null;

  userForm: FormGroup;

  constructor(public httpClient: HttpClient, private router: Router,public authService:MyAuthServiceService) {
    this.userForm = new FormGroup({
      ime: new FormControl("", [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])[A-Za-z]{4,}$/)]),
      prezime: new FormControl("", [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])[A-Za-z]{4,}$/)]),
      korisnickoIme: new FormControl("", [Validators.required, Validators.minLength(4)]),
      email: new FormControl("", [Validators.required, Validators.email]),
      datumRodjenja: new FormControl("", [Validators.required]),
      lozinka: new FormControl("", [Validators.required, Validators.pattern(/^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{7,}/)],)
    })
  }

  ngOnInit(): void {
    if(this.authService.jelLogiran()){
      this.router.navigate(['/']);
    }
  }

  kreirajKorisnika() {
    let url = MojConfig.adresa_servera + `/Dodaj`;
    this.noviKorisnik = {
      "ime": this.userForm.controls['ime'].value,
      "prezime": this.userForm.controls['prezime'].value,
      "korisnickoIme": this.userForm.controls['korisnickoIme'].value,
      "email": this.userForm.controls['email'].value,
      "datumRodjenja": this.userForm.controls['datumRodjenja'].value,
      "lozinka": this.userForm.controls['lozinka'].value
    }

    const validnaForma = this.userForm.valid;
    if(validnaForma){
      this.httpClient.post(url,this.noviKorisnik).subscribe(x=>{
        this.router.navigate(["/"]);
      })
    }else{
      alert('Provjerite ispravnost polja');
    }
  }

  postaviStil(kontrola:string){
    if (this.userForm.controls[kontrola].invalid && !this.userForm.controls[kontrola].untouched) {
      return {
        'background-color': 'red',
        'color': 'white'
      }
    } else {
      return {}
    }
  }
}
