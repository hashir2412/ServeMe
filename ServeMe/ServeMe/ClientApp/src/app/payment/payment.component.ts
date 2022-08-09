import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { UserType } from '../constants/user-type.enum';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {
  firstFormGroup: FormGroup;
  userType: UserType = UserType.Customer;
  type = UserType;
  accountNumber = '';
  expiryDate = '';
  cvv = '';
  accountHolder = '';
  constructor(private dialogRef: MatDialogRef<PaymentComponent>) { }

  ngOnInit(): void {

  }

  onConfirm() {
    this.dialogRef.close(true);
  }

}
