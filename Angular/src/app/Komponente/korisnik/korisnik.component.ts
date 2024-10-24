import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {FormsModule} from "@angular/forms";
import {CommonModule, NgForOf, NgIf} from "@angular/common";
import {Korisnik, LogiraniKorisnik} from "./logirani-korisnik";
import {DetaljiIgrice} from "../../Servis/DetaljiIgriceService/detalji-igrice";
import {Router, RouterLink} from "@angular/router";
import {UrediKorisnikaComponent} from "./uredi-korisnika/uredi-korisnika.component";
import {AzurirajKorisnika} from "./azuriraj-korisnika";
import {MyAuthServiceService} from "../../Servis/AuthService/my-auth-service.service";

@Component({
  selector: 'app-korisnik',
  standalone: true,
  imports: [HttpClientModule, FormsModule, NgForOf, NgIf, RouterLink, UrediKorisnikaComponent],
  templateUrl: './korisnik.component.html',
  styleUrl: './korisnik.component.css'
})
export class KorisnikComponent implements OnInit{

    public podaciLogKorisnik:Korisnik [] = [];
    public pripremljeniPodaci:AzurirajKorisnika | null = null;
    public prikazUredi: boolean = false;

    public selectedFile:File |null = null;

    constructor(public httpClient:HttpClient,public authService:MyAuthServiceService,public router:Router) {
    }
    ngOnInit(): void {
        if(!this.authService.jelKorisnik()){
          this.router.navigate(["/"]);
        }
        else{
          let id = this.authService.dohvatiAutorzacijskiToken()?.autentifikacijaToken.korisnikID;
          let url = MojConfig.adresa_servera + `/PregledLog?LogiraniKorisnikID=${id}`;

          this.httpClient.get<LogiraniKorisnik>(url).subscribe((x:LogiraniKorisnik)=>{
            this.podaciLogKorisnik = x.korisnik;
          })
        }
    }
  pripremiPodatke(k: Korisnik) {
      this.pripremljeniPodaci = {
        ime: k.ime,
        prezime: k.prezime,
        email: k.email
      }
  }
  otvaranjeUredi($event : boolean)
  {
    this.prikazUredi = $event;
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

}
