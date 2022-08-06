import { HttpClient } from '@angular/common/http';
import { ChangeDetectionStrategy, Component, Inject, OnInit } from '@angular/core';
import * as CryptoJS from 'crypto-js';
import { Message, MessageService } from 'primeng/api';
import { BehaviorSubject, Subject } from 'rxjs';
import { AppMemoryStoreService } from '../common/app-memory-store';
import { BaseResponseModel } from '../common/base-response.model';
import { ItemModel, ServiceCategory, ServiceModel } from '../common/service.model';
import { ApiUrl } from '../constants/api-url.enum';
import { Keys } from '../constants/keys.enum';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HomeComponent implements OnInit {
  searchType: SearchType = SearchType.StarRating;
  type = SearchType;
  cart: ServiceCategory[] = [];
  loading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  messages$: Subject<Message> = new Subject<Message>();
  constructor(private http: HttpClient, private store: AppMemoryStoreService, private service: MessageService) {
  }
  services$: BehaviorSubject<ServiceCategory[]> = new BehaviorSubject<ServiceCategory[]>([]);

  ngOnInit(): void {
    this.loading$.next(true);
    const cart = this.store.get<ServiceCategory[]>(Keys.Cart);
    this.http.get<BaseResponseModel<ServiceCategory[]>>(ApiUrl.Service).subscribe(res => {
      this.loading$.next(false);
      if (res.statusCode === 0) {
        const servies: ServiceCategory[] = res.body;
        let cartDuplicate: ServiceCategory[] = [];
        servies.forEach(ser => {
          if (cart) {
            const item = cart.find(crt => crt.serviceCategoryId === ser.serviceCategoryId);
            if (item) {
              cartDuplicate.push(item);
              ser.quantity = item.quantity;
            } else {
              ser.quantity = 0;
            }
          } else {
            ser.quantity = 0;
          }
        });
        this.cart = cartDuplicate;
        this.store.add(Keys.Cart, this.cart);
        this.services$.next(servies);
      }
    }, err => {
      this.loading$.next(false);
    })
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
