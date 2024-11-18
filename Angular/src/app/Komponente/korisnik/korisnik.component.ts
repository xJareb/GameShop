import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {FormsModule} from "@angular/forms";
import {CommonModule, NgForOf, NgIf} from "@angular/common";
import {Router, RouterLink} from "@angular/router";
import {UrediKorisnikaComponent} from "./uredi-korisnika/uredi-korisnika.component";
import {MyAuthServiceService} from "../../Servis/AuthService/my-auth-service.service";
import {LoggedUser, User} from "../../Servis/KorisnikService/logged-user";
import {PrepareLoggedUser} from "../../Servis/KorisnikService/prepare-logged-user";

@Component({
  selector: 'app-korisnik',
  standalone: true,
  imports: [HttpClientModule, FormsModule, NgForOf, NgIf, RouterLink, UrediKorisnikaComponent],
  templateUrl: './korisnik.component.html',
  styleUrl: './korisnik.component.css'
})
export class KorisnikComponent implements OnInit{

    public dataLoggedUser:User[] = [];
    public preparedLoggedUserData:PrepareLoggedUser | null = null;
    public showDialogEdit: boolean = false;
    public emptyValue: string = "";

    public selectedFile:File |null = null;

    constructor(public httpClient:HttpClient,public authService:MyAuthServiceService,public router:Router) {
    }
    ngOnInit(): void {
        if(!this.authService.isUser()){
          this.router.navigate(["/"]);
        }
        else{
          this.listLoggedUser();
        }
      console.log(this.authService.isGoogleProvider())
    }
    listLoggedUser(){
      let id = this.authService.userID();
      let url = MojConfig.adresa_servera + `/GetLogged?LoggedUserID=${id}`;

      this.httpClient.get<LoggedUser>(url,{
        headers:{
          "my-auth-token": this.authService.returnToken()
        }
      }).subscribe((x:LoggedUser)=>{
        this.dataLoggedUser = x.user;
      })
    }
  prepareData(k: User) {
      this.preparedLoggedUserData = {
        name: k.name,
        surname: k.surname,
        email: k.email
      }

  }
  openEditDialog($event : boolean)
  {
    this.showDialogEdit = $event;
  }

  onSubmit() {
    if (!this.selectedFile) {
      alert('Please choose a file.');
      return;
    }
    const formData = new FormData();
    formData.append("photo", this.selectedFile);

    let photoUrl = MojConfig.adresa_servera + `/UserPhoto`;

    this.httpClient.put(photoUrl,formData).subscribe({
      next:(response) => {
        this.listLoggedUser();
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
