import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { UserModel } from 'src/app/registration-login/registration-login.model';

@Component({
  selector: 'app-customer-profile',
  templateUrl: './customer-profile.component.html',
  styleUrls: ['./customer-profile.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CustomerProfileComponent implements OnInit {

  model: UserModel = new UserModel();

  constructor() { }

  ngOnInit(): void {
  }

}
