import { Component, ViewChild, OnInit, ElementRef, Inject } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { MatDialog, MatStepper, MatCheckbox, MatCheckboxChange } from '@angular/material';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { HttpErrorResponse } from '@angular/common/http';
import { FormControl, Validators } from '@angular/forms';

import { ResponseDetails } from './../../models/ResponseData';
import { AssociateDetails } from './../../models/associate';
import { WorkDisabilityCertificate } from './../../models/WorkDisabilityCertificate';

import { MyErrorStateMatcher } from '../../shared/myerrorstatematcher';

import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
//import { DatePipe } from '@angular/common'


import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';

import { MatIconRegistry } from "@angular/material/icon";
import { DomSanitizer } from "@angular/platform-browser";
import * as CryptoJS from 'crypto-js';
import { AppDateAdapter, APP_DATE_FORMATS } from '../../adapters/date.adapter';
import { EmailService } from 'src/app/services/emailservice/email.service';
import { DialogComponent } from '../dialog/dialog.component';
import { AgreeDiaglogComponent } from '../agree-diaglog/agree-diaglog.component';
import { CaptchaComponent } from 'angular-captcha';

import { ReCaptchaValidateComponent } from '../recaptcha-validate/recaptcha-validate.component';
import { Captcha } from 'src/app/models/Captcha';
import { SessionData } from 'src/app/models/SessionData';
import { EncryptionService } from 'src/app/services/encryptionservice/encryption.service';
import { createWorker } from 'tesseract.js';
import { Ng2ImgMaxService } from 'ng2-img-max';
import { HelpCertificateComponent } from '../help-certificate/help-certificate.component';
import { DatePipe, DOCUMENT, formatDate } from '@angular/common';
import { CountrySelectionComponent } from '../country-selection/country-selection.component';
import { UtilityService } from 'src/app/services/utility.service';
import { IllnessTypesRequest } from 'src/app/models/IllnessTypesRequest';
import { IllnessTypes } from 'src/app/models/IllnessTypes';
import { environment } from '../../../environments/environment';
//import moment = require('moment');
import * as moment from 'moment';
import { Console } from '@angular/core/src/console';



@Component({
  selector: 'app-form-container',
  templateUrl: './form-container.component.html',
  styleUrls: ['./form-container.component.css'],
  providers: [
    { provide: DateAdapter, useClass: AppDateAdapter },
    { provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS },
    DatePipe

    //{provide: MAT_DATE_LOCALE, useValue: 'es-in'},
    //{provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE]},
    //{provide: MAT_DATE_FORMATS, useValue: MAT_MOMENT_DATE_FORMATS}
  ],
})


export class FormContainerComponent implements OnInit {


  associateDetails: AssociateDetails;
  public formDate: FormControl;
  //public fromDate: FormControl;
  employeeForm: FormGroup;
  employeeForm1: FormGroup;
  formData = new FormData();
  selectedFiles: File = null;
  finalImageFile: File = null;
  responseData: ResponseDetails;
  illnessTypeRequest: IllnessTypesRequest;
  matcher = new MyErrorStateMatcher();
  minDate = new Date();

  IsMobileNumberValid: boolean = false;
  IsMobileNumberHint: boolean = false;
  IsEmailHint: boolean = false;
  isNotMatched: boolean = false;
  isUploadedImage: boolean = false;
  isWordMatched: boolean = true;
  isStartDate: boolean = false;
  isEndDate: boolean = false;
  isFollowupDate: boolean = false;
  isSpainDateNotValid: boolean = true;
  isFileFormatAllowed = true;
  isFileExtensionAllowed = true;
  url: any;

  LOCALE: string = 'Locale';
  SESSION_KEY: string = 'SessionKey';
  SECRET_KEY: string = 'SecretKey';
  //fileByteArray=[];
  // emailPattern = "^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
  initialIllness: boolean = false;
  recurringIllness: boolean = false;
  errorOccurred: boolean = false;
  accident: boolean = false;
  hospitalization: boolean = false; //new
  covid: boolean = false; //new
  partTime: boolean = false; //new
  hideMarker: boolean = false;
  IsRequired: boolean = false;
  allowedFileTypes = ["image/jpeg", "image/jpg", "image/png"];
  IsAgree: boolean = true;
  DataPrivacyTimeStamp: string = '';
  isCaptchaVerify: Boolean = false;
  sessionData: SessionData;
  certificateText: string = '';
  //ocrMatchText: string[] = ["zur", "Vorlage", "Ausfertigung", "beim", "Arbeitgeber", "krankenkasee","Diagnose"];
  ocrMatchText: string[] = ['Arbeitsunfähigkeitsbescheinigung', 'Arbeitsunfähigkeits', 'Ausfertigung', 'zur', 'Vorlage',
    'beim', 'Arbeitgeber'];

  // franceOcrMathcText: string[] = ['arrêt de travail', 'BULLETIN DE SITUATION', 'maladie professionnelle', 'accident de travail'];
  //franceOcrMathcText: string[] = ['arrêt', 'de', 'travail', 'BULLETIN', 'SITUATION', 'maladie', 'professionnelle', 'accident',
  //  'travail', "HOSPITALISATION"];

  franceOcrMathcText: string[] = ['arrêt', 'a\'arret', 'travail', 'travall', 'bravail'];

  franceOcrHospitalizationMathcText: string[] = ['BULLETIN', "HOSPITALISATION"];

  portugalOcrMathcText: string[] = ['INCAPACIDADE', 'Médico', 'Beneficiário'];
  spainOcrMathcText: string[] = ["COPY", "OF", "THE", "COMPANY", "EJEMPLAR", "PARA", "LA", "EMPRESA", "EXEMPLAR", "A", "PER", "LEMPRESA"];
  spainOcrEngText: string[] = ['Sick', 'Start', 'note', 'end', 'Follow-up', 'star/end'];
  spainOcrSpaText: string[] = ['Parte', 'baja', 'alta', 'baja/alta', 'confirmación'];
  spainOcrGalText: string[] = ['Parte', 'baixa', 'alta', 'baixa/alta', 'confirmación'];
  spainOcrCatText: string[] = ["Comunicat", "baixa", "d'alta", "baixa/d'alta", "confirmació"];

  Spain2Text: string[] = ["SICK", "START", "NOTE", "END", "FOLLOWUP", "STARTEND", "PARTE", "DE", "BAJA", "ALTA", "BAJAALTA",
    "CONFIRMACION", "BAIXA", "BAIXAALTA", "COMUNICAT", "DALTA", "BAIXADALTA", "CONFIRMACIO"];

  // ocrMatchText2: string[] = ["Arbeitsunfähigkeitsbescheinigung", "Arbeitsunfähigkeits"];
  validText1: string = "Arbeitsunfähigkeitsbescheinigung";
  validText2: string = "Arbeitsunfähigkeits";

  //inValidText1: string = "Diagnose(n)"; //begründende //AU-begründende
  inValidText1: string = "begründende";
  inValidText2: string = "Finanzämter";
  inValidText3: string = "Attest";
  inValidText4: string = "Arztliches";
  matchedWords: string[] = [];

  IsvalidationStep1: Boolean = true;
  IsvalidationStep2: Boolean = true;
  IsvalidationStep3: Boolean = true;

  imageWidth: number;
  imageHeight: number;
  aspectRatio: number;
  testWidth = 800;
  testHeight = 615;
  covidWidth = 965;

  privacyAgreement: boolean = false;
  IsChecked: boolean = false;

  selectedlanguage: string;

  lang: string = 'en';
  selectedCountry: string = '';
  //countryCode: string = '';
  ocrLanguage: string = 'deu';
  frDateValidation: boolean = false;
  poDateValidation: boolean = false;

  illnessTypeNotSelected: boolean = true;
  tempIllnessTypeNotSelected: boolean = false;

  kindofillnessNotSelected: boolean = true;
  tempKindofIllnessNotSelected: boolean = false;
  requestTypeNotSelected: boolean = true;
  tempRequestTypeNotSelected: boolean = false;

  @ViewChild('uploader') uploader: ElementRef;
  element: HTMLElement;

  //illnessTypes: IllnessTypes[];
  germanyOcrCount: number = 0;
  germanyOcrFalg: boolean = false;
  franceOcrCount: number = 0;
  portugalOcrCount: number = 0;
  spainOcrCount: number = 0;
  franceOcrFalg: boolean = false;
  portugalOcrFalg: boolean = false;
  spainOcrFalg: boolean = false;
  selectedIllnessType: string = '';
  selectedRequestType: string = '';
  selectedKindofIllness: string = '';
  fromDateStr: string = '';
  toDateStr: string = '';
  followDateStr: string = '';
  maxDate: Date;


