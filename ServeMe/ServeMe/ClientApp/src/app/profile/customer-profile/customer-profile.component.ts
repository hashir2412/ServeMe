import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { AppMemoryStoreService } from 'src/app/common/app-memory-store';
import { Keys } from 'src/app/constants/keys.enum';
import { UserModel } from 'src/app/registration-login/registration-login.model';

@Component({
  selector: 'app-customer-profile',
  templateUrl: './customer-profile.component.html',
  styleUrls: ['./customer-profile.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CustomerProfileComponent implements OnInit {

  model: UserModel = new UserModel();
  constructor(private store: AppMemoryStoreService) { }

  ngOnInit(): void {
    const user = this.store.get<UserModel>(Keys.User);
    this.model = JSON.parse(JSON.stringify(user));
  }

}
