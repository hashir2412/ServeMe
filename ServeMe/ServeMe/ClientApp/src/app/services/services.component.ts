import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Message, MessageService } from 'primeng/api';
import { BehaviorSubject, Subject } from 'rxjs';
import { AppMemoryStoreService } from '../common/app-memory-store';
import { BaseResponseModel } from '../common/base-response.model';
import { ServiceCategory } from '../common/service.model';
import { ApiUrl } from '../constants/api-url.enum';
import { Keys } from '../constants/keys.enum';

@Component({
  selector: 'app-services',
  templateUrl: './services.component.html',
  styleUrls: ['./services.component.css']
})
export class ServicesComponent implements OnInit {
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

}
