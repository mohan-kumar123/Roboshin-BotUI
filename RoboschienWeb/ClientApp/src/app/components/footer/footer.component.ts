import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { DateAdapter } from '@angular/material';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {

  selectedlanguage: string;
  selectedLanguageName: string;
  lang: string = 'en';

  languages = [{
    'language_abbrevation': 'en',
    'language_name': 'English'
  },
  {
    'language_abbrevation': 'de',
    'language_name': 'Deutsch'
  }]
  constructor(public translate: TranslateService, private adapter: DateAdapter<Date>) {
    let localLang = localStorage.getItem("lang");
    this.selectedlanguage = localLang == null ? this.lang : localLang;
    //this.selectedLanguageName = localLang == null ? 'English' : this.languages.find(m => m.language_abbrevation == localLang).language_name;
    this.selectedlanguage == null ? this.translate.use("en") : this.translate.use(this.selectedlanguage);
    console.log('Footer Component  constructor: ' + localLang);
  }

  ngOnInit() {
    let localLang = localStorage.getItem("lang");
    let lang = localLang == null ? this.lang : localLang;
    console.log('Footer Component  ngOnInit: ' + localLang);

    this.selectedlanguage = localLang == null ? this.lang : localLang;
    this.selectedlanguage == null ? this.translate.use("en") : this.translate.use(this.selectedlanguage);

    var script = document.createElement('script');
    script.src = "https://www.google.com/recaptcha/api.js?hl=" + lang;
    document.head.appendChild(script); //or something of the likes
  }

  imprintsClicked() {

    console.log('Footer : imprints clicked');
  }
}
