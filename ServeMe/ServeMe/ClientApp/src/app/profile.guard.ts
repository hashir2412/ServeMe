import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AppMemoryStoreService } from './common/app-memory-store';
import { Keys } from './constants/keys.enum';
import { UserModel } from './registration-login/registration-login.model';

@Injectable({
  providedIn: 'root'
})
export class ProfileGuard implements CanActivate {
  /**
   *
   */
  constructor(private store: AppMemoryStoreService) {

  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.store.get<UserModel>(Keys.User) != null;
  }

}
