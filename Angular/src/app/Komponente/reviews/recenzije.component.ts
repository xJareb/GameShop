import {Component, OnInit} from '@angular/core';
import {NgbRating, NgbRatingModule} from "@ng-bootstrap/ng-bootstrap";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {NgForOf, NgIf} from "@angular/common";
import {Review, ReviewResponse} from "../../Servis/RecenzijeService/reviews-response";
import {MyAuthServiceService} from "../../Servis/AuthService/my-auth-service.service";

@Component({
  selector: 'app-reviews',
  standalone: true,
    imports: [
        NgbRating, NgbRatingModule, HttpClientModule, NgForOf, NgIf
    ],
  templateUrl: './recenzije.component.html',
  styleUrl: './recenzije.component.css'
})
export class RecenzijeComponent implements OnInit{

  public listOfReviews:Review[] = [];
  public checkGooglePhoto:boolean = false;
  public checkBytePhoto:boolean = false;
  public rating = 1;

  constructor(public httpClient: HttpClient, public authService:MyAuthServiceService) {
  }
  ngOnInit(): void {
    this.showReviews();
  }
  showReviews(){
    let url = MojConfig.adresa_servera + `/ReviewsGetBy`;

    this.httpClient.get<ReviewResponse>(url).subscribe((x:ReviewResponse)=>{
      this.listOfReviews = x.reviews;
    })
  }


}
