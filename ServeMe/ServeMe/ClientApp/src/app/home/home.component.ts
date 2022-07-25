import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  constructor(private http: HttpClient) {
  }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  // Declare this key and iv values in declaration
  private key = '4512631236589784';
  private iv = '4512631236589784';

  // // Methods for the encrypt and decrypt Using AES
  // encryptUsingAES256() {
  //   var encrypted = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(JSON.stringify("Your Json Object data or string")), this.key, {
  //     keySize: 128 / 8,
  //     iv: this.iv,
  //     mode: CryptoJS.mode.CBC,
  //     padding: CryptoJS.pad.Pkcs7
  //   });
  //   console.log('Encrypted :' + encrypted);
  //   this.http.get<boolean>(`api/login?email=asd&password=${encrypted}`).subscribe(result => {
  //     console.log(result);
  //   }, error => console.error(error));
  //   return encrypted;
  // }

  // generate() {
  //   var encrypted = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(JSON.stringify("Your Json Object data or string")), this.key, {
  //     keySize: 128 / 8,
  //     iv: this.iv,
  //     mode: CryptoJS.mode.CBC,
  //     padding: CryptoJS.pad.Pkcs7
  //   });
  //   console.log('Encrypted :' + encrypted);
  // }
}
