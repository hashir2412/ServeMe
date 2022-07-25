import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSliderModule } from '@angular/material/slider';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { LoginComponent } from './registration-login/login/login.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatRadioModule } from '@angular/material/radio';
import { RegistrationLoginComponent } from './registration-login/registration-login.component';
import { LoadingComponent } from './common/loading/loading.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AppInterceptorInterceptor as AppInterceptor } from './app-interceptor.interceptor';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { MessageComponent } from './common/message/message.component';
import { MatMenuModule } from '@angular/material/menu';
import { AppMemoryStoreService } from './common/app-memory-store';
import { ProfileComponent } from './profile/profile.component';
import { ProfileGuard } from './profile.guard';
import { VendorProfileComponent } from './profile/vendor-profile/vendor-profile.component';
import { CustomerProfileComponent } from './profile/customer-profile/customer-profile.component';
import { TableModule } from 'primeng/table';
import { MatCardModule } from '@angular/material/card';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    RegistrationLoginComponent,
    LoadingComponent,
    MessageComponent,
    ProfileComponent,
    CustomerProfileComponent,
    VendorProfileComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: HomeComponent, },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'profile', component: ProfileComponent, canActivate: [ProfileGuard] }
    ], { relativeLinkResolution: 'legacy', useHash: true }),
    BrowserAnimationsModule,
    MatFormFieldModule,
    MatSliderModule,
    MatInputModule,
    MatDialogModule,
    MatIconModule,
    MatTabsModule,
    MatButtonModule,
    MatCheckboxModule,
    FlexLayoutModule,
    MatRadioModule,
    MatProgressSpinnerModule,
    MessagesModule,
    MessageModule,
    MatMenuModule,
    TableModule,
    MatCardModule
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: AppInterceptor, multi: true }, AppMemoryStoreService],
  bootstrap: [AppComponent]
})
export class AppModule { }
