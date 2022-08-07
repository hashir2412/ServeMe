import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AppMemoryStoreService } from '../common/app-memory-store';
import { BaseResponseModel } from '../common/base-response.model';
import { CartResponseModel } from '../common/service.model';
import { ApiUrl } from '../constants/api-url.enum';
import { Keys } from '../constants/keys.enum';
import { UserModel } from '../registration-login/registration-login.model';

@Component({
  selector: 'app-bids',
  templateUrl: './bids.component.html',
  styleUrls: ['./bids.component.css']
})
export class BidsComponent implements OnInit {

  constructor(private http: HttpClient, private store: AppMemoryStoreService) { }

  ngOnInit(): void {
    const userId = this.store.get<UserModel>(Keys.User)?.userID != null ? this.store.get<UserModel>(Keys.User)?.userID : 0;
    if (userId > 0) {
      this.http.get<BaseResponseModel<CartResponseModel[]>>(`${ApiUrl.Vendor}activebid?id=2`).subscribe(res => {
        console.log(res);
      });

    }
  }

}
