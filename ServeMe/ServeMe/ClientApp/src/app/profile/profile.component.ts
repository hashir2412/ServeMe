import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { AppMemoryStoreService } from '../common/app-memory-store';
import { Keys } from '../constants/keys.enum';
import { UserModel } from '../registration-login/registration-login.model';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProfileComponent implements OnInit {

  user$: Observable<UserModel>;

  constructor(private store: AppMemoryStoreService) { }

  ngOnInit(): void {
    this.user$ = this.store.observe<UserModel>(Keys.User);
  }

}
