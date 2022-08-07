import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { BehaviorSubject } from 'rxjs';
import { AppMemoryStoreService } from '../common/app-memory-store';
import { Keys } from '../constants/keys.enum';
import { RegistrationLoginComponent } from '../registration-login/registration-login.component';
import { UserModel } from '../registration-login/registration-login.model';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  /**
   *
   */
  user$: BehaviorSubject<UserModel> = new BehaviorSubject<UserModel>(null);
  constructor(public dialog: MatDialog, private store: AppMemoryStoreService, private service: MessageService, private route: Router) {

  }
  ngOnInit(): void {
    this.store.observe<UserModel>(Keys.User).subscribe(res => {
      this.user$.next(res);
      console.log(`hello `);
      console.log(res);
    })
  }
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(RegistrationLoginComponent, {
      width: '490px',
      height: '400px'
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  onLogOut() {
    this.store.add(Keys.User, null);
    localStorage.removeItem(Keys.User);
    this.service.add({ severity: 'success', detail: 'Logout Successful' });
    this.route.navigateByUrl('/home');
  }
}