  constructor(public appService: EmailService, private fb: FormBuilder, public dialog: MatDialog,
    private router: Router, protected translate: TranslateService,
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer,
    private encService: EncryptionService,
    private ng2ImgMax: Ng2ImgMaxService,
    public utilityService: UtilityService,
    private datePipe: DatePipe,
    @Inject(DOCUMENT) document


  ) {
    this.createForm();
    this.matIconRegistry.addSvgIcon(
      "upload_icon",
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/images/icons/upload.svg")
    );

    this.matIconRegistry.addSvgIcon(
      "calender_icon",
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/images/icons/calendar_bosch.svg")
    );

    this.matIconRegistry.addSvgIcon(
      "delete_icon",
      this.domSanitizer.bypassSecurityTrustResourceUrl("assets/images/icons/bosch-ic-delete.svg")
    );

    this.formDate = new FormControl('');
    // this.fromDate = new FormControl();
    this.employeeForm.markAsPristine();
    this.employeeForm.markAsUntouched();

    let localLang = localStorage.getItem("lang");
    this.selectedlanguage = localLang == null ? this.lang : localLang;
    //this.selectedLanguageName = localLang == null ? 'English' : this.languages.find(m => m.language_abbrevation == localLang).language_name;
    this.selectedlanguage == null ? this.translate.use("en") : this.translate.use(this.selectedlanguage);




  }

  ngOnInit(): void {
    debugger;
    let localLang = localStorage.getItem("lang");
    this.selectedlanguage = localLang == null ? this.lang : localLang;
    //this.selectedLanguageName = localLang == null ? 'English' : this.languages.find(m => m.language_abbrevation == localLang).language_name;
    this.selectedlanguage == null ? this.translate.use("en") : this.translate.use(this.selectedlanguage);
    //country change to reset the date fields
    this.utilityService.Countrychangeevent.subscribe(data => {
      this.resetDatefields();
    })
    this.germanyOcrCount = 0;
    this.franceOcrCount = 0;
    this.portugalOcrCount = 0;
    this.spainOcrCount = 0;
    this.franceOcrFalg = false;
    this.portugalOcrFalg = false;
    this.germanyOcrFalg = false;
    this.spainOcrFalg = false;

    this.selectedCountry = localStorage.getItem("country");
    //this.dynamicWidth();
    console.log("Form component : Selected-Country : " + this.selectedCountry);
    this.utilityService.set(this.selectedCountry);
    this.utilityService.selectedLang=this.selectedlanguage;

    this.dialog.open(CountrySelectionComponent, {
      width: '600px',
      height: '400px',
      disableClose: true,
      data: { action: 1, data: '' }
    }).afterClosed().subscribe(data => {
      if (data && data.action === 1) {
        // this.DataPrivacyTimeStamp = data.data;

        console.log('form-container : countryselection-close : ' + data.data);


      } else {
        console.log('form-container : countryselection-close : ' + data.data);
      }


      if (this.utilityService.selectedCountry == environment.COUNTRY_NAME_PORTUGAL) {
        this.utilityService.setCountryCode(environment.COUNTRY_CODE_PORTUGAL);
        this.dynamicWidth();


      } else if (this.utilityService.selectedCountry == environment.COUNTRY_NAME_FRANCE) {
        this.utilityService.setCountryCode(environment.COUNTRY_CODE_FRANCE);
       this.dynamicWidth();

      } else if (this.utilityService.selectedCountry == environment.COUNTRY_NAME_SPAIN) {
        this.utilityService.setCountryCode(environment.COUNTRY_CODE_SPAIN);

      } else {
        this.utilityService.setCountryCode(environment.COUNTRY_CODE_GERMANY);

      }

      const browserLang = this.translate.getBrowserLang();
      let currentLang = localStorage.getItem("lang");

      if (this.utilityService.selectedCountryCode == environment.COUNTRY_CODE_PORTUGAL && ((browserLang == 'pt' && currentLang == 'po') || currentLang == 'po')) {
        if (currentLang == 'po')
          this.utilityService.fetchIllnessTypes(environment.COUNTRY_CODE_PORTUGAL, currentLang);
        else
          this.utilityService.fetchIllnessTypes(environment.COUNTRY_CODE_GERMANY, currentLang);
      }
      //else if (this.utilityService.selectedCountryCode == environment.COUNTRY_CODE_SPAIN ) {
      //  if (currentLang == 'es')
      //    this.utilityService.fetchIllnessTypes(environment.COUNTRY_CODE_SPAIN);
      //  else
      //    this.utilityService.fetchIllnessTypes(environment.COUNTRY_CODE_GERMANY);
      //}
      else {

        this.utilityService.fetchIllnessTypes(environment.COUNTRY_CODE_PORTUGAL, currentLang);
      }
      /*fetchRequestTypes*/
      console.log('fetchRequestTypes ' + this.utilityService.selectedCountryCode)
      if (this.utilityService.selectedCountryCode == environment.COUNTRY_CODE_SPAIN) {
        console.log(currentLang);
        this.utilityService.fetchRequestTypes(currentLang);

      }
      else {

        this.utilityService.fetchRequestTypes(environment.COUNTRY_CODE_GERMANY);
      }
    });
    this.dynamicWidth();
    //this.maxDate = new Date();
    //if (this.utilityService.selectedCountryCode != environment.COUNTRY_CODE_SPAIN) {
    //  this.maxDate.setDate(this.maxDate.getDate() + 7);
    //}

  }

dynamicWidth()
{
  if(this.utilityService.selectedCountry == "portugal" || this.utilityService.selectedCountryCode == "po")
  {
    this.covidWidth = 0;
  }
  else{
    this.covidWidth = 965;
  }
}


  getSessionDetails(stepper: MatStepper) {
    this.responseData = new ResponseDetails();
    this.dialog.open(DialogComponent, {
      data: this.responseData
      , disableClose: true
      , width: '220px'

    });

    this.illnessTypeRequest = new IllnessTypesRequest();
    this.illnessTypeRequest.CountryCode = this.utilityService.selectedCountryCode;

    this.appService.getSessionDetails(this.illnessTypeRequest).then(resp => {
      if (resp.IsSuccess) {
        let response: any[] = resp.ResponseDataDetails;
        this.sessionData = response[0];


        console.log("SessionData is not null");
        console.log("SessionKey : " + this.sessionData.SessionKey);
        console.log("SecretKey : " + this.sessionData.SecretKey);
        localStorage.setItem(this.SESSION_KEY, this.sessionData.SessionKey);
        localStorage.setItem(this.SECRET_KEY, this.sessionData.SecretKey);
        this.dialog.closeAll();
        this.sendFormData(stepper);
      } else {
        console.log("SessionData is null : " + resp.ErrorMessage);
        this.errorOccurred = false;
        this.dialog.closeAll();
        stepper.next();
        //this.sendFormData(stepper, false);
      }
    });
  }

  InitialIllness() {
    setTimeout(() => {

      if (this.employeeForm.get('initialIllness').value) {
        this.recurringIllness = false;
        this.initialIllness = true;
        this.employeeForm.get('recurringIllness').setValue(false);
      }

      if (this.employeeForm.get('accident').value && this.employeeForm.get('initialIllness').value)
        this.IsRequired = false;
      else if (!this.employeeForm.get('accident').value && this.employeeForm.get('initialIllness').value)
        this.IsRequired = false;
      else if (this.employeeForm.get('accident').value && (!this.employeeForm.get('initialIllness').value && !this.employeeForm.get('recurringIllness').value))
        this.IsRequired = true;
      else if (this.employeeForm.get('hospitalization').value && (!this.employeeForm.get('initialIllness').value && !this.employeeForm.get('recurringIllness').value))
        this.IsRequired = true;
        else if (this.employeeForm.get('covid').value && (!this.employeeForm.get('initialIllness').value && !this.employeeForm.get('recurringIllness').value))
        this.IsRequired = true;
        else if (this.employeeForm.get('partTime').value && (!this.employeeForm.get('initialIllness').value && !this.employeeForm.get('recurringIllness').value))
        this.IsRequired = true;
      else if (this.selectedIllnessType && (!this.employeeForm.get('initialIllness').value && !this.employeeForm.get('recurringIllness').value))
        this.IsRequired = true;
      else
        this.IsRequired = false;

      if (this.selectedIllnessType) {
        this.illnessTypeNotSelected = false;
        this.tempIllnessTypeNotSelected = false;
      } else {
        this.illnessTypeNotSelected = true;
        this.tempIllnessTypeNotSelected = true;
      }

    }, 100);

  }

