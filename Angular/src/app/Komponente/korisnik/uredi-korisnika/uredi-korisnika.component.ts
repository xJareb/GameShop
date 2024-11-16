import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormsModule} from "@angular/forms";
import {NgIf} from "@angular/common";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../../moj-config";
import {MyAuthServiceService} from "../../../Servis/AuthService/my-auth-service.service";
import {UserUpdateRequest} from "../../../Servis/KorisnikService/user-update-request";

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

    @Input() name!:string;
    @Input() surname!:string;
    @Input() email!:string;
    @Output() open = new EventEmitter<boolean>();
    private show: boolean = true;

    public password:string="";
    public confirmedPassword:string="";

    public userUpdateRequest:UserUpdateRequest|null = null;
    transition: boolean = false;

    public buttonText:string = "Next";
    public condition:boolean = false;

    constructor(public httpClient: HttpClient, public authService: MyAuthServiceService) {
    }
    ngOnInit(): void {

    }
    close() {
    this.show = !this.show;
    this.open.emit(this.show);
  }

    UpdateUser() {
      if(this.condition == true) {
        this.userUpdateRequest = {
          userID: this.authService.userID(),
          name: this.name,
          surname: this.surname,
          email: this.email,
          password: this.password
        };

        let url = MojConfig.adresa_servera + `/UserUpdate`;

        if(this.userUpdateRequest.password != "" && this.confirmedPassword != "")
        {
          this.httpClient.put(url,this.userUpdateRequest,{
            headers:{
              "my-auth-token":this.authService.returnToken()
            }
          }).subscribe({
            next:(response) => {
              if(this.userUpdateRequest?.password == this.confirmedPassword)
              {
                this.close();
                window.location.reload();
              }
            },
            error:(err) =>{
              this.setStyle();
            }
          })
        }else{
          this.setStyle();
        }

      }
    }

  prepareToNextPage() {
    this.buttonText = "Update";
    this.condition = true;
  }
  setStyle(){
      let lozinkaInput = document.getElementById("password") as HTMLInputElement;
      let potlozinkaInput = document.getElementById("compassword") as HTMLInputElement;
      lozinkaInput.style.backgroundColor = 'red';
      potlozinkaInput.style.backgroundColor = 'red';
  }
}
