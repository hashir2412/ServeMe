import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AES } from 'crypto-ts';
import { Message } from 'primeng/api';
import { BehaviorSubject, Subject } from 'rxjs';
import { BaseResponseModel } from 'src/app/common/base-response.model';
import { ApiUrl } from 'src/app/constants/api-url.enum';
import { Keys } from 'src/app/constants/keys.enum';
import { UserType } from 'src/app/constants/user-type.enum';

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
  private key = '4512631236589784';

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }
  onLoginClick(form: NgForm) {
    if (form.valid) {
      let encrypted = AES.encrypt(this.password, this.key);
      this.http.get(`${ApiUrl.Login}?email=${this.email.toLowerCase()}&password=${encrypted}&isCustomer=${this.userType === UserType.Customer}`).subscribe(res => {
        console.log(res);
      })
      // if (this.userType === UserType.Customer) {
      //   this.loading$.next(true);
      //   this.http.get<BaseResponseModel<boolean>>(ApiUrl.User,).subscribe(
      //     res => {
      //       this.loading$.next(false);
      //       if (res.statusCode === 0) {
      //         this.messages$.next({ severity: 'success', summary: 'Success', detail: 'User successfully registered' });
      //         let user: UserRequestModel = {
      //           userID: res.body, point: 25, receiveCommunication: this.model.receiveCommunication
      //           , email: this.model.email, name: this.model.name, phone: this.model.phone
      //         }
      //         this.store.add<UserRequestModel>(Keys.User, user);
      //         localStorage.setItem(Keys.User, JSON.stringify(user));
      //       } else {
      //         this.messages$.next({ severity: 'error', summary: 'Error', detail: res.message });
      //       }
      //     }, err => {
      //       this.loading$.next(false);
      //       this.messages$.next({ severity: 'error', summary: 'Error', detail: err.message });
      //     }
      //   );
      // } else {
      //   const userModel: RegisterVendorRequestModel = {
      //     vendor: {
      //       receiveCommunication: this.model.receiveCommunication
      //       , email: this.model.email, name: this.model.name, phone: this.model.phone,
      //       address: this.model.address, agreement: this.model.agreement, totalEarnings: this.model.totalEarnings
      //     }, password: encrypted.toString()
      //   };
      //   this.loading$.next(true);
      //   this.http.post<BaseResponseModel<number>>(ApiUrl.Vendor, JSON.stringify(userModel)).subscribe(
      //     res => {
      //       this.loading$.next(false);
      //       if (res.statusCode === 0) {
      //         this.messages$.next({ severity: 'success', summary: 'Success', detail: 'Vendor successfully registered' });
      //         let vendor: VendorRequestModel = {
      //           vendorID: res.body, receiveCommunication: this.model.receiveCommunication
      //           , email: this.model.email, name: this.model.name, phone: this.model.phone,
      //           address: this.model.address, agreement: this.model.agreement,
      //           totalEarnings: this.model.totalEarnings
      //         }
      //         this.store.add<VendorRequestModel>(Keys.User, vendor);
      //         localStorage.setItem(Keys.User, JSON.stringify(vendor));
      //       } else {
      //         this.messages$.next({ severity: 'error', summary: 'Error', detail: res.message });
      //       }
      //     }, err => {
      //       this.loading$.next(false);
      //       this.messages$.next({ severity: 'error', summary: 'Error', detail: err.message });
      //     }
      //   );
      // }


    }
  }
  onCancel() {
    this.close.emit(true);
  }
}


