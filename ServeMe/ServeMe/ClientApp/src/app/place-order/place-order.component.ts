import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Message } from 'primeng/api';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { AppMemoryStoreService } from '../common/app-memory-store';
import { ItemModel } from '../common/service.model';
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
    // this.http.post()
  }
}


class PlaceOrderRequestModel {
  items: ItemModel[];
  paymentType: string;
  total: number;
  address: string;
  userId?: number;
}