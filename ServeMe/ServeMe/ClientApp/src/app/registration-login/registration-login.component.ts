import { HttpClient } from '@angular/common/http';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { UserType } from '../constants/user-type.enum';
import { RegisterUserRequestModel, RegisterVendorRequestModel, UserModel, UserRequestModel, VendorRequestModel } from './registration-login.model';
import { ApiUrl } from '../constants/api-url.enum';
import { BaseResponseModel } from '../common/base-response.model';
import { IDataStore, MemoryStore } from '@svaza/datastore';
import { Keys } from '../constants/keys.enum';
import { AES } from 'crypto-ts';
import { BehaviorSubject, Subject } from 'rxjs';
import { Message } from 'primeng/api';


@Component({
  selector: 'app-registration-login',
  templateUrl: './registration-login.component.html',
  styleUrls: ['./registration-login.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RegistrationLoginComponent implements OnInit {
  userType: UserType = UserType.Customer;
  type = UserType;
  model: UserModel = new UserModel();
  password = '';
  hide = true;
  loading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  messages$: Subject<Message> = new Subject<Message>();

  private store: IDataStore = new MemoryStore();

  private key = '4512631236589784';
  constructor(
    public dialogRef: MatDialogRef<RegistrationLoginComponent>, private http: HttpClient) { }

  ngOnInit(): void {
    var subscription = this.store.observe(Keys.User)
      .subscribe(data => {
        // handle the data change
        // ignore if data is undefined
        if (data === undefined) return;
      });
  }
  onRegisterClick(form: NgForm) {
    if (form.valid) {
      let encrypted = AES.encrypt(this.password, this.key);
      if (this.userType === UserType.Customer) {
        const userModel: RegisterUserRequestModel = {
          user: {
            userID: 0, point: 0, receiveCommunication: this.model.receiveCommunication
            , email: this.model.email.toLowerCase(), name: this.model.name, phone: this.model.phone
          }, password: encrypted.toString()
        };
        this.loading$.next(true);
        this.http.post<BaseResponseModel<number>>(ApiUrl.User, JSON.stringify(userModel)).subscribe(
          res => {
            this.loading$.next(false);
            if (res.statusCode === 0) {
              this.messages$.next({ severity: 'success', summary: 'Success', detail: 'User successfully registered' });
              let user: UserRequestModel = {
                userID: res.body, point: 25, receiveCommunication: this.model.receiveCommunication
                , email: this.model.email, name: this.model.name, phone: this.model.phone
              }
              this.store.add<UserRequestModel>(Keys.User, user);
              localStorage.setItem(Keys.User, JSON.stringify(user));
            } else {
              this.messages$.next({ severity: 'error', summary: 'Error', detail: res.message });
            }
          }, err => {
            this.loading$.next(false);
            this.messages$.next({ severity: 'error', summary: 'Error', detail: err.message });
          }
        );
      } else {
        const userModel: RegisterVendorRequestModel = {
          vendor: {
            receiveCommunication: this.model.receiveCommunication
            , email: this.model.email.toLowerCase(), name: this.model.name, phone: this.model.phone,
            address: this.model.address, agreement: this.model.agreement, totalEarnings: this.model.totalEarnings
          }, password: encrypted.toString()
        };
        this.loading$.next(true);
        this.http.post<BaseResponseModel<number>>(ApiUrl.Vendor, JSON.stringify(userModel)).subscribe(
          res => {
            this.loading$.next(false);
            if (res.statusCode === 0) {
              this.messages$.next({ severity: 'success', summary: 'Success', detail: 'Vendor successfully registered' });
              let vendor: VendorRequestModel = {
                vendorID: res.body, receiveCommunication: this.model.receiveCommunication
                , email: this.model.email, name: this.model.name, phone: this.model.phone,
                address: this.model.address, agreement: this.model.agreement,
                totalEarnings: this.model.totalEarnings
              }
              this.store.add<VendorRequestModel>(Keys.User, vendor);
              localStorage.setItem(Keys.User, JSON.stringify(vendor));
            } else {
              this.messages$.next({ severity: 'error', summary: 'Error', detail: res.message });
            }
          }, err => {
            this.loading$.next(false);
            this.messages$.next({ severity: 'error', summary: 'Error', detail: err.message });
          }
        );
      }


    }
  }

  onCancel() {
    this.dialogRef.close();
  }

}
