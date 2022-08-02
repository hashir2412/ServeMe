import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Message } from 'primeng/api';
import { BehaviorSubject, Subject } from 'rxjs';
import { AppMemoryStoreService } from '../common/app-memory-store';
import { ApiUrl } from '../constants/api-url.enum';
import { Keys } from '../constants/keys.enum';
import { UserModel } from '../registration-login/registration-login.model';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
  loading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  messages$: Subject<Message> = new Subject<Message>();
  constructor(private http: HttpClient, private store: AppMemoryStoreService) { }

  ngOnInit(): void {
    const userId = this.store.get<UserModel>(Keys.User)?.userID != null ? this.store.get<UserModel>(Keys.User)?.userID : 0;
    console.log(this.store.get<UserModel>(Keys.User));
    this.http.get(`${ApiUrl.Order}?id=${userId}`).subscribe(res => {
      console.log(res);
    });
  }

  onPlaceReview() {
    this.loading$.next(true);
    const res: ReviewRequestModel = {
      comment: 'good',
      cartId: 4,
      serviceId: 2,
      stars: 3,
      userId: 2
    }
    this.http.post(ApiUrl.Review, res).subscribe(res => {
      this.loading$.next(false);
      console.log(res);
    });
  }

}

class ReviewRequestModel {
  serviceId: number;
  cartId: number;
  userId: number;
  comment: string;
  stars: number;
}