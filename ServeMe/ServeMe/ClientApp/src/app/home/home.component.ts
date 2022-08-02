import { HttpClient } from '@angular/common/http';
import { ChangeDetectionStrategy, Component, Inject, OnInit } from '@angular/core';
import * as CryptoJS from 'crypto-js';
import { Message } from 'primeng/api';
import { BehaviorSubject, Subject } from 'rxjs';
import { AppMemoryStoreService } from '../common/app-memory-store';
import { BaseResponseModel } from '../common/base-response.model';
import { ItemModel, ServiceModel } from '../common/service.model';
import { ApiUrl } from '../constants/api-url.enum';
import { Keys } from '../constants/keys.enum';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HomeComponent implements OnInit {
  cart: ItemModel[] = [];
  loading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  messages$: Subject<Message> = new Subject<Message>();
  constructor(private http: HttpClient, private store: AppMemoryStoreService) {
  }
  services$: BehaviorSubject<ServiceModel[]> = new BehaviorSubject<ServiceModel[]>([]);
  longText = `The Shiba Inu is the smallest of the six original and distinct spitz breeds of dog
  from Japan. A small, agile dog that copes very well with mountainous terrain, the Shiba Inu was
  originally bred for hunting.`;
  ngOnInit(): void {
    // this.loading$.next(true);
    this.services$.next([{
      "serviceID": 1,
      "rateType": 1,
      "rate": 12,
      "name": "Maths Professor",
      "vendorId": 1,
      "serviceCategoryId": 5
  },
  {
      "serviceID": 2,
      "rateType": 1,
      "rate": 15,
      "name": "Science Professor",
      "vendorId": 1,
      "serviceCategoryId": 5
  },
  {
      "serviceID": 3,
      "rateType": 1,
      "rate": 18,
      "name": "Physics Professor",
      "vendorId": 1,
      "serviceCategoryId": 5
  },
  {
      "serviceID": 4,
      "rateType": 1,
      "rate": 10,
      "name": "Chemistry Professor",
      "vendorId": 1,
      "serviceCategoryId": 5
  },
  {
      "serviceID": 5,
      "rateType": 1,
      "rate": 20,
      "name": "Biology Professor",
      "vendorId": 1,
      "serviceCategoryId": 5
  },
  {
    "serviceID": 3,
    "rateType": 1,
    "rate": 18,
    "name": "Physics Professor",
    "vendorId": 1,
    "serviceCategoryId": 5
},
{
    "serviceID": 4,
    "rateType": 1,
    "rate": 10,
    "name": "Chemistry Professor",
    "vendorId": 1,
    "serviceCategoryId": 5
},
{
    "serviceID": 5,
    "rateType": 1,
    "rate": 20,
    "name": "Biology Professor",
    "vendorId": 1,
    "serviceCategoryId": 5
}]);
    // this.http.get<BaseResponseModel<ServiceModel[]>>(ApiUrl.Service).subscribe(res => {
    //   this.loading$.next(false);
    //   if (res.statusCode === 0) {
    //     this.services$.next(res.body);
    //   }
    // }, err => {
    //   this.loading$.next(false);
    // })
  }

  onAddToCart(service: ServiceModel) {
    this.cart.push({ service: service, date: new Date(new Date().setDate(new Date().getDate() + 1)), quantity: 1, rate: service.rate });
    this.store.add(Keys.Cart,this.cart);
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
