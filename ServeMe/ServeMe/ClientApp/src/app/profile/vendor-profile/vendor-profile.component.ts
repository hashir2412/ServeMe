import { HttpClient } from '@angular/common/http';
import { Component, ChangeDetectionStrategy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MessageService } from 'primeng/api';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { AddServiceComponent } from 'src/app/add-service/add-service.component';
import { AppMemoryStoreService } from 'src/app/common/app-memory-store';
import { BaseResponseModel } from 'src/app/common/base-response.model';
import { ServiceCategory, ServiceModel } from 'src/app/common/service.model';
import { ApiUrl } from 'src/app/constants/api-url.enum';
import { Keys } from 'src/app/constants/keys.enum';
import { UserModel } from 'src/app/registration-login/registration-login.model';
@Component({
  selector: 'app-vendor-profile',
  templateUrl: './vendor-profile.component.html',
  styleUrls: ['./vendor-profile.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class VendorProfileComponent {
  loading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  services$: BehaviorSubject<ServiceCategory[]> = new BehaviorSubject<ServiceCategory[]>([]);
  model: UserModel = new UserModel();

  constructor(private http: HttpClient, private store: AppMemoryStoreService, private dialog: MatDialog, private service: MessageService) {

  }
  ngOnInit(): void {
    this.getServices();
    const user = this.store.get<UserModel>(Keys.User);
    this.model = JSON.parse(JSON.stringify(user));
  }

  private getServices() {
    this.loading$.next(true);
    const userId = this.store.get<UserModel>(Keys.User)?.userID != null ? this.store.get<UserModel>(Keys.User)?.userID : 0;
    if (userId > 0) {
      this.http.get<BaseResponseModel<ServiceCategory[]>>(`${ApiUrl.Service}${userId}`).subscribe(res => {
        this.loading$.next(false);
        if (res.statusCode === 0) {
          console.log(res);
          this.services$.next(res.body);
        }
      });

    }
  }

  onAddService() {
    const dialogRef = this.dialog.open(AddServiceComponent, {
      width: '400px', data: this.services$.value
    });

    dialogRef.afterClosed().subscribe((result: ServiceCategory) => {
      if (result) {
        const userId = this.store.get<UserModel>(Keys.User)?.userID != null ? this.store.get<UserModel>(Keys.User)?.userID : 0;
        const serviceModel: ServiceModel = { serviceCategoryId: result.serviceCategoryId, vendorId: userId };
        this.http.post<BaseResponseModel<number>>(ApiUrl.Service, serviceModel).subscribe(res => {
          if (res.statusCode === 0) {
            this.service.add({ severity: 'success', detail: 'Successfully added service' });
            this.getServices();
          }
        });
      }
    });
  }


}
