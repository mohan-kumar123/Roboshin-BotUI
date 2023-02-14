import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { promise } from 'protractor';
import { AssociateDetails } from 'src/app/models/associate';
import { Observable } from 'rxjs';

import { RequestTypesResponse } from 'src/app/models/RequestTypesResponse';
import { RequestTypesRequest } from 'src/app/models/RequestTypesRequest';
@Injectable({
  providedIn: 'root'
})
export class RoboschienSpainService {
  public formData: AssociateDetails;

  //localhost end point
  //url="https://roboschienapi-preprod.azurewebsites.net/Roboschien/SendFileEmailNotification";
  //url="https://roboschienapi-uat.azurewebsites.net/Roboschien/";
  url = "/RoboschienSpain/"; //http://localhost:51537
  //Azure app service end point
  constructor(public http: HttpClient) {
  }

  /*GetRequestTypes*/
  getRequestTypes(requestTypesRequest: RequestTypesRequest): Promise<RequestTypesResponse> {
    const headers = new HttpHeaders({ "Content-Type": "application/json" });

    let res: Promise<any>;
    res = this.http.post(this.url + 'GetRequestTypes', requestTypesRequest).toPromise();
    return res;
  }
  

}
