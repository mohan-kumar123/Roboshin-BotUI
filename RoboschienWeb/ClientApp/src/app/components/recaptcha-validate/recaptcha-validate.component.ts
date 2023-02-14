import { Component, OnInit, Inject, ViewChild, ElementRef } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatIconRegistry } from '@angular/material';

import { NgForm } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { EmailService } from 'src/app/services/emailservice/email.service';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-recaptcha-validate',
  templateUrl: './recaptcha-validate.component.html',
  styleUrls: ['./recaptcha-validate.component.css']
})
export class ReCaptchaValidateComponent implements OnInit {

 // captchaApiUrl = "/DNTCaptchaApi/CreateDNTCaptcha";
  //captcha = new DNTCaptchaBase();
  validationErrorMessage: string;
  success: boolean;
  IsShowButton: boolean=true;

  @ViewChild('recaptcha') recaptchaElement: ElementRef;
  @ViewChild('btnClick') btnClick: ElementRef;





  constructor(public dialogRef: MatDialogRef<ReCaptchaValidateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

  ngOnInit() {
    this.addRecaptchaScript();
  }

  renderReCaptch() {
    window['grecaptcha'].render(this.recaptchaElement.nativeElement, {
      'sitekey': '6LfdPM0bAAAAAEoM-UTPNagsEeocqJcug2HXdezx',
      //'sitekey': '6Lc_qsMUAAAAABxH8jnJ3AWTcMMMwq8wlZsMuMJS', //DE
      'callback': (response) => {
        console.log("Captcha Response : " + response);
       
        this.data.CaptchaResponse = response;
        this.data.IsCaptchaValidated = true;
       
       // this.dialogRef.close(this.data);
        this.closeDialog();

       // this.btnClick.nativeElement.di
      }
    });
   // this.IsShowButton = true;
  }
  closeDialog(): any {
    console.log("close dialog Called.... ");
   
    let elem = document.getElementById('btnClick');

    let evt = new MouseEvent('click', {
      bubbles: true,
      cancelable: true,
      view: window
    });

    elem.dispatchEvent(evt);
  }

  onClick() {
    console.log("onClick method Called.... ");

    this.dialogRef.close(this.data); 
  }

  addRecaptchaScript() {

    window['grecaptchaCallback'] = () => {
      this.renderReCaptch();
    }

    (function (d, s, id, obj) {
      var js, fjs = d.getElementsByTagName(s)[0];
      if (d.getElementById(id)) { obj.renderReCaptch(); return; }
      js = d.createElement(s); js.id = id;
      js.src = "https://www.google.com/recaptcha/api.js?onload=grecaptchaCallback&amp;render=explicit";
      fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'recaptcha-jssdk', this));

  }
}

export interface DialogData {
  IsCaptchaValidated: boolean;
  CaptchaResponse: string;
}
