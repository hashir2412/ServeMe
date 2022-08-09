import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { BaseResponseModel } from '../common/base-response.model';
import { ApiUrl } from '../constants/api-url.enum';
import { SearchType } from '../home/home.component';
import { VendorRequestModel } from '../registration-login/registration-login.model';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  searchType: SearchType = SearchType.StarRating;
  type = SearchType;
  searchText = '';
  loading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  vendors$: BehaviorSubject<VendorReviewRatingModel[]> = new BehaviorSubject<VendorReviewRatingModel[]>([]);
  performSearch() {
    let vendors: VendorReviewRatingModel[] = [];
    if (this.searchType === SearchType.StarRating) {
      vendors = this.vendors$.value.filter(v => v.stars.toString() === this.searchText);
    } else if (this.searchType === SearchType.Address) {
      vendors = this.vendors$.value.filter(v => v.address.toLowerCase().includes(this.searchText.toLowerCase()));
    } else {
      vendors = this.vendors$.value.filter(v => v.name.toLowerCase().includes(this.searchText.toLowerCase()));
    }
    this.vendors$.next(vendors);
  }
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.loading$.next(true);
    this.http.get<BaseResponseModel<VendorReviewRatingModel[]>>(ApiUrl.Vendor).subscribe(res => {
      this.loading$.next(false);
      if (res.statusCode === 0) {
        this.vendors$.next(res.body);
      }
    });
  }

}
class VendorReviewRatingModel extends VendorRequestModel {
  stars: number;
}