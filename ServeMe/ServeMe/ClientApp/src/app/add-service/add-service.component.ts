import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MessageService } from 'primeng/api';
import { BehaviorSubject } from 'rxjs';
import { BaseResponseModel } from '../common/base-response.model';
import { ServiceCategory } from '../common/service.model';
import { ApiUrl } from '../constants/api-url.enum';

@Component({
  selector: 'app-add-service',
  templateUrl: './add-service.component.html',
  styleUrls: ['./add-service.component.css']
})
export class AddServiceComponent implements OnInit {
  loading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  services$: BehaviorSubject<ServiceCategory[]> = new BehaviorSubject<ServiceCategory[]>([]);
  selectedService: ServiceCategory;
  constructor(private dialogRef: MatDialogRef<AddServiceComponent>, private http: HttpClient,
    @Inject(MAT_DIALOG_DATA) public data: ServiceCategory[]) { }

  ngOnInit(): void {
    this.loading$.next(true);
    this.http.get<BaseResponseModel<ServiceCategory[]>>(ApiUrl.Service).subscribe(res => {
      this.loading$.next(false);
      if (res.statusCode === 0) {

        this.services$.next(res.body);
      }
    }, err => {
    });
  }

  onAdd() {
    if (this.selectedService) {
      this.dialogRef.close(this.selectedService);
    }
  }

}