  RecurringIllness() {
    setTimeout(() => {

      if (this.employeeForm.get('recurringIllness').value) {
        this.recurringIllness = true;
        this.initialIllness = false;
        this.employeeForm.get('initialIllness').setValue(false);
      }

      if (this.employeeForm.get('accident').value && this.employeeForm.get('recurringIllness').value)
        this.IsRequired = false;
      else if (!this.employeeForm.get('accident').value && this.employeeForm.get('recurringIllness').value)
        this.IsRequired = false;
      else if (this.employeeForm.get('accident').value && (!this.employeeForm.get('initialIllness').value && !this.employeeForm.get('recurringIllness').value))
        this.IsRequired = true;
      else if (this.employeeForm.get('hospitalization').value && (!this.employeeForm.get('initialIllness').value && !this.employeeForm.get('recurringIllness').value))
        this.IsRequired = true;
        else if (this.employeeForm.get('covid').value && (!this.employeeForm.get('initialIllness').value && !this.employeeForm.get('recurringIllness').value))
        this.IsRequired = true;
      else if (this.employeeForm.get('partTime').value && (!this.employeeForm.get('initialIllness').value && !this.employeeForm.get('recurringIllness').value))
        this.IsRequired = true;
      else if (this.selectedIllnessType && (!this.employeeForm.get('initialIllness').value && !this.employeeForm.get('recurringIllness').value))
        this.IsRequired = true;
      else
        this.IsRequired = false;

      if (this.selectedIllnessType) {
        this.illnessTypeNotSelected = false;
        this.tempIllnessTypeNotSelected = false;
      } else {
        this.illnessTypeNotSelected = true;
        this.tempIllnessTypeNotSelected = true;
      }

    }, 100);
  }


  createForm() {
    this.employeeForm = this.fb.group({
      employeeNumber: ['', [
        Validators.required,
        Validators.minLength(8),
        Validators.maxLength(8),
        Validators.pattern(new RegExp("^[0-9]+$"))],



      ],
      employeeFirstName: ['', [
        Validators.required,
        //Validators.pattern(new RegExp("^[a-zA-Z0-9_ ]*$"))
        Validators.pattern(new RegExp("^[^<>\"/;%]*$"))

      ]
      ],
      employeeLastName: ['', [
        Validators.required,
        Validators.pattern(new RegExp("^[^<>\"/;%]*$"))
      ],
      ],
      email: ['', [

        // Validators.pattern("^[A-Za-z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,}$")
        Validators.email
      ]
      ],
      mobileNumber: ['', [
        Validators.maxLength(14),
        Validators.pattern(new RegExp("^[0-9+]+$")),
        //Validators.pattern(new RegExp("^[0-9]{3}['']{1}[0-9]{8}$")),

      ]
      ],
      // agreeStatement: ['', [Validators.required]],

      fromDate: [''],

      toDate: [''],
      followupDate: [''],
      initialIllness: [false],
      recurringIllness: [false],

      accident: [false],
      hospitalization: [false],
      covid: [false],
      partTime: [false],
      // illnessTypeControl: ['', [Validators.required]],
      attachment: ['', [Validators.required]],
    });

    this.employeeForm1 = this.fb.group({
      employeeNumber1: [''],
      employeeFirstName1: [''],
      employeeLastName1: [''],
      email1: [''],
      mobileNumber1: [''],
      agreeStatement1: [''],

      fromDate1: [''],

      toDate1: [''],
      followupDate1: [''],
      initialIllness1: [''],
      recurringIllness1: [''],

      accident1: [''],
      hospitalization1: [''],
      covid1:[''],
      partTime1:[''],
    });
  }



  fromdateChange(event: any) {
    this.maxDate = new Date(event.value);

    console.log('fromDate: event.value : ' + event.value);
    this.fromDateStr = moment(event.value).format('yyyy-MM-DD');

    console.log('fromDate: dateStr : ' + this.fromDateStr);

    console.log('fromDate: minDate :' + this.minDate);
    //this.dateValidation(this.minDate);
    this.employeeForm.get('toDate').setValue('');
    this.SpainDateValidate();
  }

  toDateChange(event: any) {
    console.log('fromDate: event.value : ' + event.value);
    this.toDateStr = moment(event.value).format('yyyy-MM-DD');

    console.log('fromDate: dateStr : ' + this.toDateStr);
    if (this.utilityService.selectedCountryCode == environment.COUNTRY_CODE_SPAIN)
    this.SpainDateValidate();
  }

  followupChange(event: any) {
    console.log('followupDate: event.value : ' + event.value);
    this.followDateStr = moment(event.value).format('yyyy-MM-DD');

    console.log('followupDate: dateStr : ' + this.followDateStr);
    if (this.utilityService.selectedCountryCode == environment.COUNTRY_CODE_SPAIN)
    this.SpainDateValidate();
  }

  dateValidation(selectedDate: Date) {

    let todayDate = new Date();
    let days = Math.floor((todayDate.getTime() - selectedDate.getTime()) / 1000 / 60 / 60 / 24);

    if (this.utilityService.selectedCountryCode == environment.COUNTRY_CODE_FRANCE && days > 7) {

      this.frDateValidation = true;
      this.poDateValidation = false;
      console.log('fr date validation : IF block executed');
    } else if (this.utilityService.selectedCountryCode == environment.COUNTRY_CODE_PORTUGAL && days > 60) {

      this.poDateValidation = true;
      this.frDateValidation = false;
      console.log('po date validation : IF block executed');
    } else {
      this.poDateValidation = false;
      this.frDateValidation = false;

    }
    //} else if (this.utilityService.selectedCountryCode == environment.COUNTRY_CODE_PORTUGAL) {
    //  this.poDateValidation = false;
    //  console.log('po date validation : ELSE block executed');


    //} else if (this.utilityService.selectedCountryCode == environment.COUNTRY_CODE_FRANCE) {

    //  this.frDateValidation = false;

    //  console.log('fr date validation : ELSE block executed');
    //}

    console.log("Days difference : " + days);
    console.log("Date validation :  Selected Country Code : " + this.utilityService.selectedCountryCode);
    //console.log("Date validation :  environment.COUNTRY_CODE_PORTUGAL : " + environment.COUNTRY_CODE_PORTUGAL);
  }

  OnSubmit(stepper: MatStepper) {


    const dialogRef = this.dialog.open(ReCaptchaValidateComponent, {
      disableClose: true, data: { IsCaptchaValidated: false }, height: '300px',
      width: '450px',
    })


    dialogRef.afterClosed().subscribe(result => {
      console.log('Form-Container : Captcha-Response' + result.CaptchaResponse);
      if (result.IsCaptchaValidated === true) {
        //extract input values from form
        let captcha: Captcha = {

          requestSource: 'WEB',
          response: result.CaptchaResponse
        };

        this.appService.verifyCaptcha(captcha).then(resp => {
          this.isCaptchaVerify = resp;
          console.log("Captcha Value : " + this.isCaptchaVerify);

        });

        this.getSessionDetails(stepper);


      }
    });


  }

  sendFormData(stepper: MatStepper) {

    this.responseData = new ResponseDetails();


    this.extractFormControlValues('fromCode');

    this.dialog.open(DialogComponent, {
      data: this.responseData
      , disableClose: true
      , width: '220px'

    });

    //append associate details to form
    if (this.privacyAgreement) {
      this.formData.append('Consent', 'true');
    } else {
      this.formData.append('Consent', 'false');
    }

    var tempLocale = localStorage.getItem("lang");
    if (tempLocale) {
      this.formData.append(this.LOCALE, tempLocale);
    } else {
      this.formData.append(this.LOCALE, 'en');
    }

    // this.formData.append('Locale', localStorage.getItem("lang"));
    this.formData.append('RequestSource', JSON.stringify("Web"));
    this.formData.append(this.SESSION_KEY, JSON.stringify("" + this.sessionData.SessionKey));
    this.formData.append('Associate', JSON.stringify(this.associateDetails));

    //let encrypted = CryptoJS.AES.encrypt("Secret Text", key, {iv: iv}).toString();
    this.appService.sendDetails(this.formData).then(resp => {
      this.responseData = resp;
      this.errorOccurred = this.responseData.IsSuccess;
      console.log(this.responseData);
      stepper.next();
      this.dialog.closeAll();

    }, error2 => {
      if (error2 instanceof HttpErrorResponse) {
        console.log(error2);
        this.errorOccurred = false;
        stepper.next();
        this.dialog.closeAll();
      }
    });




    //reset the form here
    this.resetForm();
  }



  onIntermediateImage() {

    this.dialog.open(HelpCertificateComponent, {
      width: '800px',
      height: '650px',
      disableClose: true,
      data: { action: 1, data: '' }
    }).afterClosed().subscribe(data => {
      if (data && data.action === 1) {
        // this.DataPrivacyTimeStamp = data.data;

        console.log('form-container : HelperCertificate-close : ' + data.data);
      } else {
        // var d = new Date();
        //var time = '' + d.getTime();
        // this.DataPrivacyTimeStamp = time;
      }
      //this.onSelectFile(event);
      //console.log('form-container : HelperCertificate-close : ');
      //let element: HTMLElement = this.uploader.nativeElement as HTMLElement;
      //element.click();
      //console.log(this.uploader.nativeElement.innerHTML);
      // let el: HTMLElement = this.myDiv.nativeElement;
      //el.click();
      // this.myDiv.nativeElement.click();
      //console.log(this.myDiv.nativeElement.innerHTML);

      // this.element = document.getElementById('ButtonX') as HTMLElement;
      //this.element.click();
    });


    //this.IsAgree = false;

    // console.log("IsAgree : " + this.IsAgree);

  }

