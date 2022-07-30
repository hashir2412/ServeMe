import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Message } from 'primeng/api';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { AppMemoryStoreService } from '../common/app-memory-store';
import { ItemModel } from '../common/service.model';
import { ApiUrl } from '../constants/api-url.enum';
import { Keys } from '../constants/keys.enum';

@Component({
  selector: 'app-place-order',
  templateUrl: './place-order.component.html',
  styleUrls: ['./place-order.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PlaceOrderComponent implements OnInit {
  loading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  messages$: Subject<Message> = new Subject<Message>();
  items$: Observable<ItemModel[]> = new Observable<ItemModel[]>();
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  thirdFormGroup: FormGroup;
  isEditable = false;
  paymentType = 'cash';
  model: PlaceOrderRequestModel = new PlaceOrderRequestModel();
  constructor(private store: AppMemoryStoreService, private _formBuilder: FormBuilder, private http: HttpClient) { }

  ngOnInit(): void {
    this.items$ = this.store.observe<ItemModel[]>(Keys.Cart);
    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: ['', Validators.required],
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
    const res: PlaceOrderRequestModel = {
      address: '21', items: [{
        date: new Date(), quantity: 1, rate: 12, service: {
          name: 'Maths Professor', rate: 12, rateType: 1, serviceCategoryId: 5, serviceID: 1, vendorId: 1
        }
      }], paymentType: 'cash', total: 12, email: 'z@z.com', userId: 0
    }
    this.http.post(ApiUrl.Order, res).subscribe(res => {
      this.loading$.next(false);
      console.log(res);
    });
  }
}


class PlaceOrderRequestModel {
  items: ItemModel[];
  paymentType: string;
  total: number;
  address: string;
  userId: number;
  name?: string;
  email: string;
}