import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { IllnessTypes } from '../models/IllnessTypes';
import { IllnessTypesRequest } from '../models/IllnessTypesRequest';

import { RequestTypes } from '../models/RequestTypes';
import { RequestTypesRequest } from '../models/RequestTypesRequest';
import { EmailService } from './emailservice/email.service';
import { RoboschienSpainService } from './roboschienSpainservice/roboschienSpain.service';

@Injectable({
  providedIn: 'root'
})
export class UtilityService {

  public selectedCountry: string;
  public selectedCountryCode: string;
  public selectedCountryCodeStr: string;
  public selectedLang: string;
  illnessTypeRequest: IllnessTypesRequest;
  public illnessTypes: IllnessTypes[];
  public kindofillness: IllnessTypes[];
  requestTypeRequest: RequestTypesRequest;
  public requestTypes: RequestTypes[];

  Countrychangeevent: Subject<any> = new Subject();

  constructor(private appService: EmailService, private spainService: RoboschienSpainService) {
  }

  set(country: string) {
    this.selectedCountry = country;
  }
  public getSelectedCountry(): string {
    return this.selectedCountry;
  }

  public getSelectedCountryCode(): string {
    console.log('Service this.selectedCountryCode  return :' + this.selectedCountryCode);
    return this.selectedCountryCode;
  }

  public setCountryCode(countryCode: string) {
    this.selectedCountryCode = countryCode;
    console.log('Service countryCode ' + countryCode);
    console.log('Service this.selectedCountryCode ' + this.selectedCountryCode);
    this.setCountryCodeForYou(this.selectedCountryCode);
  }

  setCountryCodeForYou(countryCode: string) {
    this.selectedCountryCodeStr = (' - ' + countryCode).toUpperCase();
  }

  public fetchIllnessTypes(countryCode: string, lang: string) {

    this.illnessTypeRequest = new IllnessTypesRequest();
    if (lang != undefined && lang != "") {
      this.illnessTypeRequest.CountryCode = lang;
    }
    else {
      this.illnessTypeRequest.CountryCode = "en";
    }
    //this.illnessTypeRequest.CountryCode = lang;
    console.log(countryCode + '_' + lang);
    if (countryCode == "po") {
      this.appService.getIllnessTypes(this.illnessTypeRequest).then(resp => {

        if (resp.IsSuccess) {
          this.illnessTypes = [];
          this.illnessTypes = resp.IllnessTypes;

          console.log("Illness Types response : " + this.illnessTypes.length);
        } else {
          console.log('something went wrong : ' + resp.Message);
        }
      })
    }
  }

  public fetchRequestTypes(countryCode: string) {
    this.illnessTypeRequest = new RequestTypesRequest();
    if (countryCode != undefined && countryCode != "") {
      this.illnessTypeRequest.CountryCode = countryCode;
    }
    else {
      this.illnessTypeRequest.CountryCode = "en";
    }

    console.log(countryCode + "+" + this.selectedCountryCode);
    if (this.selectedCountryCode == "es") {
      this.spainService.getRequestTypes(this.illnessTypeRequest).then(resp => {

        if (resp.IsSuccess) {
          console.log(resp);
          this.requestTypes = resp.RequestTypes;
          this.kindofillness = [];
          this.kindofillness = resp.IllnessTypesSP;
          console.log(this.requestTypes);
          console.log("Request Types response : " + this.requestTypes.length);
        } else {
          console.log('something went wrong : ' + resp.Message);
        }
      })
    }
  }

  //to get change of country
  CountrychangeEmit() {
    this.Countrychangeevent.next();
  }

  getSelectedLanguage() {
    console.log('Service this.selectedLang  return :' + this.selectedLang);
    return this.selectedLang;
  }
}
