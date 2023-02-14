import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { promise } from 'protractor';
import { ResponseDetails } from 'src/app/models/ResponseData';
import { AssociateDetails } from 'src/app/models/associate';
import { Observable } from 'rxjs';

import { Captcha } from 'src/app/models/Captcha';
import { SessionData } from 'src/app/models/SessionData';
import { IllnessTypesResponse } from 'src/app/models/IllnessTypesResponse';
import { IllnessTypesRequest } from 'src/app/models/IllnessTypesRequest';
import { RequestTypesResponse } from 'src/app/models/RequestTypesResponse';
import { RequestTypesRequest } from 'src/app/models/RequestTypesRequest';
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

  verifyCaptcha(captcha: Captcha): Promise<Boolean> {
    let headerValues = new HttpHeaders({ "Content-Type": "application/json" });

    let res: Promise<any>;
    res = this.http.post(this.url + 'captcha/verify', captcha).toPromise();
    return res;
  }

  getSessionDetails(countryCode: IllnessTypesRequest): Promise<ResponseDetails> {
    const headers = new HttpHeaders({ "Content-Type": "application/json" });

    let res: Promise<any>;
    res = this.http.post(this.url + 'GetSessionDetails', countryCode).toPromise();
    return res;
  }

  getIllnessTypes(illnessTypeRequest: IllnessTypesRequest): Promise<IllnessTypesResponse> {
    const headers = new HttpHeaders({ "Content-Type": "application/json" });

    let res: Promise<any>;
    res = this.http.post(this.url + 'GetIllnessTypes', illnessTypeRequest).toPromise();
    return res;
  }
  /*GetRequestTypes*/
  getRequestTypes(requestTypesRequest: RequestTypesRequest): Promise<RequestTypesResponse> {
    const headers = new HttpHeaders({ "Content-Type": "application/json" });

    let res: Promise<any>;
    res = this.http.post(this.url + 'GetRequestTypes', requestTypesRequest).toPromise();
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
