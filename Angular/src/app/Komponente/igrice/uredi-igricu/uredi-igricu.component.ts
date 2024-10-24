import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {DetaljiIgrice} from "../../../Servis/DetaljiIgriceService/detalji-igrice";
import { FormsModule } from '@angular/forms';
import {MojConfig} from "../../../moj-config";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {Zanr} from "../../../Servis/ZanrService/zanr";
import {NgForOf} from "@angular/common";
import {UrediIgricu} from "../../../Servis/IgriceService/uredi-igricu";


@Component({
  selector: 'app-uredi-igricu',
  standalone: true,
  imports: [FormsModule, HttpClientModule, NgForOf],
  templateUrl: './uredi-igricu.component.html',
  styleUrl: './uredi-igricu.component.css'
})
export class UrediIgricuComponent implements OnInit{

  @Input() igricaID!:number;
  @Input() naziv!:string;
  @Input() zanrID!:number;
  @Input() datumIzlaska!:Date;
  @Input() slika!:string;
  @Input() izdavac!:string;
  @Input() opis!:string;
  @Input() cijena!:number;
  @Input() postotakAkcije!:number;
  @Output() otvori = new EventEmitter<boolean>();
  prikaz:boolean = true;

  listaZanrova:any;
  public odabranaIgrica:UrediIgricu | null = null;

  ngOnInit(): void {
    this.izlistajZanrove();

    /*const privremeniDatum = this.datumIzlaska;
    const noviDatum = new Date(privremeniDatum);
    this.datumIzlaska = noviDatum;
    */

  }
  constructor(public httpClient:HttpClient) {
  }
  izlistajZanrove(){
    let url = MojConfig.adresa_servera + `/Izlistaj`;

    this.httpClient.get<Zanr>(url).subscribe(x=>{
        this.listaZanrova = x.zanrovi;
    })
  }

  sacuvajPromjene() {
    let url = MojConfig.adresa_servera + `/AzurirajIgricu`;

    this.odabranaIgrica = {
      igricaID: this.igricaID,
      naziv: this.naziv,
      zanrID: this.zanrID,
      datumIzlaska: this.datumIzlaska,
      slika: this.slika,
      izdavac: this.izdavac,
      opis: this.opis,
      cijena: this.cijena,
      postotakAkcije: this.postotakAkcije
    }

    this.httpClient.put(url,this.odabranaIgrica).subscribe(x=>{
      this.zatvori();
      window.location.reload();
    })
  }

  zatvori() {
    this.prikaz = !this.prikaz;
    this.otvori.emit(this.prikaz);
  }
}
