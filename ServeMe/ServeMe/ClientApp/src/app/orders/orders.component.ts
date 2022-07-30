import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
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

  constructor(private http: HttpClient, private store: AppMemoryStoreService) { }

  ngOnInit(): void {
    const userId = this.store.get<UserModel>(Keys.User)?.userID != null ? this.store.get<UserModel>(Keys.User)?.userID : 0;
    console.log(this.store.get<UserModel>(Keys.User));
    this.http.get(`${ApiUrl.Order}?id=${userId}`).subscribe(res => {
      console.log(res);
    });
  }

}
