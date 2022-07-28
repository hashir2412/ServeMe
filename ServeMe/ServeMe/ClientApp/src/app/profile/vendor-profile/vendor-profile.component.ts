import { HttpClient } from '@angular/common/http';
import { Component, ChangeDetectionStrategy } from '@angular/core';
@Component({
  selector: 'app-vendor-profile',
  templateUrl: './vendor-profile.component.html',
  styleUrls: ['./vendor-profile.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class VendorProfileComponent {
  cars = [{ vin: 1, year: 2015, brand: 'Honda', color: 'red' },
  {
    vin: 2, year: 2016, brand: 'Hyundai', color: 'yellow'
  }];
  constructor(private http: HttpClient) {

  }
  ngOnInit(): void {
  }


}
