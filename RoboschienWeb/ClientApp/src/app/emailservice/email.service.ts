import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { promise } from 'protractor';
import { AssociateDetails } from './../models/associate';
import { WorkDisabilityCertificate } from '../models/WorkDisabilityCertificate';
import { ResponseDetails } from '../models/ResponseData';
@Injectable({
  providedIn: 'root'
})
export class EmailService {
  public formData: AssociateDetails;

  //localhost end point
  //url="https://roboschienapi-preprod.azurewebsites.net/Roboschien/SendFileEmailNotification";
  //url="https://roboschienapi-uat.azurewebsites.net/Roboschien/";
  url = "/Roboschien/"; //http://localhost:51537
  //Azure app service end point
  constructor(public http: HttpClient) {
  }

  sendDetails(formData: FormData): Promise<ResponseDetails> {
    let headerValues = new HttpHeaders({ 'Accept': 'q=0.8;application/json;q=0.9' });

    let res: Promise<any>;
    res = this.http.post(this.url + 'SendFileEmailNotification', formData).toPromise();
    return res;
  }

  validateOcrImage(formData: FormData): Promise<ResponseDetails> {
    let res: Promise<any>;
    res = this.http.post(this.url + 'ValidateOcr', formData, { responseType: 'text' }).toPromise(); //,{headers:headerValues}
    return res;
  }

  getFormData() {
    return this.formData;
  }

  getNewCaptcha() {
    return this.http.get(this.url + 'GetCustomCaptcha');
  }

}
