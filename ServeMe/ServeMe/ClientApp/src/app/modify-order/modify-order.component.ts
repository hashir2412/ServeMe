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
  todayDate: string;
  showCancelModify = false;

  constructor(public dialogRef: MatDialogRef<ModifyOrderComponent>, private http: HttpClient, @Inject(MAT_DIALOG_DATA) public data: { date: Date, cartId: number }) { }

  private formatDate(nmbr: number): string {
    let date = nmbr + "";
    date = (date.length < 2) ? "0" + date : date;
    return date;
  }

  ngOnInit(): void {
    const month = this.formatDate(new Date().getMonth() + 1);
    const day = this.formatDate(new Date().getDate() + 1);
    const hours = this.formatDate(new Date().getHours() + 1);
    const minutes = this.formatDate(new Date().getMinutes() + 1);
    this.todayDate = new Date().getFullYear() + "-" + month + "-" + day + 'T' + hours + ':' + minutes;
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
