import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormsModule} from "@angular/forms";
import {NgIf} from "@angular/common";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {NoviAzuriraniKorisnik} from "../../../Servis/KorisnikService/novi-azurirani-korisnik";
import {MojConfig} from "../../../moj-config";

@Component({
  selector: 'app-uredi-korisnika',
  standalone: true,
  imports: [
    FormsModule,
    NgIf,
    HttpClientModule,
  ],
  templateUrl: './uredi-korisnika.component.html',
  styleUrl: './uredi-korisnika.component.css'
})
export class UrediKorisnikaComponent implements OnInit{

    @Input() ime!:string;
    @Input() prezime!:string;
    @Input() email!:string;
    @Output() otvori = new EventEmitter<boolean>();
    private prikaz: boolean = true;

    public lozinka:string="";
    public ponovljenaLozinka:string="";

    public azuriraniKorisnik:NoviAzuriraniKorisnik|null = null;
    tranzicija: boolean = false;

    public nazivDugmeta:string = "Nastavi";
    public uslov:boolean = false;

    constructor(public httpClient: HttpClient) {
    }
    ngOnInit(): void {

    }
    zatvori() {
    this.prikaz = !this.prikaz;
    this.otvori.emit(this.prikaz);
  }

    azuriraj() {
      if(this.uslov == true) {
        this.azuriraniKorisnik = {
          korisnikID: this.dohvatiKorisnika().autentifikacijaToken.korisnickiNalog.id,
          ime: this.ime,
          prezime: this.prezime,
          email: this.email,
          lozinka: this.lozinka
        };
        console.log(this.azuriraniKorisnik);

        let url = MojConfig.adresa_servera + `/AzurirajKorisnika`;

        if(this.azuriraniKorisnik.lozinka != "" && this.ponovljenaLozinka != "")
        {
          this.httpClient.put(url,this.azuriraniKorisnik).subscribe({
            next:(response) => {
              if(this.azuriraniKorisnik?.lozinka == this.ponovljenaLozinka)
              {
                alert('Korisnik uspjesno azuriran');
                this.zatvori();
                window.location.reload();
              }

            },
            error:(err) =>{
              alert('Neispravna lozinka')
            }
          })
        }else{
          alert('Sva polja su obavezna')
        }

      }
    }
    dohvatiKorisnika(){
      return JSON.parse(window.localStorage.getItem("korisnik")??"");
    }

  priprema() {
    this.nazivDugmeta = "Azuriraj";
    this.uslov = true;
  }
}
