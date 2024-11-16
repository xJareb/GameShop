import {Component, OnInit} from '@angular/core';
import {NgbRating, NgbRatingModule} from "@ng-bootstrap/ng-bootstrap";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {NgForOf} from "@angular/common";
import {Review, ReviewResponse} from "../../Servis/RecenzijeService/reviews-response";

@Component({
  selector: 'app-reviews',
  standalone: true,
  imports: [
    NgbRating, NgbRatingModule, HttpClientModule, NgForOf
  ],
  templateUrl: './recenzije.component.html',
  styleUrl: './recenzije.component.css'
})
export class RecenzijeComponent implements OnInit{

  public listOfReviews:Review[] = [];
  public rating = 1;

  constructor(public httpClient: HttpClient) {
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
