import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { BehaviorSubject } from 'rxjs';
import { AppMemoryStoreService } from '../common/app-memory-store';
import { BaseResponseModel } from '../common/base-response.model';
import { CartResponseModel, ServiceCategory } from '../common/service.model';
import { ApiUrl } from '../constants/api-url.enum';
import { Keys } from '../constants/keys.enum';
import { UserModel } from '../registration-login/registration-login.model';

@Component({
  selector: 'app-bids',
  templateUrl: './bids.component.html',
  styleUrls: ['./bids.component.css']
})
export class BidsComponent implements OnInit {

  items$: BehaviorSubject<BidModel[]> = new BehaviorSubject<BidModel[]>([]);
  loading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  constructor(private http: HttpClient, private store: AppMemoryStoreService, private service: MessageService) { }

  ngOnInit(): void {
    const userId = this.store.get<UserModel>(Keys.User)?.userID != null ? this.store.get<UserModel>(Keys.User)?.userID : 0;
    if (userId > 0) {
      this.http.get<BaseResponseModel<CartResponseModel[]>>(`${ApiUrl.Vendor}activebid?id=2`).subscribe(res => {
        console.log(res);
        if (res.statusCode === 0) {
          const items: BidModel[] = [];
          res.body.forEach(cart => {
            const bidModel: BidModel = {
              cartId: cart.cartId,
              serviceCategory: cart.serviceCategory,
              quantity: cart.quantity,
              date: cart.date,
              amount: cart.bids.length === 1 ? cart.bids[0].amount : 0,
              bidId: cart.bids.length === 1 ? cart.bids[0].bidId : 0,
              address: `${cart.addressLine1},${cart.addressLine2},${cart.city},${cart.state},${cart.pincode}`,
              name: cart.name
            };
            items.push(bidModel);
          });
          this.items$.next(items);
        } else {
          this.service.add({ severity: 'warn', detail: res.message });
        }
      });

    }
  }

  onUpdateBid(rowData: BidModel) {
    this.loading$.next(true);
    const user = this.store.get<UserModel>(Keys.User);
    if (user) {
      const requestModel: BidRequestModel = { amount: rowData.amount, bidId: rowData.bidId, cartId: rowData.cartId, vendorId: user.userID };
      this.http.put<BaseResponseModel<number>>(`${ApiUrl.Vendor}bid`, requestModel).subscribe(res => {
        this.loading$.next(false);
        if (res.statusCode === 0) {
          this.service.add({ severity: 'success', detail: res.message });
        } else {
          this.service.add({ severity: 'warn', detail: res.message });
        }
      });
    }

  }

}

class BidModel {
  cartId: number;
  serviceCategory: ServiceCategory;
  quantity: number;
  date: Date;
  amount: number;
  bidId: number;
  address: string;
  name: string;
}

class BidRequestModel {
  bidId: number;
  cartId: number;
  vendorId: number;
  amount: number;
}