  onSelectFile(event: any) {
    this.employeeForm.get('attachment').setErrors(null);
    this.selectedFiles = <File>event.target.files[0];

    let uploadedFileType = this.selectedFiles.type;
    this.isFileFormatAllowed = true;
    this.isFileExtensionAllowed = true;
    let fileName = this.selectedFiles.name;
    let IsValid = false;
    if (this.selectedFiles != undefined) {

      var ext = fileName.match(/\.(.+)$/)[1];
      switch(ext)
      {
          case 'jpg':
            IsValid = true;
            console.log('valid format is'+IsValid);
            break;
          case 'jpeg':
            IsValid = true;
            console.log('valid format is'+IsValid);
            break;
          case 'png':
            IsValid = true;
            console.log('valid format is'+IsValid);
            break;
          default:
              console.log('4Invalid format is'+IsValid);
              this.isFileExtensionAllowed = false;
              this.isFileFormatAllowed = true;
              this.IsvalidationStep1 = true;
              this.IsvalidationStep2 = true;
              this.IsvalidationStep3 = true;
              this.selectedFiles = null;
              this.employeeForm.get('attachment').setErrors({ required: true });
              fileName = '';
              break;
      }

      if(IsValid)
      {
      //Allowed File size is 5 MB (5 * 1024 * 1024) and only .jpg , .jpeg and .png are allowed  5242880
      //Allowed File size is 10 MB (10 * 1024 * 1024) and only .jpg , .jpeg and .png are allowed  10485760
      if ((this.selectedFiles.size > 10485760) || !this.allowedFileTypes.find((i) => i == uploadedFileType)) {
        this.isFileFormatAllowed = false;
        this.selectedFiles = null;
        this.employeeForm.get('attachment').setErrors({ required: true });
      }
      else {
        this.initializeImageProcess();
      }
    }
    }
  }

  initializeImageProcess() {

    try {
      // let formOcrData = new FormData();
      // let ocrFile = <File>event.target.files[0];
      // formOcrData.append('Image', this.selectedFiles, this.selectedFiles.name);

      // Loading
      this.responseData = new ResponseDetails();
      this.responseData.Isocr = true;
      this.dialog.open(DialogComponent, {
        data: this.responseData
        , disableClose: true
        , width: '220px'
      });

      let reader = new FileReader();

      reader.onload = (event: ProgressEvent) => {
        this.url = (<FileReader>event.target).result;

        const img = new Image();
        img.onload = () => {
          console.log('image  : image width : ' + img.width);
          console.log('image : image height : ' + img.height);
          this.imageHeight = img.height;
          this.imageWidth = img.width;

          this.processImage();
        };
        img.src = reader.result as string;
      }
      reader.readAsDataURL(this.selectedFiles);

    } catch (ex) {

      console.log('Exception  : ' + ex);
      this.isNotMatched = true;
      this.dialog.closeAll();
    }
  }

  processImage() {


    if (this.imageHeight > this.testHeight && this.imageWidth > this.testWidth) {
      if (this.imageWidth > this.imageHeight) {

        this.aspectRatio = this.imageWidth / this.imageHeight;
        console.log('Landscape : aspect ratio : ' + this.aspectRatio);
        console.log('Landscape : resizing image width as ' + this.aspectRatio * this.testWidth);
        console.log('Landscape : resizing image height as ' + this.aspectRatio * this.testHeight);

        this.ng2ImgMax.resizeImage(this.selectedFiles, this.aspectRatio * this.testWidth, this.aspectRatio * this.testHeight).subscribe(
          result => {
            this.finalImageFile = result;
            console.log('finalImageFile Size is : ' + this.finalImageFile.size);

            this.startOcrProcess(this.finalImageFile);
          },
          error => {
            console.log('😢 Oh no!', error);
            this.isNotMatched = true;
            this.dialog.closeAll();
          }
        );
      } else {
        console.log('else : image width : ' + this.imageWidth);
        console.log('else : image Height : ' + this.imageHeight);
        this.aspectRatio = this.imageHeight / this.imageWidth;
        console.log('portrait  : aspect ratio : ' + this.aspectRatio);
        console.log('portrait  : resizing image width as ' + this.aspectRatio * this.testHeight);
        console.log('portrait  : resizing image height as ' + this.aspectRatio * this.testWidth);

        this.ng2ImgMax.resizeImage(this.selectedFiles, this.testHeight * this.aspectRatio, this.testWidth * this.aspectRatio).subscribe(
          result => {
            this.finalImageFile = result;

            console.log('finalImageFile Size is : ' + this.finalImageFile.size);
            // const reader = new FileReader();
            // reader.readAsDataURL(this.uploadedImage); // read file as data url
            //reader.onload = (event) => { // called once readAsDataURL is completed
            //  this.url = event.target.result as string;
            //  console.log('After compress image size is' + this.uploadedImage.size);
            //};
            this.startOcrProcess(this.finalImageFile);
          },
          error => {
            console.log('😢 Oh no!', error);
            this.isNotMatched = true;
            this.dialog.closeAll();
          }
        );

      }
    } else {

      console.log('Image width should be >' + this.testWidth + ' and Height should be > ' + this.testHeight);
      this.startOcrProcess(this.selectedFiles);
    }


  }
  startOcrProcess(finalImage: File) {

    if (this.utilityService.selectedCountry == environment.COUNTRY_NAME_FRANCE) {
      this.ocrLanguage = environment.COUNTRY_CODE_FRANCE_OCR;

    } else if (this.utilityService.selectedCountry == environment.COUNTRY_NAME_PORTUGAL) {
      this.ocrLanguage = environment.COUNTRY_CODE_PORTUGAL_OCR;
    }
    else {
      this.ocrLanguage = environment.COUNTRY_CODE_SPAIN_OCR;
    }

    try {
      console.log('startOcrProcess Started...')
      console.log('ocr language : ' + this.ocrLanguage);
      let count = 0;
      const d = new Date();
      this.isNotMatched = false;
      this.isUploadedImage = false;
      this.isWordMatched = true;

      const worker = createWorker({
        // workerPath: '../node_modules/tesseract.js/dist/worker.min.js',
        // workerPath: '',
        // langPath: '../lang-data',
        // corePath: '../node_modules/tesseract.js-core/tesseract-core.wasm.js',
        logger: (m) => console.log(m),

      });

      (async () => {
        await worker.load();
        await worker.loadLanguage(this.ocrLanguage);
        await worker.initialize(this.ocrLanguage);
        const { data: { text } } = await worker.recognize(finalImage);


        console.log('...............................................')

        console.log(this.utilityService.selectedCountry + ' : ocr');
        console.log('ocr result : ' + text);
        const l = new Date();
        console.log('end time : ' + (l.getTime() - d.getTime()));
        this.certificateText = text;
        console.log(text[1]);


        if (this.utilityService.selectedCountry == environment.COUNTRY_NAME_FRANCE
        ) {

          console.log('OCR Language : ' + this.utilityService.selectedCountry);

          let franceRes = this.processFranceOcr(this.certificateText);
          if (franceRes) {
            this.updateImageInFormDat();
            this.franceOcrFalg = true;
          } else {
            this.franceOcrCount += 1;
            //checking the ocr failures
            if (this.franceOcrCount > 2) {
              this.isNotMatched = false;
              this.updateImageInFormDat();
              this.franceOcrCount = 0;
              this.franceOcrFalg = false;
            } else {
              this.isNotMatched = true;
            }

          }
        }
        else if (this.utilityService.selectedCountry == environment.COUNTRY_NAME_PORTUGAL) {

          console.log('OCR Language : ' + this.utilityService.selectedCountry);

          let portugalRes = this.processPortugalOcr(this.certificateText);
          if (portugalRes) {
            this.updateImageInFormDat();
            this.portugalOcrFalg = true;
          } else {
            this.portugalOcrCount += 1;
            if (this.portugalOcrCount > 2) {

              this.isNotMatched = false;
              this.updateImageInFormDat();
              this.portugalOcrCount = 0;
              this.portugalOcrFalg = false;
            } else {
              this.isNotMatched = true;
            }
          }
        }
        //spain
        else if (this.utilityService.selectedCountry == environment.COUNTRY_NAME_SPAIN) {

          console.log('OCR Language : ' + this.utilityService.selectedCountry);

          let spainRes = this.processSpainOcr(this.certificateText);
          if (spainRes) {
            this.updateImageInFormDat();
            this.spainOcrFalg = true;
          } else {
            this.spainOcrCount += 1;
            if (this.spainOcrCount > 2) {

              this.isNotMatched = false;
              this.updateImageInFormDat();
              //this.spainOcrCount = 0;    //Every third attampt it is accepting

              this.spainOcrFalg = false;
            } else {
              this.isNotMatched = true;
            }
          }
          this.SpainDateValidate();
        }
        else {
          console.log("Country Selection not matched...");
          console.log("selected country : " + this.utilityService.selectedCountry);
        }

        //  if (this.certificateText != '' && this.certificateText.includes(this.validText) &&
        //    (!this.certificateText.includes(this.inValidText1) && !this.certificateText.includes(this.inValidText2))) {
        //  this.isNotMatched = false;

        //  this.formData.append('Image', this.selectedFiles, this.selectedFiles.name); //Send as  File.  TODO: Need to Encrypt
        //    this.employeeForm.get('attachment').setErrors(null);
        //    console.log("Document is Valid...");
        //  } else {
        //    console.log("Document is invalid...");
        //  this.isNotMatched = true;
        //}
        await worker.terminate();
        this.dialog.closeAll();
      })();
    } catch (e) {
      console.log(e);
      this.isNotMatched = true;
      this.dialog.closeAll();
    }
  }


