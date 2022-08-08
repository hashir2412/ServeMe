import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-reviews-ratings',
  templateUrl: './reviews-ratings.component.html',
  styleUrls: ['./reviews-ratings.component.css']
})
export class ReviewsRatingsComponent implements OnInit {
  stars: number[] = [1, 2, 3, 4, 5];
  selectedValue: number;
  comment = ''
  constructor() { }

  ngOnInit(): void {
  }

  countStar(star) {
    this.selectedValue = star;
    console.log('Value of star', star);
  }

}
