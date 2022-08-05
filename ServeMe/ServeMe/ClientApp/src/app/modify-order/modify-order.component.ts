import { HttpClient } from '@angular/common/http';
import { ChangeDetectionStrategy, Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Message } from 'primeng/api';
import { BehaviorSubject, Subject } from 'rxjs';
import { BaseResponseModel } from '../common/base-response.model';
import { ApiUrl } from '../constants/api-url.enum';

@Component({
  selector: 'app-modify-order',
  templateUrl: './modify-order.component.html',
  styleUrls: ['./modify-order.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ModifyOrderComponent implements OnInit {
  date: Date;

  messages$: Subject<Message> = new Subject<Message>();
  loading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  constructor(public dialogRef: MatDialogRef<ModifyOrderComponent>, private http: HttpClient, @Inject(MAT_DIALOG_DATA) public data: { date: Date, cartId: number }) { }

  ngOnInit(): void {
  }
  
  onModify() {
    this.loading$.next(true);
    const res = { cartId: this.data.cartId, dateTime: new Date(this.data.date) };
    this.http.post<BaseResponseModel<number>>(ApiUrl.ModifyOrder, res).subscribe(res => {
      this.loading$.next(false);
      if (res.statusCode === 0) {
        this.dialogRef.close(true);
      } else {
        this.messages$.next({ severity: 'warn', detail: res.message });
      }
    });
  }
}