  processFranceOcr(certificateResult: string) {

    // Count the matched words in the image source
    this.matchedWords.length = 0;
    let count = 0;

    if (this.employeeForm.get('hospitalization').value) {

      for (const word of this.franceOcrHospitalizationMathcText) {

        if (certificateResult.includes(word)) {
          count++;
          console.log('Hospitalisation option : Matched word : ' + word);
          this.matchedWords.push(word);
        }
      }
    } else {


      for (const word of this.franceOcrMathcText) {


        if (certificateResult.includes(word)) {
          count++;
          console.log('Matched word : ' + word);
          this.matchedWords.push(word);
        }
      }

    }

    if (this.matchedWords.length >= 1) {

      console.log('Stpe 2 : ' + count + '  words matched with the Ocr Results, so the document is valid....');
      this.IsvalidationStep1 = true;
      return true;
    } else {
      console.log('Stpe 2 : ' + count + '  words not matched with the Ocr Results , so the document is invalid....')
      this.IsvalidationStep1 = false;
      return false;
    }
  }

  processPortugalOcr(certificateResult: string) {

    // Count the matched words in the image source
    this.matchedWords.length = 0;
    let count = 0;
    for (const word of this.portugalOcrMathcText) {

      if (certificateResult.includes(word)) {
        count++;
        console.log('Matched word : ' + word);
        this.matchedWords.push(word);
      }
    }

    if (this.matchedWords.length > 2) {

      console.log('Portugal Stpe 2 : ' + count + '  words matched with the Ocr Results, so the document is valid....');
      this.IsvalidationStep1 = true;
      return true;
    } else {
      console.log('Portugal Stpe 2 : ' + count + '  words not matched with the Ocr Results , so the document is invalid....')
      this.IsvalidationStep1 = false;
      return false;
    }
  }

