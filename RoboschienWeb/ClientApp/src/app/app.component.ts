import {
  MatDatepickerModule,
  MatNativeDateModule,
  DateAdapter
} from "@angular/material";
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { DateFormat } from './date-format';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [
     {provide: DateAdapter, useClass: DateFormat}
  ]
})


export class AppComponent {

  selectedlanguage: string;
   lang: string = 'en';

  constructor(public translate: TranslateService,private dateAdapter: DateAdapter<Date>) {


    //if (lang == null || lang == undefined) {
    //  translate.addLangs(['en', 'de']);
    //}


   // translate.setDefaultLang('en');
    ///localStorage.setItem("lang","en");

    const browserLang = translate.getBrowserLang();
    console.log("AppComponent : Actaul Browser Language : " + browserLang);


    let currentLang = localStorage.getItem("lang");
    console.log("AppComponent : LocalStorage Language : " + currentLang);
   
    this.selectedlanguage = currentLang == null ? this.lang : currentLang;
    translate.use(this.selectedlanguage);

    //translate.use(browserLang.match(/en|de/) ? browserLang : 'en');
    console.log("AppComponent : Changed Browser Language : " + this.selectedlanguage);
    // Date picker
    
    if (currentLang == null || currentLang == undefined){
      dateAdapter.setLocale("en");
      localStorage.setItem("lang", "en");
    }else{
      dateAdapter.setLocale(currentLang);
      localStorage.setItem("lang", currentLang);
    }
    
   

    var script = document.createElement('script');
    script.src = "https://www.google.com/recaptcha/api.js?hl=" + this.selectedlanguage;
    document.head.appendChild(script); 

    // window.onbeforeunload = function(e) {
    //   localStorage.removeItem("lang");
    //   return 'Dialog text here.';
    // };
  }
}
