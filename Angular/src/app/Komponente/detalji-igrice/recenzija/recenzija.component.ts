import {Component, EventEmitter, Input, input, Output} from '@angular/core';
import {NgbRatingModule} from "@ng-bootstrap/ng-bootstrap";
import {FormsModule} from "@angular/forms";
import {BrowserModule} from "@angular/platform-browser";
import {HttpClient} from "@angular/common/http";
import {MyAuthServiceService} from "../../../Servis/AuthService/my-auth-service.service";
import {MojConfig} from "../../../moj-config";
import {ReviewRequest} from "../../../Servis/RecenzijeService/review-request";

@Component({
  selector: 'app-recenzija',
  standalone: true,
  imports: [NgbRatingModule, FormsModule],
  templateUrl: './recenzija.component.html',
  styleUrl: './recenzija.component.css'
})
export class RecenzijaComponent {
  rating = 1;
  public show:boolean = true;
  @Input() gameID:any;
  @Output() open = new EventEmitter<boolean>();
  public reviewRequest:ReviewRequest | null = null;

  constructor(public httpClient:HttpClient, public authService:MyAuthServiceService) {
  }

  leaveReview() {
    let content = document.getElementById('message-text') as HTMLInputElement;
    let korisnikID = this.authService.handleAuthToken()?.autentifikacijaToken.korisnikID;
    let url = MojConfig.adresa_servera + `/ReviewAdd`

    this.reviewRequest = {
      userID:korisnikID,
      gameID:this.gameID,
      content:content.value,
      grade:this.rating
    }
    if(this.reviewRequest.content != ""){
    this.httpClient.post(url,this.reviewRequest,{
      headers:{
        "my-auth-token":this.authService.returnToken()
      }
    }).subscribe(x=>{
      this.close();
      window.location.reload();
    })
  }
  }

  close() {
    this.show = !this.show;
    this.open.emit(this.show);
  }
}
