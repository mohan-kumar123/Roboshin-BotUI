import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import {DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE} from '@angular/material/core';
import { Route, Router, NavigationEnd } from '@angular/router';
import 'rxjs/add/operator/filter';
import { RouteUrl } from 'src/app/models/RouteUrl';
import { filter } from 'rxjs-compat/operator/filter';
import { HelpContentComponent } from '../help-content/help-content.component';
import { MatDialog } from '@angular/material/dialog';
import { UtilityService } from 'src/app/services/utility.service';
import { CountrySelectionComponent } from '../country-selection/country-selection.component';
import { environment } from '../../../environments/environment';
declare var $: any;

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  selectedlanguage: string;
  selectedLanguageName: string;
  lang : string = 'en';

  languages = [{
    'language_abbrevation': 'en',
    'language_name':'English'
  },
    {
      'language_abbrevation': 'de',
      'language_name': 'Deutsch'
    }]

  constructor(public translate: TranslateService, private adapter: DateAdapter<Date>,
    private router: Router, public dialog: MatDialog,
    public utilityService: UtilityService) {
   let localLang = localStorage.getItem("lang");
    this.selectedlanguage = localLang == null ? this.lang : localLang;
    //this.selectedLanguageName = localLang == null ? 'English' : this.languages.find(m => m.language_abbrevation == localLang).language_name;
    this.selectedlanguage == null ? this.translate.use("en") : this.translate.use(this.selectedlanguage);
    console.log('Header constructor : loalLang : ' + localLang);

  }
  urlValue: string = '/home';
  //onChange(selectedlang, languageName) {
  //  this.selectedLanguageName = languageName;
  //  this.translate.use(selectedlang);
  //  this.selectedlanguage = selectedlang;
  //  this.lang = this.selectedlanguage;
  //  localStorage.setItem("lang", this.selectedlanguage);

  //}

  ngOnInit() {
    let localLang = localStorage.getItem("lang");
    let lang = localLang == null ? this.lang : localLang;

    console.log('loalLang : ' + localLang);
    console.log('lang  : ' + lang)
    
    var script = document.createElement('script');
    script.src = "https://www.google.com/recaptcha/api.js?hl=" + lang;
    document.head.appendChild(script); //or something of the likes
    //console.log('Header : URL :' + this.router.url);
    //console.log('Header : URL :' + this.router.routerState.snapshot.url);

  
   
    this.router.events.filter(event => event instanceof NavigationEnd)
      .subscribe((event: NavigationEnd) => {

        console.log('Before If URL : '+event.url);
        this.urlValue = event.url;
        if (this.urlValue == '/') {
          this.urlValue = event.urlAfterRedirects;
          console.log('IF : url:' + event.urlAfterRedirects);
        }
        //if (event instanceof RouteUrl) {
        //  this.urlValue = event.url;
        //  console.log('url:'+event.urlAfterRedirects);
        //}
      });

  }


  onChange(selectedlang) {
    this.translate.use(selectedlang);
    this.selectedlanguage = selectedlang;
    this.lang = this.selectedlanguage;

    localStorage.setItem("lang", selectedlang);
    this.utilityService.selectedLang = selectedlang;

    console.log('Header Component onChange this.lang: ' + this.lang);
    console.log('Header Component onChange selectedlang: ' + selectedlang);
    
    this.utilityService.fetchIllnessTypes(this.utilityService.selectedCountryCode,this.lang);
    /*for Spain*/
    this.utilityService.fetchRequestTypes(this.lang);

  }

  openHelpContent(): void {
    const dialogRef = this.dialog.open(HelpContentComponent, {
      width: '1000px',
      height: '700px'
     // data: { name: this.hero.name }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      //if (result) {
      //  this.hero.name = result;
      //}
    });
  }

  onChangeCountry(): void {
    this.utilityService.CountrychangeEmit();
    this.dialog.open(CountrySelectionComponent, {
      width: '600px',
      height: '400px',
      disableClose: true,
      data: { action: 1, data: '' }
    }).afterClosed().subscribe(data => {

      console.log('CountrySelectionComponent : countryselection-close : ' + data.data);

      if (data && data.action === 1) {
      
        console.log('CountrySelectionComponent : countryselection-close : ' + data.data);


      } 
   
      if (this.utilityService.selectedCountry == environment.COUNTRY_NAME_PORTUGAL) {
        this.utilityService.setCountryCode(environment.COUNTRY_CODE_PORTUGAL);
        this.utilityService.set(environment.COUNTRY_NAME_PORTUGAL);

        this.utilityService.fetchIllnessTypes(this.utilityService.selectedCountryCode, this.utilityService.selectedLang);

      } else if (this.utilityService.selectedCountry == environment.COUNTRY_NAME_FRANCE) {
        this.utilityService.setCountryCode(environment.COUNTRY_CODE_FRANCE);
        this.utilityService.set(environment.COUNTRY_NAME_FRANCE);

      } else if (this.utilityService.selectedCountry == environment.COUNTRY_NAME_SPAIN) {
        this.utilityService.setCountryCode(environment.COUNTRY_CODE_SPAIN);
        this.utilityService.set(environment.COUNTRY_NAME_SPAIN);

        this.utilityService.fetchRequestTypes(this.utilityService.selectedLang);
      }

      // else {
      //  this.utilityService.setCountryCode(environment.COUNTRY_CODE_GERMANY);
        
      //}

     // location.reload();
    });

  
  }

}
