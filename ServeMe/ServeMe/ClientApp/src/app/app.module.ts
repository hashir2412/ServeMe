import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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
import { PlaceOrderComponent } from './place-order/place-order.component';
import { LoginComponent } from './registration-login/login/login.component';
import { MatStepperModule } from '@angular/material/stepper';
import { OrdersComponent } from './orders/orders.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatDividerModule } from '@angular/material/divider';
import { CommonModule } from '@angular/common';
import { MatListModule } from '@angular/material/list';
import { ConfirmComponent } from './confirm/confirm.component';
import { ModifyOrderComponent } from './modify-order/modify-order.component';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { ToastModule } from 'primeng/toast';
import { MatToolbarModule } from '@angular/material/toolbar';
import { AddServiceComponent } from './add-service/add-service.component';
import { BidsComponent } from './bids/bids.component';
import { ChartModule } from 'primeng/chart';
import { ServicesComponent } from './services/services.component';
import { VendorStatsComponent } from './vendor-stats/vendor-stats.component';
import { SearchComponent } from './search/search.component';
import { VendorOrdersComponent } from './vendor-orders/vendor-orders.component';
import { MatChipsModule } from '@angular/material/chips';

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
    VendorProfileComponent,
    PlaceOrderComponent,
    OrdersComponent,
    ConfirmComponent,
    ModifyOrderComponent,
    AddServiceComponent,
    BidsComponent,
    ServicesComponent,
    VendorStatsComponent,
    SearchComponent,
    VendorOrdersComponent
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
      { path: 'profile', component: ProfileComponent },
      { path: 'cart', component: PlaceOrderComponent },
      { path: 'orders', component: OrdersComponent },
      { path: 'searchvendor', component: SearchComponent }
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
    MatCardModule,
    MatStepperModule,
    ReactiveFormsModule,
    MatExpansionModule,
    MatDividerModule,
    CommonModule,
    MatListModule,
    MatButtonToggleModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    ToastModule,
    MatToolbarModule,
    ChartModule,
    MatChipsModule
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: AppInterceptor, multi: true }, AppMemoryStoreService],
  bootstrap: [AppComponent]
})
export class AppModule { }
