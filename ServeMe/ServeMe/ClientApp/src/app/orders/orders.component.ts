import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Message, MessageService } from 'primeng/api';
import { BehaviorSubject, Subject } from 'rxjs';
import { AppMemoryStoreService } from '../common/app-memory-store';
import { BaseResponseModel } from '../common/base-response.model';
import { ServiceModel } from '../common/service.model';
import { ConfirmComponent } from '../confirm/confirm.component';
import { ApiUrl } from '../constants/api-url.enum';
import { Keys } from '../constants/keys.enum';
import { ModifyOrderComponent } from '../modify-order/modify-order.component';
import { UserModel } from '../registration-login/registration-login.model';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
  loading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  messages$: Subject<Message> = new Subject<Message>();
  private orders$: BehaviorSubject<OrderResponseModel[]> = new BehaviorSubject<OrderResponseModel[]>([]);
  currentOrders$: BehaviorSubject<OrderResponseModel[]> = new BehaviorSubject<OrderResponseModel[]>([]);
  cancelledOrders$: BehaviorSubject<OrderResponseModel[]> = new BehaviorSubject<OrderResponseModel[]>([]);
  pastOrders$: BehaviorSubject<OrderResponseModel[]> = new BehaviorSubject<OrderResponseModel[]>([]);
  constructor(private http: HttpClient, private store: AppMemoryStoreService, public dialog: MatDialog,
    private messageService: MessageService) { }

  ngOnInit(): void {
    this.getOrders();
  }

  private getOrders() {

    const userId = this.store.get<UserModel>(Keys.User)?.userID != null ? this.store.get<UserModel>(Keys.User)?.userID : 0;
    this.loading$.next(true);
    this.http.get<BaseResponseModel<OrderResponseModel[]>>(`${ApiUrl.Order}?id=${userId}`).subscribe(res => {
      this.loading$.next(false);
      if (res.statusCode === 0) {
        const currentOrders = res.body.filter(curr => curr.items.some(item => item.statusId === 1))
        this.currentOrders$.next(currentOrders);
        const cancelledOrders = res.body.filter(curr => curr.items.every(item => item.statusId === 3))
        this.cancelledOrders$.next(cancelledOrders);
        const pastOrders = res.body.filter(curr => curr.items.every(item => item.statusId === 2))
        this.pastOrders$.next(pastOrders);
      } else {
        this.messages$.next({ severity: 'warn', detail: res.message });
      }

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

  onCancel(item: CartResponseModel) {
    const dialogRef = this.dialog.open(ConfirmComponent, {
      width: '400px', data: { cartId: item.cartId }
    });

    dialogRef.afterClosed().subscribe((result: boolean) => {
      if (result) {
        this.getOrders();
      }
    });
  }

  onModify(item: CartResponseModel) {
    const dialogRef = this.dialog.open(ModifyOrderComponent, {
      width: '400px', data: { cartId: item.cartId, date: item.date }
    });

    dialogRef.afterClosed().subscribe((result: boolean) => {
      if (result) {
        this.messageService.clear();
        this.messageService.add({ severity: 'success', detail: 'Successfully Modified Service Request' });
        this.getOrders();
      }
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

class OrderResponseModel {
  id: number;
  address: string;
  date: Date;
  total: number;
  items: CartResponseModel[];
}

class CartResponseModel {
  cartId: number;
  quantity: number;
  name: string;
  rate: number;
  date: Date;
  statusId: number;
  service: ServiceModel;
}