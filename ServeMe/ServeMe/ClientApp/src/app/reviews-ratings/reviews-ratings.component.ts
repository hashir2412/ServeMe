import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-reviews-ratings',
  templateUrl: './reviews-ratings.component.html',
  styleUrls: ['./reviews-ratings.component.css']
})
export class ReviewsRatingsComponent implements OnInit {
  stars: number[] = [1, 2, 3, 4, 5];
  selectedValue: number;
  comment = ''
  constructor(public dialogRef: MatDialogRef<ReviewsRatingsComponent>) { }

  ngOnInit(): void {
  }

  countStar(star) {
    this.selectedValue = star;
    console.log('Value of star', star);
  }

  onSubmit() {
    if (this.selectedValue) {
      const review: ReviewRatingModel = { comment: this.comment, stars: this.selectedValue };
      this.dialogRef.close(review);
    }
  }

}

export class ReviewRatingModel {
  stars: number;
  comment: string;
}
