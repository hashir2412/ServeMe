import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Message, MessageService } from 'primeng/api';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { AppMemoryStoreService } from '../common/app-memory-store';
import { BaseResponseModel } from '../common/base-response.model';
import { OrderResponseModel, ServiceCategory } from '../common/service.model';
import { ApiUrl } from '../constants/api-url.enum';
import { Keys } from '../constants/keys.enum';
import { UserModel } from '../registration-login/registration-login.model';

@Component({
  selector: 'app-place-order',
  templateUrl: './place-order.component.html',
  styleUrls: ['./place-order.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PlaceOrderComponent implements OnInit {
  loading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  messages$: Subject<Message> = new Subject<Message>();
  items$: Observable<ServiceCategory[]> = new Observable<ServiceCategory[]>();
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  thirdFormGroup: FormGroup;
  showAddress = false;
  isEditable = false;
  paymentType = 'cash';
  cart: ServiceCategory[] = [];
  model: PlaceOrderRequestModel = new PlaceOrderRequestModel();
  tomorrowDate: Date = new Date((new Date()).getDate() + 1);
  constructor(private store: AppMemoryStoreService, private _formBuilder: FormBuilder, private http: HttpClient, private service: MessageService) { }

  ngOnInit(): void {
    const items = this.store.observe<ServiceCategory[]>(Keys.Cart);
    if (items) {
      this.items$ = items;
    }
    this.cart = this.store.get(Keys.Cart);
    this.firstFormGroup = this._formBuilder.group({
      name: ['', Validators.required],
      address_line1: ['', Validators.required],
      address_line2: [''],
      city: ['', Validators.required],
      state: ['', Validators.required],
      pincode: ['', Validators.required],
      email_address: ['', Validators.required],
      phone: ['', Validators.required],
    });
    this.secondFormGroup = this._formBuilder.group({
      secondCtrl: ['', Validators.required],
    });
    this.thirdFormGroup = this._formBuilder.group({
      thirdCtrl: ['', Validators.required],
    });
  }
  onPlaceOrder() {
    this.loading$.next(true);
    this.model.items = this.cart;
    const user = this.store.get<UserModel>(Keys.User);
    if (user) {
      this.model.userId = user.userID;
    }
    this.http.post<BaseResponseModel<number>>(ApiUrl.Order, this.model).subscribe(res => {
      this.loading$.next(false);
      if (res.statusCode === 0) {
        this.cart = [];
        this.store.add(Keys.Cart, this.cart);
        this.service.add({ severity: 'success', detail: 'Successfully Placed Service Request' });
      } else {
        this.service.add({ severity: 'warn', detail: res.message });
      }

    });
  }

  onAddToCart(service: ServiceCategory) {
    service.quantity = 1;
    service.date = new Date();
    this.cart.push(service);
    this.store.add(Keys.Cart, this.cart);
  }

  decreaseQuantity(service: ServiceCategory) {
    service.quantity--;
    if (service.quantity === 0) {
      const index = this.cart.findIndex(item => item.serviceCategoryId === service.serviceCategoryId);
      if (index > -1) {
        this.cart.splice(index, 1);
        this.store.add(Keys.Cart, this.cart);
      }
    }
  }


}

class PlaceOrderRequestModel {
  items: ServiceCategory[];
  total: number;
  addressLine1: string;
  addressLine2: string;
  city: string;
  state: string;
  pincode: string;
  phone: string;
  name: string;
  userId: number;
  email: string;
}

