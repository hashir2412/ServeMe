import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { BehaviorSubject } from 'rxjs';
import { AppMemoryStoreService } from '../common/app-memory-store';
import { BaseResponseModel } from '../common/base-response.model';
import { CartResponseModel } from '../common/service.model';
import { ApiUrl } from '../constants/api-url.enum';
import { Keys } from '../constants/keys.enum';
import { UserModel } from '../registration-login/registration-login.model';

@Component({
  selector: 'app-vendor-orders',
  templateUrl: './vendor-orders.component.html',
  styleUrls: ['./vendor-orders.component.css']
})
export class VendorOrdersComponent implements OnInit {

  currentItems$: BehaviorSubject<CartResponseModel[]> = new BehaviorSubject<CartResponseModel[]>([]);
  pastItems$: BehaviorSubject<CartResponseModel[]> = new BehaviorSubject<CartResponseModel[]>([]);
  cancelledItems$: BehaviorSubject<CartResponseModel[]> = new BehaviorSubject<CartResponseModel[]>([]);
  loading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  constructor(private http: HttpClient, private store: AppMemoryStoreService, private service: MessageService) { }

  ngOnInit(): void {
    this.getOrders();
  }

  private getOrders() {
    this.loading$.next(true);
    const userId = this.store.get<UserModel>(Keys.User)?.userID != null ? this.store.get<UserModel>(Keys.User)?.userID : 0;
    if (userId > 0) {
      this.http.get<BaseResponseModel<CartResponseModel[]>>(`${ApiUrl.Vendor}order?id=${userId}`).subscribe(res => {
        this.loading$.next(false);
        if (res.statusCode === 0) {
          const currentItems = res.body.filter(item => item.statusId === 2);
          this.currentItems$.next(currentItems);
          const pastItems = res.body.filter(item => item.statusId === 4);
          this.pastItems$.next(pastItems);
          const cancelledItems = res.body.filter(item => item.statusId === 3);
          this.cancelledItems$.next(cancelledItems);
        } else {
          this.service.add({ severity: 'warn', detail: res.message });
        }
      });
    }
  }

  onMarkComplete(item: CartResponseModel) {
    this.loading$.next(true);
    const userId = this.store.get<UserModel>(Keys.User)?.userID != null ? this.store.get<UserModel>(Keys.User)?.userID : 0;
    if (userId > 0) {
      this.http.post<BaseResponseModel<number>>(`${ApiUrl.Vendor}ordercomplete`, item).subscribe(res => {
        this.loading$.next(false);
        if (res.statusCode === 0) {
          this.service.add({ severity: 'success', detail: res.message });
          this.getOrders();
        } else {
          this.service.add({ severity: 'warn', detail: res.message });
        }
      });
    }
  }

}
