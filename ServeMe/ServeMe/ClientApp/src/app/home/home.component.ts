import { HttpClient } from '@angular/common/http';
import { ChangeDetectionStrategy, Component, Inject, OnInit } from '@angular/core';
import * as CryptoJS from 'crypto-js';
import { Message, MessageService } from 'primeng/api';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { AppMemoryStoreService } from '../common/app-memory-store';
import { BaseResponseModel } from '../common/base-response.model';
import { ItemModel, ServiceCategory, ServiceModel } from '../common/service.model';
import { ApiUrl } from '../constants/api-url.enum';
import { Keys } from '../constants/keys.enum';
import { UserModel } from '../registration-login/registration-login.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HomeComponent implements OnInit {
  /**
   *
   */
  constructor(private store: AppMemoryStoreService) {

  }
  ngOnInit(): void {
    this.user$ = this.store.observe<UserModel>(Keys.User);
  }
  user$: Observable<UserModel>;


  // Declare this key and iv values in declaration
  private key = '4512631236589784';
  private iv = '4512631236589784';

  // // Methods for the encrypt and decrypt Using AES
  // encryptUsingAES256() {
  //   var encrypted = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(JSON.stringify("Your Json Object data or string")), this.key, {
  //     keySize: 128 / 8,
  //     iv: this.iv,
  //     mode: CryptoJS.mode.CBC,
  //     padding: CryptoJS.pad.Pkcs7
  //   });
  //   console.log('Encrypted :' + encrypted);
  //   this.http.get<boolean>(`api/login?email=asd&password=${encrypted}`).subscribe(result => {
  //     console.log(result);
  //   }, error => console.error(error));
  //   return encrypted;
  // }

  // generate() {
  //   var encrypted = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(JSON.stringify("Your Json Object data or string")), this.key, {
  //     keySize: 128 / 8,
  //     iv: this.iv,
  //     mode: CryptoJS.mode.CBC,
  //     padding: CryptoJS.pad.Pkcs7
  //   });
  //   console.log('Encrypted :' + encrypted);
  // }
}
export enum SearchType {
  StarRating = 1,
  Address = 2,
  Name = 3
}
