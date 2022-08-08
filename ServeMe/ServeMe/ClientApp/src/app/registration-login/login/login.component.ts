import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { IDataStore, MemoryStore } from '@svaza/datastore';
import { AES } from 'crypto-ts';
import { Message, MessageService } from 'primeng/api';
import { BehaviorSubject, Subject } from 'rxjs';
import { AppMemoryStoreService } from 'src/app/common/app-memory-store';
import { BaseResponseModel } from 'src/app/common/base-response.model';
import { ApiUrl } from 'src/app/constants/api-url.enum';
import { Keys } from 'src/app/constants/keys.enum';
import { UserType } from 'src/app/constants/user-type.enum';
import { UserModel, UserRequestModel, VendorRequestModel } from '../registration-login.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  password = '';
  email = '';
  userType: UserType = UserType.Customer;
  type = UserType;
  @Output() close: EventEmitter<boolean> = new EventEmitter<boolean>();
  loading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  messages$: Subject<Message> = new Subject<Message>();

  constructor(private http: HttpClient, private store: AppMemoryStoreService) { }

  ngOnInit(): void {
  }
  onLoginClick(form: NgForm) {
    if (form.valid) {
      this.loading$.next(true);
      this.http.get(`${ApiUrl.Login}?email=${this.email.toLowerCase()}&password=${this.password}&isCustomer=${this.userType === UserType.Customer}`)
        .subscribe((res: BaseResponseModel<object>) => {
          console.log(res);
          this.loading$.next(false);
          if (res.statusCode === 0) {
            this.messages$.next({ severity: 'success', summary: 'Success', detail: 'Login Successful' });
            if (this.userType === UserType.Customer) {
              const customer = res.body as UserRequestModel;
              const final: UserModel = {
                email: customer.email, name: customer.name, phone: customer.phone, userID: customer.userId,
                receiveCommunication: customer.receiveCommunication, isCustomer: true, point: customer.point
              };
              this.store.add<UserModel>(Keys.User, final);
              localStorage.setItem(Keys.User, JSON.stringify(final));
            } else {
              const vendor = res.body as VendorRequestModel;
              const final: UserModel = {
                email: vendor.email, name: vendor.name, phone: vendor.phone, userID: vendor.vendorId,
                receiveCommunication: vendor.receiveCommunication, isCustomer: false, address: vendor.address,
                agreement: vendor.agreement, totalEarnings: vendor.totalEarnings
              };
              this.store.add<UserModel>(Keys.User, final);
              localStorage.setItem(Keys.User, JSON.stringify(final));
            }
            this.close.emit(true);
          } else {
            this.messages$.next({ severity: 'error', summary: 'Error', detail: res.message });
          }
        }, err => {
          this.loading$.next(false);
          this.messages$.next({ severity: 'error', summary: 'Error', detail: err.message });
        });
    }
  }
  onCancel() {
    this.close.emit(true);
  }
}


