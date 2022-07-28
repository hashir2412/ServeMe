import { Component, OnInit } from '@angular/core';
import { IDataStore, MemoryStore } from '@svaza/datastore';
import { AppMemoryStoreService } from './common/app-memory-store';
import { Keys } from './constants/keys.enum';
import { UserModel } from './registration-login/registration-login.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit{
  /**
   *
   */
  constructor(private store: AppMemoryStoreService) {

  }
  ngOnInit(): void {
    const user = localStorage.getItem(Keys.User);
    if(user != null){
      this.store.add<UserModel>(Keys.User, <UserModel>JSON.parse(user));
      this.store.notify(Keys.User);
    }

  }
  title = 'app';

  
}
