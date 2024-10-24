import {Component, OnInit} from '@angular/core';
import {Router, RouterLink} from "@angular/router";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {Korisnik, LogiraniKorisnik} from "../korisnik/logirani-korisnik";
import {Korisnici, ListaKorisnika} from "./lista-korisnika";
import {NgForOf, NgIf} from "@angular/common";
import {Admin, LogiraniAdmin} from "./logirani-admin";
import {MyAuthServiceService} from "../../Servis/AuthService/my-auth-service.service";

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [
    RouterLink,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgForOf,
    NgIf
  ],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent implements OnInit{

    listaKorisnika:Korisnici[] = [];
    crnaLista:Korisnici[] = [];
    adminPodaci: Admin[] = [];

    public selectedFile:File |null = null;

    constructor(public httpClient:HttpClient,public authService:MyAuthServiceService,public router:Router) {
    }
    ngOnInit(): void {
      if(!this.authService.jelAdmin()){
        this.router.navigate(["/"])
      }else{
        this.izlistajSveKorisnike();
        this.izlistajCrnuListu();
        this.izlistajAdmina();
      }

    }
    izlistajAdmina(){
      let id = this.dohvatiKorisnika().autentifikacijaToken.korisnickiNalog.id;
      let url = MojConfig.adresa_servera + `/PretraziAdmina?ID=${id}`;

      this.httpClient.get<LogiraniAdmin>(url).subscribe((x:LogiraniAdmin) => {
        this.adminPodaci = x.admin;
      })
    }
    izlistajSveKorisnike(){
        let url = MojConfig.adresa_servera + `/PregledSvih?isBlackList=false`;

        this.httpClient.get<ListaKorisnika>(url).subscribe((x:ListaKorisnika)=>{
              this.listaKorisnika = x.korisnici;
        })
    }
    izlistajCrnuListu(){
      let url = MojConfig.adresa_servera + `/PregledSvih?isBlackList=true`;

      this.httpClient.get<ListaKorisnika>(url).subscribe((x:ListaKorisnika)=>{
        this.crnaLista = x.korisnici;
      })
    }
  obrisiKorisnika(lk: Korisnici) {
    let id = lk.id;

    let url = MojConfig.adresa_servera + `/ObrisiKorisnika?ID=${id}&isBlackList=false`;
    this.httpClient.put(url,{}).subscribe({
      next:(response)=> {
        alert('Uspješno obrisan korisnik');
        window.location.reload();
      },
      error:(error)=>{
        alert('Došlo je do pogreške');
      }
      })
  }

  crnaListaKorisnika(lk: Korisnici) {
    let id = lk.id;
    let url = MojConfig.adresa_servera + `/ObrisiKorisnika?ID=${id}&isBlackList=true`;

    this.httpClient.put(url,{}).subscribe({
      next:(response)=>{
        alert('Uspješno blacklistan korisnik');
        window.location.reload();
      },
      error:(error)=>{
        alert('Došlo je do pogreske');
      }
    })
  }

  onSubmit() {
    if (!this.selectedFile) {
      alert('Molimo odaberite fajl.');
      return;
    }
    const formData = new FormData();
    formData.append("slika", this.selectedFile);

    let slikaUrl = MojConfig.adresa_servera + `/Slika`;

    this.httpClient.put(slikaUrl,formData).subscribe({
      next:(response) => {
        alert('Uspjesan upload slike')
        window.location.reload();
      },
      error: (error) => {
        alert('error')
      }
    })
  }
  onFileSelected($event: Event) {
    const target = $event.target as HTMLInputElement;
    const file = target.files?.[0];
    if (file) {
      this.selectedFile = file;
    }
  }
  dohvatiKorisnika(){
    return JSON.parse(window.localStorage.getItem("korisnik")??"");
  }
}
