import {Component, input, OnInit} from '@angular/core';
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {Router, RouterLink} from "@angular/router";
import { NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";

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

  userForm: FormGroup;

  constructor(public httpClient: HttpClient, private router: Router) {
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
  }

  kreirajKorisnika() {
    let url = MojConfig.adresa_servera + `/Dodaj`;
    let requestBody = {
      "ime": this.userForm.controls['ime'].value,
      "prezime": this.userForm.controls['prezime'].value,
      "korisnickoIme": this.userForm.controls['korisnickoIme'].value,
      "email": this.userForm.controls['email'].value,
      "datumRodjenja": this.userForm.controls['datumRodjenja'].value,
      "lozinka": this.userForm.controls['lozinka'].value
    }

    const validnaForma = this.userForm.valid;
    if(validnaForma){
      this.httpClient.post(url,requestBody).subscribe(x=>{
        this.router.navigate(["/"]);
      })
    }
  }

  postaviStilIme(){
    if (this.userForm.controls['ime'].invalid && !this.userForm.controls['ime'].untouched) {
      return {
        'background-color': 'red',
        'color': 'white'
      }
    } else {
      return {}
    }
  }
  postaviStilPrezime(){
    if (this.userForm.controls['prezime'].invalid && !this.userForm.controls['prezime'].untouched) {
      return {
        'background-color': 'red',
        'color': 'white'
      }
    } else {
      return {}
    }
  }
  postaviStilKorisnickoIme(){
    if (this.userForm.controls['korisnickoIme'].invalid && !this.userForm.controls['korisnickoIme'].untouched) {
      return {
        'background-color': 'red',
        'color': 'white'
      }
    } else {
      return {}
    }
  }
  postaviStilEmail(){
    if (this.userForm.controls['email'].invalid && !this.userForm.controls['email'].untouched) {
      return {
        'background-color': 'red',
        'color': 'white'
      }
    } else {
      return {}
    }
  }
  postaviStilDatumRodjenja(){
    if (this.userForm.controls['datumRodjenja'].invalid && !this.userForm.controls['datumRodjenja'].untouched) {
      return {
        'background-color': 'red',
        'color': 'white'
      }
    } else {
      return {}
    }
  }
  postaviStilLozinka(){
    if (this.userForm.controls['lozinka'].invalid && !this.userForm.controls['lozinka'].untouched) {
      return {
        'background-color': 'red',
        'color': 'white'
      }
    } else {
      return {}
    }
  }
}