  //Spain
  processSpainOcr(certificateResult: string) {

    // Count the matched words in the image source
    this.matchedWords.length = 0;
    let count = 0;
    let resultFlag = false;
    let scannedWordResult = [];
    certificateResult = certificateResult.replace(/[#_)(.\|,]/g, '');
    var sentencesp = certificateResult.split('\n');
    var sp = certificateResult.split(' ');
    //console.log(sp);
    console.log(sentencesp);
    for (var i = 0; i < sp.length; i++) {
      var sub = sp[i].split('\n');
      for (var j = 0; j < sub.length; j++) {
        scannedWordResult.push(sub[j]);
      }
    }
    //let scannedWordResult = certificateResult.split(' ');

    var first_logic_word_rangeValue = this.wordMatching(sentencesp, "firstlogic");
    console.log(first_logic_word_rangeValue);
    //console.log(scannedWordResult);
    if (((this.checkFirstLogic("copy", "for", "the", "company", scannedWordResult) || first_logic_word_rangeValue > 70)
      && ((certificateResult.toUpperCase().includes("SICK") && certificateResult.toUpperCase().includes("START") && certificateResult.toUpperCase().includes("NOTE"))
      || (certificateResult.toUpperCase().includes("SICK") && certificateResult.toUpperCase().includes("END") && certificateResult.toUpperCase().includes("NOTE"))
      || (certificateResult.toUpperCase().includes("SICK") && certificateResult.toUpperCase().includes("START-END") && certificateResult.toUpperCase().includes("NOTE"))
        || (certificateResult.toUpperCase().includes("FOLLOW-UP") && certificateResult.toUpperCase().includes("NOTE"))))
      || ((this.checkFirstLogic("ejemplar", "para", "la", "empresa", scannedWordResult) || first_logic_word_rangeValue > 70)
        && ((certificateResult.toUpperCase().includes("PARTE") && certificateResult.toUpperCase().includes("DE") && certificateResult.toUpperCase().includes("BAJA"))
      || (certificateResult.toUpperCase().includes("PARTE") && certificateResult.toUpperCase().includes("DE") && certificateResult.toUpperCase().includes("ALTA"))
      || (certificateResult.toUpperCase().includes("PARTE") && certificateResult.toUpperCase().includes("DE") && certificateResult.toUpperCase().includes("CONFIRMACIÓN"))
          || (certificateResult.toUpperCase().includes("PARTE") && certificateResult.toUpperCase().includes("DE") && certificateResult.toUpperCase().includes("BAJA/ALTA"))))
      || ((this.checkFirstLogic("exemplar", "para", "a", "empresa", scannedWordResult) || first_logic_word_rangeValue > 70)
        && ((certificateResult.toUpperCase().includes("PARTE") && certificateResult.toUpperCase().includes("DE") && certificateResult.toUpperCase().includes("BAIXA"))
      || (certificateResult.toUpperCase().includes("PARTE") && certificateResult.toUpperCase().includes("DE") && certificateResult.toUpperCase().includes("ALTA"))
      || (certificateResult.toUpperCase().includes("PARTE") && certificateResult.toUpperCase().includes("DE") && certificateResult.toUpperCase().includes("CONFIRMACIÓN"))
          || (certificateResult.toUpperCase().includes("PARTE") && certificateResult.toUpperCase().includes("DE") && certificateResult.toUpperCase().includes("BAIXA/ALTA"))))
      || ((this.checkFirstLogic("exemplar", "per", "a", "l'empresa", scannedWordResult) || first_logic_word_rangeValue > 70)
        && ((certificateResult.toUpperCase().includes("COMUNICAT") && certificateResult.toUpperCase().includes("DE") && certificateResult.toUpperCase().includes("BAIXA"))
      || (certificateResult.toUpperCase().includes("COMUNICAT") && certificateResult.toUpperCase().includes("DE") && certificateResult.toUpperCase().includes("D'ALTA"))
    ||(certificateResult.toUpperCase().includes("COMUNICAT") && certificateResult.toUpperCase().includes("DE") && certificateResult.toUpperCase().includes("CONFIRMACIÓ"))
          || (certificateResult.toUpperCase().includes("COMUNICAT") && certificateResult.toUpperCase().includes("BAIXA/D'ALTA"))))
    ) {
      resultFlag = true;
    }

    else {
      var second_logic_word_rangeValue = this.wordMatching(scannedWordResult, "secondlogic");
      // System.out.println("Word range value"+" "+first_logic_word_rangeValue + " Second logic range value "+ second_logic_word_rangeValue);
      if (first_logic_word_rangeValue > 70 && second_logic_word_rangeValue >= 90) {
        resultFlag = true;
      } else {

        resultFlag = false;
        count++;
      }

    }


    console.log(resultFlag);
    //if (unique2Chars.length >= 3) {
    if (resultFlag) {
      console.log('Spain Stpe 2 : ' + count + '  words matched with the Ocr Results, so the document is valid....');
      this.IsvalidationStep1 = true;
      return true;
    } else {
      console.log('Spain Stpe 2 : ' + count + '  words not matched with the Ocr Results , so the document is invalid....')
      this.IsvalidationStep1 = false;
      return false;
    }
  }

  checkFirstLogic(copy, fore, the, company, scannedWordResult) {
    if (scannedWordResult.length > 3) {
      for (let i = 0; i <= scannedWordResult.length - 4; i++) {   //
        if (scannedWordResult[i].toUpperCase().includes(copy.toUpperCase())) {
          console.log(copy.toUpperCase());
          if (scannedWordResult[i + 1].toUpperCase().includes(fore.toUpperCase()) && scannedWordResult[i + 2].toUpperCase().includes(the.toUpperCase())
            && scannedWordResult[i + 3].toUpperCase().includes(company.toUpperCase())
          ) {
            return true;
          }
        }
      }
    }
    return false;
  }
  //including the image into formData
  updateImageInFormDat() {

    this.isNotMatched = false;
    this.IsvalidationStep1 = true;
    this.IsvalidationStep2 = true;
    this.IsvalidationStep3 = true;

    this.formData.append('Image', this.selectedFiles, this.selectedFiles.name); //Send as  File.  TODO: Need to Encrypt
    this.employeeForm.get('attachment').setErrors(null);
    console.log(this.formData);
  }

  searchWordsInOcrResults() {

    // Count the matched words in the image source
    this.matchedWords.length = 0;
    let count = 0;
    for (const word of this.ocrMatchText) {

      if (this.certificateText.includes(word)) {
        count++;
        console.log('Matched word : ' + word);
        this.matchedWords.push(word);
      }
    }

    if (this.matchedWords.length > 5) {

      console.log('Stpe 2 : ' + count + '  words matched with the Ocr Results, so the document is valid....');
      this.IsvalidationStep1 = true;
      return true;
    } else {
      console.log('Stpe 2 : ' + count + '  words not matched with the Ocr Results , so the document is invalid....')
      this.IsvalidationStep1 = false;
      return false;
    }

  }

  validateOcrSecondStep(): Boolean {

    let count = 0;
    if (this.matchedWords.length > 0) {
      for (const actualWord of this.matchedWords) {

        if (actualWord == this.validText1 || actualWord == this.validText2) {
          count++;
          console.log('Matched word in step2 new : ' + actualWord);
        }

      }
      console.log('New Flow step2 matched word count (2): ' + count);
      if (count >= 1) {
        this.IsvalidationStep2 = true;

        return true;
      } else {
        this.IsvalidationStep2 = false;
        return false;
      }
    } else {
      return false;
    }

  }





  findWordSimilarity() {

    const words = this.certificateText.split(' ');

    console.log('words : ' + words);
    const similarityRes: any[] = [];

    for (const expected of this.ocrMatchText) {

      // tslint:disable-next-line: forin
      for (const actual of words) {


        const res = this.similarity(actual.replace(/[-_+.^:%$()?/><@!~`=,]/g, '').trim(), expected) * 100;


        if (res > 70.00) {

          console.log('Step3 : ' + actual + ' actual word matched  with ' + expected + ' % : ' + res);
          similarityRes.push(res);
          break;

        }


      }
    }

    // should be matched more than 5 words
    if (similarityRes.length > 5) {

      console.log('similarityRes length : ' + similarityRes.length);
      let finalRes = 0.0;

      for (let i = 0; i < similarityRes.length; i++) {

        console.log(' value : ' + similarityRes[i]);
        finalRes += similarityRes[i];
      }

      if ((finalRes / similarityRes.length) >= 70.00) {
        console.log('Document is valid....................');
        return true;
      }
      console.log('Step3 : Matched words in similarity (%) : ' + finalRes / similarityRes.length);

    } else {
      console.log('Step3 : Document is Invalid due to failing in words similarity (%)  !!!!!!!!');
      return false;
    }
  }

  similarity(s1: string, s2: string) {
    let longer = s1;
    let shorter = s2;
    // console.log('longer : ' + longer);
    // console.log('shorter : ' + longer);

    if (s1.length < s2.length) { // longer should always have greater length
      longer = s2; shorter = s1;
    }
    const longerLength = longer.length;
    if (longerLength === 0) { return 1.0; /* both strings are zero length */ }
    /* // If you have Apache Commons Text, you can use it to calculate the edit distance:*/

    const result = this.editDistance(longer, shorter);
    // console.log('result : ' + result);
    return (longerLength - result) / longerLength;

  }

  editDistance(s1: string, s2: string) {
    s1 = s1.toLowerCase();
    s2 = s2.toLowerCase();

    const costs: number[] = [s2.length + 1];
    // console.log('costs length : ' + costs.length);

    for (let i = 0; i <= s1.length; i++) {
      let lastValue: number = i;

      for (let j = 0; j <= s2.length; j++) {
        if (i === 0) {
          costs[j] = j;
        } else {
          if (j > 0) {
            let newValue = costs[j - 1];
            if (s1.charAt(i - 1) !== s2.charAt(j - 1)) {
              newValue = Math.min(Math.min(newValue, lastValue),
                costs[j]) + 1;
            }
            costs[j - 1] = lastValue;
            lastValue = newValue;
          }
        }
      }
      if (i > 0) {
        costs[s2.length] = lastValue;
      }
    }
    return costs[s2.length];
  }

  adjustDateForTimeOffset(dateToAdjust) {
    var offsetMs = dateToAdjust.getTimezoneOffset() * 60000;
    return new Date(dateToAdjust.getTime() - offsetMs);
  }

  imageDelete() {
    this.selectedFiles = null;
    this.employeeForm.get('attachment').setErrors({ required: true });
    this.employeeForm.get('attachment').setValidators(Validators.required);

    this.IsvalidationStep1 = true;
    this.IsvalidationStep2 = true;
    this.IsvalidationStep3 = true;
    this.isFileFormatAllowed = true;

  }

  extractFormControlValues(value: string) {
    console.log('extractFormControlValues : ' + value)

    if (value == 'fromForm') {
      this.associateDetails = new AssociateDetails();
      //this.associateDetails.Consent = this.privacyAgreement;
      this.associateDetails.AssociateNumber = this.employeeForm.get('employeeNumber').value;
      this.associateDetails.AssociateFirstName = this.employeeForm.get('employeeFirstName').value;
      this.associateDetails.AssociateLastName = this.employeeForm.get('employeeLastName').value;
      this.associateDetails.AssociateEmail = this.employeeForm.get('email').value;
      //this.associateDetails.AssociateMobileNumber = '+49 ' + this.formDate.value.slice(0, 3) + ' ' + this.formDate.value.slice(3, 15);
      //this.associateDetails.AssociateMobileNumber = this.formDate.value;
      this.associateDetails.AssociateMobileNumber = this.employeeForm.get('mobileNumber').value;
      console.log('fromForm : MobileNumber : ' + this.associateDetails.AssociateMobileNumber);
      this.associateDetails.DataPrivacyTimeStamp = this.DataPrivacyTimeStamp;
      //this.associateDetails.SessionKey = localStorage.getItem('SessionKey');
      //console.log('SessionKey' + this.associateDetails.SessionKey);
      //this.employeeForm.get('mobileNumber').value;
      this.associateDetails.WorkDisabilityCertificateDetails = new WorkDisabilityCertificate();
      this.associateDetails.WorkDisabilityCertificateDetails.FirstTimeSickness = this.employeeForm.get('initialIllness').value;
      this.associateDetails.WorkDisabilityCertificateDetails.FollowUpSickness = this.employeeForm.get('recurringIllness').value;
      this.associateDetails.WorkDisabilityCertificateDetails.Accident = this.employeeForm.get('accident').value;
      this.associateDetails.WorkDisabilityCertificateDetails.Covid = this.employeeForm.get('covid').value;
      this.associateDetails.WorkDisabilityCertificateDetails.PartTime = this.employeeForm.get('partTime').value;
      // this.associateDetails.WorkDisabilityCertificateDetails.StartDate = (this.employeeForm.get('fromDate').value);
      //this.associateDetails.WorkDisabilityCertificateDetails.EndDate = (this.employeeForm.get('toDate').value);

      this.associateDetails.WorkDisabilityCertificateDetails.StartDate = this.fromDateStr;
      this.associateDetails.WorkDisabilityCertificateDetails.EndDate = this.toDateStr;
      this.associateDetails.WorkDisabilityCertificateDetails.FollowupDate = this.followDateStr;
    } else {
      this.associateDetails = this.getEncryptedFormData();
    }


    this.appService.formData = this.associateDetails;

  }
  getEncryptedFormData(): AssociateDetails {
    let keyStr = localStorage.getItem(this.SECRET_KEY);
    console.log('SecretKey : ' + keyStr);
    const key = atob(keyStr);
    console.log('keyStr : ' + key);
    this.associateDetails = new AssociateDetails();
    //this.associateDetails.Consent = this.privacyAgreement;

    let email = this.employeeForm.get('email').value;
    console.log("email : " + email);
    if (typeof email != 'undefined' && email) {
      console.log("If-Block email : " + email);
      this.associateDetails.AssociateEmail = this.encService.set(key, email);
    } else {
      console.log("Else block email : " + email);
      this.associateDetails.AssociateEmail = '';
    }

    //let phone = this.formDate.value.slice(0, 3) + ' ' + this.formDate.value.slice(3, 15);
    //let phone = this.formDate.value;
    //console.log(" Phone number....................... : " + phone);
    //if (typeof phone != 'undefined' && phone) {
    //  console.log(" IF block phone : " + phone);
    //  this.associateDetails.AssociateMobileNumber = this.encService.set(key, phone);
    //} else {
    //  console.log("ELSE block phone : " + phone);
    //  this.associateDetails.AssociateMobileNumber = '';
    //}

    this.associateDetails.AssociateMobileNumber = this.encService.set(key, this.employeeForm.get('mobileNumber').value);
    console.log(" Phone number....................... : " + this.associateDetails.AssociateMobileNumber);
    this.associateDetails.AssociateNumber = this.encService.set(key, this.employeeForm.get('employeeNumber').value);
    this.associateDetails.AssociateFirstName = this.encService.set(key, this.employeeForm.get('employeeFirstName').value);
    this.associateDetails.AssociateLastName = this.encService.set(key, this.employeeForm.get('employeeLastName').value);
    this.associateDetails.DataPrivacyTimeStamp = this.encService.set(key, this.DataPrivacyTimeStamp);
    this.associateDetails.SelectedCountryCode = this.utilityService.selectedCountryCode;
    if (this.utilityService.selectedCountryCode == environment.COUNTRY_CODE_PORTUGAL) {
      this.associateDetails.IsOcrCheck = this.portugalOcrFalg;
    } else if (this.utilityService.selectedCountryCode == environment.COUNTRY_CODE_FRANCE) {
      this.associateDetails.IsOcrCheck = this.franceOcrFalg;
    } else if (this.utilityService.selectedCountryCode == environment.COUNTRY_CODE_SPAIN) {
      this.associateDetails.IsOcrCheck = this.spainOcrFalg;
      console.log(" First Name....................... : " + this.employeeForm.get('employeeFirstName').value.toLowerCase());
      console.log(" Last Name....................... : " + this.employeeForm.get('employeeLastName').value.toLowerCase());
      this.associateDetails.AssociateFirstName = this.encService.set(key, this.employeeForm.get('employeeFirstName').value.toLowerCase());
      this.associateDetails.AssociateLastName = this.encService.set(key, this.employeeForm.get('employeeLastName').value.toLowerCase());

    } else {
      this.associateDetails.IsOcrCheck = this.germanyOcrFalg;
    }
    //this.associateDetails.SessionKey = localStorage.getItem('SessionKey');
    //console.log('SessionKey' + this.associateDetails.SessionKey);
    //this.employeeForm.get('mobileNumber').value;

    console.log('Initial Illness Value : ' + this.employeeForm.get('initialIllness').value);
    console.log('FollowUpSickness Value : ' + this.employeeForm.get('recurringIllness').value);
    this.associateDetails.WorkDisabilityCertificateDetails = new WorkDisabilityCertificate();
    this.associateDetails.WorkDisabilityCertificateDetails.FirstTimeSickness = this.encService.set(key, this.employeeForm.get('initialIllness').value);
    this.associateDetails.WorkDisabilityCertificateDetails.FollowUpSickness = this.encService.set(key, this.employeeForm.get('recurringIllness').value);
    this.associateDetails.WorkDisabilityCertificateDetails.Accident = this.encService.set(key, this.employeeForm.get('accident').value);
    var fromDateEn = '';
    if (this.fromDateStr != '' && this.fromDateStr != null) {
      fromDateEn = this.encService.set(key, this.fromDateStr);
    }
    this.associateDetails.WorkDisabilityCertificateDetails.StartDate = fromDateEn;
    var toDateEn = '';
    if (this.toDateStr != '' && this.toDateStr != null) {
      toDateEn = this.encService.set(key, this.toDateStr);
    }
    this.associateDetails.WorkDisabilityCertificateDetails.EndDate = toDateEn;// this.encService.set(key, this.toDateStr);
    //this.associateDetails.WorkDisabilityCertificateDetails.StartDate = this.encService.set(key, (this.employeeForm.get('fromDate').value));
    //this.associateDetails.WorkDisabilityCertificateDetails.EndDate = this.encService.set(key, (this.employeeForm.get('toDate').value));
    var followupDateEn = '';
    if (this.followDateStr != '' && this.followDateStr != null) {
      followupDateEn = this.encService.set(key, this.followDateStr);
    }
    this.associateDetails.WorkDisabilityCertificateDetails.FollowupDate = followupDateEn;//this.encService.set(key, this.followDateStr);
    console.log('Selected Illness Type : ' + this.selectedIllnessType);
    if (this.selectedIllnessType != '') {
      this.associateDetails.WorkDisabilityCertificateDetails.IllnessType = this.encService.set(key, this.selectedIllnessType);
    }
    if (this.selectedRequestType != '') {
      this.associateDetails.WorkDisabilityCertificateDetails.RequestType = this.encService.set(key, this.selectedRequestType);
    }
    console.log('Selected Illness Type : ' + this.selectedKindofIllness);
    if (this.selectedKindofIllness != '') {
      this.associateDetails.WorkDisabilityCertificateDetails.KindofIllness = this.encService.set(key, this.selectedKindofIllness);
    }
    console.log('Selected Country Code : ' + this.utilityService.selectedCountryCode);

    if (this.utilityService.selectedCountryCode == environment.COUNTRY_CODE_FRANCE) {

      console.log('Hospitalization Value' + this.employeeForm.get('hospitalization').value);
      console.log('Covid Value' + this.employeeForm.get('covid').value);
      console.log('partTime Value' + this.employeeForm.get('partTime').value);
      this.associateDetails.WorkDisabilityCertificateDetails.Hopitalization = this.encService.set(key, this.employeeForm.get('hospitalization').value);
      this.associateDetails.WorkDisabilityCertificateDetails.Covid = this.encService.set(key, this.employeeForm.get('covid').value);
      this.associateDetails.WorkDisabilityCertificateDetails.PartTime = this.encService.set(key, this.employeeForm.get('partTime').value);
    }
    else if (this.utilityService.selectedCountryCode == environment.COUNTRY_CODE_SPAIN) {

      if (this.IsChecked) {
        this.associateDetails.WorkDisabilityCertificateDetails.AgreeContactDataTimeStamp = "Date";
      } else {
        this.associateDetails.WorkDisabilityCertificateDetails.AgreeContactDataTimeStamp = '';
      }

    }

    return this.associateDetails;
  }
  resetForm() {

    this.employeeForm.reset();
    this.selectedFiles = null;
    this.isFileFormatAllowed = true;
    this.associateDetails = new AssociateDetails();

  }
  refreshPage() {
    window.location.reload();
  }

  openAgreement() {
    this.dialog.open(AgreeDiaglogComponent, {
      width: '900px',

      disableClose: true,


      data: { action: 1, data: '' }
    }).afterClosed().subscribe(data => {
      if (data && data.action === 1) {
        this.DataPrivacyTimeStamp = data.data;

        console.log('form-container : agreement-close : ' + this.DataPrivacyTimeStamp);
      } else {
        var d = new Date();
        var time = '' + d.getTime();
        this.DataPrivacyTimeStamp = time;
      }
    });


    this.IsAgree = false;
    this.SpainDateValidate();
    console.log(this.isSpainDateNotValid+"_"+this.employeeForm.invalid + "_" + this.isNotMatched + "_" +
      ((this.IsEmailHint + "_" + this.IsMobileNumberHint) && !this.IsChecked) + "_" + this.IsRequired + "_" + this.IsAgree + "_" + (!this.recurringIllness && !this.initialIllness)
      + "_" + this.frDateValidation + "_" + this.poDateValidation + "_" + (this.utilityService.selectedCountry == 'portugal' && this.illnessTypeNotSelected))
  }


  setAccident() {
    setTimeout(() => {
      if (this.employeeForm.get('accident').value) {
        this.accident = true;
        this.hospitalization = false;
        this.covid = false;
        this.partTime = false;
        this.employeeForm.get('hospitalization').setValue(false);
        this.employeeForm.get('partTime').setValue(false);
        this.employeeForm.get('covid').setValue(false);
      }
      if (this.employeeForm.get('accident').value && !this.employeeForm.get('initialIllness').value && !this.employeeForm.get('recurringIllness').value)
        this.IsRequired = true;
      else
        this.IsRequired = false;


    }, 100);
  }

  setHospitalization() {
    setTimeout(() => {
      if (this.employeeForm.get('hospitalization').value) {
        this.accident = false;
        this.covid = false;
        this.partTime = false;
        this.hospitalization = true;
        this.employeeForm.get('accident').setValue(false);
        this.employeeForm.get('partTime').setValue(false);
        this.employeeForm.get('covid').setValue(false);
      }

      if (this.employeeForm.get('hospitalization').value && !this.employeeForm.get('initialIllness').value && !this.employeeForm.get('recurringIllness').value)
        this.IsRequired = true;
      else
        this.IsRequired = false;
    }, 100);
  }

  setCovid() {
    setTimeout(() => {
      if (this.employeeForm.get('covid').value) {
        this.accident = false;
        this.hospitalization = false;
        this.partTime = false;
        this.covid = true;
        this.employeeForm.get('hospitalization').setValue(false);
        this.employeeForm.get('accident').setValue(false);
        this.employeeForm.get('partTime').setValue(false);
      }

      if (this.employeeForm.get('covid').value && !this.employeeForm.get('initialIllness').value && !this.employeeForm.get('recurringIllness').value)
        this.IsRequired = true;
      else
        this.IsRequired = false;
      }, 100);
  }

  setPartTime() {
    setTimeout(() => {
      if (this.employeeForm.get('partTime').value) {
        this.accident = false;
        this.hospitalization = false;
        this.covid = false;
        this.partTime = true;
        this.employeeForm.get('accident').setValue(false);
        this.employeeForm.get('covid').setValue(false);
        this.employeeForm.get('hospitalization').setValue(false);
      }

      if (this.employeeForm.get('partTime').value && !this.employeeForm.get('initialIllness').value && !this.employeeForm.get('recurringIllness').value)
        this.IsRequired = true;
      else
        this.IsRequired = false;
    }, 100);
  }

  validateMobileBox(searchValue: string) {
    if (searchValue.length == 0) {
      //this.IsMobileNumberValid = false;
      this.IsMobileNumberHint = false;
      this.IsChecked = false;
    }
    else if (searchValue.length < 11) {
      //this.IsMobileNumberValid = true;
      this.IsMobileNumberHint = true;
    }
    else {
      // this.IsMobileNumberValid = false;
      this.IsMobileNumberHint = true;
    }


    console.log("validateMobileBox :IsMobileNumberHint : " + this.IsMobileNumberHint)
  }

  validateEmailBox(searchValue: string) {
    if (searchValue.length > 0) {
      this.IsEmailHint = true;
    } else {
      this.IsEmailHint = false;
      this.IsChecked = false;
    }
  }

  //AgreeCheckbox(event: MatCheckboxChange) {

  //  if (event.checked) {
  //    this.privacyAgreement = true;
  //    this.IsChecked = true;
  //    // this.translateService.use(lang); // changing ngx-translate language
  //    console.log('Checked : ' + this.privacyAgreement);
  //  } else {
  //    this.privacyAgreement = false;
  //    this.IsChecked = false;
  //    console.log('UnChecked : ' + this.privacyAgreement);
  //  }
  //}

  AgreeCheckbox(value) {

    if (!value) {
      // this.privacyAgreement = true;
      this.IsChecked = true;
      // this.translateService.use(lang); // changing ngx-translate language
      console.log('Checked : ' + this.privacyAgreement);
    } else {
      // this.privacyAgreement = false;
      this.IsChecked = false;
      console.log('UnChecked : ' + this.privacyAgreement);
    }
  }

  onIllnessTypeChange(value) {

    this.selectedIllnessType = value;

    if (!this.employeeForm.get('initialIllness').value && !this.employeeForm.get('recurringIllness').value)
      this.IsRequired = true;
    else
      this.IsRequired = false;

    this.illnessTypeNotSelected = false;
    this.tempIllnessTypeNotSelected = false;
    console.log('Selected Illnesstype : ' + this.selectedIllnessType);


  }
  onKindofIllnessChange(value) {
    if (value != null && value != "") {

      this.kindofillnessNotSelected = false;
    } else {

      this.kindofillnessNotSelected = true;
    }
    this.selectedKindofIllness = value;

    //if (!this.employeeForm.get('initialIllness').value && !this.employeeForm.get('recurringIllness').value)
    //  this.IsRequired = true;
    //else
      this.IsRequired = false;
    this.recurringIllness = true;
    this.initialIllness = false;
    //this.illnessTypeNotSelected = false;
    //this.tempIllnessTypeNotSelected = false;
    console.log('Selected Illnesstype : ' + this.selectedKindofIllness);


  }
  onRequestTypeChange(value) {

    this.selectedRequestType = value;
    this.employeeForm.get('fromDate').setValue('');
    this.employeeForm.get('toDate').setValue('');
    this.employeeForm.get('followupDate').setValue('');
    this.followDateStr = '';
    this.fromDateStr = '';
    this.toDateStr = '';
    console.log(this.employeeForm);
    console.log(this.selectedRequestType);
    console.log('fromDateStr : ' + this.fromDateStr);
    //this.employeeForm.controls['fromData'].setValue('');
    //this.employeeForm.controls['toDate'].setValue('');
    //this.employeeForm.controls['followupDate'].setValue('');

    if (this.selectedRequestType != null && this.selectedRequestType != " ") {

      this.selectedRequestType = this.selectedRequestType.trim();
      if (this.selectedRequestType == "Sickness start note" || this.selectedRequestType == "Sick start note" || this.selectedRequestType == "PARTE BAJA") {
        this.isStartDate = true;
        this.isEndDate = false;
        this.isFollowupDate = false;
      } else if (this.selectedRequestType == "Sickness end note" || this.selectedRequestType == "Sick end note" || this.selectedRequestType == "PARTE ALTA") {
        this.isStartDate = false;
        this.isEndDate = true;
        this.isFollowupDate = false;
        this.maxDate = new Date(2000,1,1);
      } else if (this.selectedRequestType == "Short leave note" || this.selectedRequestType == "Sick start-end note" || this.selectedRequestType == "PARTE BAJA-ALTA") {
        this.isStartDate = true;
        this.isEndDate = true;
        this.isFollowupDate = false;
      } else if (this.selectedRequestType == "Follow up note" || this.selectedRequestType == "PARTE CONFIRMACIÓN") {
        this.isStartDate = false;
        this.isEndDate = false;
        this.isFollowupDate = true;
      } else {
        this.isStartDate = true;
        this.isEndDate = true;
        this.isFollowupDate = true;
      }
    }
    else {
      this.resetDatefields();
    }
    this.requestTypeNotSelected = false;
    this.tempRequestTypeNotSelected = false;
    console.log('Selected RequestType : ' + this.selectedRequestType);

    this.SpainDateValidate();
  }

  resetDatefields() {
    this.isStartDate = false;
    this.isEndDate = false;
    this.isFollowupDate = false;
  }

  //OCR Similarity Logic

  wordMatching(ArrayCertText, logic) {
    var hasParte = false;
    var ocrMatchText1 = [];

    if (logic.includes("firstlogic")) {
      //["COPY", "OF", "THE", "COMPANY", "EJEMPLAR", "PARA", "LA", "EMPRESA", "EXEMPLAR", "A", "PER", "LEMPRESA"];
      ocrMatchText1 = ["EJEMPLAR PARA LA EMPRESA", "EXEMPLAR PER A L'EMPRESA", "EXEMPLAR PARA A EMPRESA", "COPY OF THE COMPANY"];
    } else if (logic.includes("secondlogic")) {
      ocrMatchText1 = ["SICK", "START", "NOTE", "END", "FOLLOWUP", "STARTEND", "PARTE", "DE", "BAJA", "ALTA", "BAJAALTA", "CONFIRMACION", "BAIXA", "BAIXAALTA", "COMUNICAT", "DALTA", "BAIXADALTA", "CONFIRMACIO"];
      //["parte", "de", "baja", "alta", "baja/alta", "baixa", "baixa/alta", "comunicat", "sick", "start", "end", "note", "follow-up"];
    }
    var finalRes = 0.0;
    var array = [];
    for (var j = 0; j < ArrayCertText.length; j++) {
      array[j] = ArrayCertText[j];
    }
    var words1 = array;

    var similarityRes1 = [];
    for (var j = 0; j < ocrMatchText1.length; j++) {
      for (var i = 0; i < words1.length; i++) {
        var res = this.similarity(words1[i].toUpperCase(), ocrMatchText1[j].toUpperCase()) * 100;
        //console.log(res);
        if (res > 70.00) {
          console.log(words1[i] + " matched  with " + ocrMatchText1[j] + " % : " + res);
          if (words1[i].toUpperCase().includes("PARTE")) {
            hasParte = true;
          }
          similarityRes1.push(res);
          break;

        }
      }
    }
    if (logic.includes("firstlogic")) {
      if (similarityRes1.length >= 1) {
        console.log(similarityRes1.length);
        for (let i = 0; i < similarityRes1.length; i++) {

          console.log(' value : ' + similarityRes1[i]);
          finalRes += similarityRes1[i];
        }
      }
    } else if (logic.includes("secondlogic")) {
      if (hasParte) {
        if (similarityRes1.length >= 3) {
          console.log(similarityRes1.length);
          for (let i = 0; i < similarityRes1.length; i++) {

            console.log(' value : ' + similarityRes1[i]);
            finalRes += similarityRes1[i];
          }
        }
      } else {
        if (similarityRes1.length >= 2) {
          console.log(similarityRes1.length);
          for (let i = 0; i < similarityRes1.length; i++) {

            console.log(' value : ' + similarityRes1[i]);
            finalRes += similarityRes1[i];
          }
        }
      }
    }
    if (similarityRes1.length > 0) {
      return finalRes / similarityRes1.length;
    } else {
      return 0;
    }


  }

  SpainDateValidate() {

    var count = 0;
    this.isSpainDateNotValid = false;
    console.log(this.isStartDate + '_' + this.isEndDate + '_' + this.isFollowupDate)
    console.log(this.fromDateStr + '_' + this.toDateStr + '_' + this.toDateStr)
    if (this.isStartDate && this.isEndDate) {
      if (this.fromDateStr == '' || this.toDateStr == '') {
        this.isSpainDateNotValid = true;
        count++;
      }
    }
    if (this.isStartDate && count==0) {
      if (this.fromDateStr == '') {
        this.isSpainDateNotValid = true;
        count++;
      }
    }
    if (this.isEndDate && count == 0) {
      if (this.toDateStr == '') {
        this.isSpainDateNotValid = true;
        count++;
      }
    }
    if (this.isFollowupDate && count == 0) {
      if (this.followDateStr == '') {
        this.isSpainDateNotValid = true;
        count++;
      }
    }
    console.log(this.requestTypeNotSelected + '_' + this.kindofillnessNotSelected + '_' +  this.isSpainDateNotValid);
  }

}

