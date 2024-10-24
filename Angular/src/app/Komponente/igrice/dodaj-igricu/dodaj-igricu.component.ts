import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormsModule} from "@angular/forms";
import {NgForOf} from "@angular/common";
import {MojConfig} from "../../../moj-config";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {Zanr} from "../../../Servis/ZanrService/zanr";
import {DodajIgricu} from "../../../Servis/IgriceService/dodaj-igricu";

@Component({
  selector: 'app-dodaj-igricu',
  standalone: true,
  imports: [
    FormsModule,
    NgForOf,
    HttpClientModule
  ],
  templateUrl: './dodaj-igricu.component.html',
  styleUrl: './dodaj-igricu.component.css'
})
export class DodajIgricuComponent implements OnInit{

    public listaZanrova:any;
    public novaIgra:DodajIgricu | null  = null;

    public naziv: any;
    public zanrID: number = 1;
    public datumIzlaska:any;
    public slika:any;
    public izdavac:any;
    public opis:any;
    public cijena:any;
    public postotakAkcije:any;

    @Output() otvori = new EventEmitter<boolean>();
    public prikaz:boolean = true;

    ngOnInit(): void {
      this.izlistajZanrove();
    }
    constructor(public httpClient:HttpClient) {
    }

    izlistajZanrove(){
      let url = MojConfig.adresa_servera + `/Izlistaj`;

      this.httpClient.get<Zanr>(url).subscribe(x=>{
        this.listaZanrova = x.zanrovi;
      })
    }

  dodajIgricu() {
    this.novaIgra = {
      naziv: this.naziv,
      zanrID: this.zanrID,
      datumIzlaska: this.datumIzlaska,
      slika: this.slika,
      izdavac: this.izdavac,
      opis: this.opis,
      cijena: this.cijena,
      postotakAkcije: this.postotakAkcije,
    }

    let url = MojConfig.adresa_servera + `/DodajIgricu`;

    if(!this.provjeriIspravnostObjekta(this.novaIgra)){
      this.httpClient.post(url,this.novaIgra).subscribe(x=>{
        alert('Dodana nova igra');
        window.location.reload();
      })
    }else{
      alert('Provjerite ispravnost svih polja');
    }



  }

   provjeriIspravnostObjekta(obj: any): boolean {
    return Object.values(obj).some(value =>
      value === null || value === undefined || value === ''
    );
  }


  zatvori() {
    this.prikaz = !this.prikaz;
    this.otvori.emit(this.prikaz);
  }
}
