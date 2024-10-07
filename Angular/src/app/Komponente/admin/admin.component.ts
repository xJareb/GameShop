import {Component, OnInit} from '@angular/core';
import {RouterLink} from "@angular/router";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {Korisnik, LogiraniKorisnik} from "../korisnik/logirani-korisnik";
import {Korisnici, ListaKorisnika} from "./lista-korisnika";
import {NgForOf} from "@angular/common";

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [
    RouterLink,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgForOf
  ],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent implements OnInit{

    listaKorisnika:Korisnici[] = [];
    constructor(public httpClient:HttpClient) {
    }
    ngOnInit(): void {
      this.izlistajSveKorisnike();

    }
    izlistajSveKorisnike(){
        let url = MojConfig.adresa_servera + `/PregledSvih`;

        this.httpClient.get<ListaKorisnika>(url).subscribe((x:ListaKorisnika)=>{
              this.listaKorisnika = x.korisnici;
              console.log(this.listaKorisnika)
        })
    }

  obrisiKorisnika(lk: Korisnici) {
    let id = lk.id;

    let url = MojConfig.adresa_servera + `/ObrisiKorisnika?ID=${id}`;
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
}
