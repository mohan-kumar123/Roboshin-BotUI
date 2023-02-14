import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { promise } from 'protractor';
import { ResponseDetails } from 'src/app/models/ResponseData';
import { AssociateDetails } from 'src/app/models/associate';
import { Observable } from 'rxjs';

import { Captcha } from 'src/app/models/Captcha';
import { SessionData } from 'src/app/models/SessionData';
import * as CryptoJS from 'crypto-js';
@Injectable({
  providedIn: 'root'
})
export class EncryptionService {
  
  constructor() { }

  // The set method is use for encrypt the value.
  ivArr = 'RobertBoschBanga';

  set(keys, value) {
    const key = CryptoJS.enc.Utf8.parse(keys);
    const iv = CryptoJS.enc.Utf8.parse(this.ivArr);
    console.log('Key : ' + key);
    console.log('IV : ' + iv);
    const encrypted = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(value.toString()), key,
      {
        keySize: 256 / 8,
        iv,
        mode: CryptoJS.mode.ECB,
        //padding: CryptoJS.pad.Pkcs7
      });

    return encrypted.toString();
  }

  // The get method is use for decrypt the value.
  get(keys,  value) {
    let key = CryptoJS.enc.Utf8.parse(keys);
    let iv = CryptoJS.enc.Utf8.parse(this.ivArr);
    let decrypted = CryptoJS.AES.decrypt(value, key, {
      keySize: 256 / 8,
      iv,
      mode: CryptoJS.mode.ECB,
      //padding: CryptoJS.pad.Pkcs7
    });



    return decrypted.toString(CryptoJS.enc.Utf8);
  }
}
