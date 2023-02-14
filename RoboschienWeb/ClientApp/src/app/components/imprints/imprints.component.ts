import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { environment } from '../../../environments/environment.prod';
import { UtilityService } from '../../services/utility.service';
//import { UtilsService } from 'src/app/utils.service';

@Component({
  selector: 'app-imprints',
  templateUrl: './imprints.component.html',
  styleUrls: ['./imprints.component.scss']
})
export class ImprintsComponent implements OnInit {

  fossLisenceTitle = 'FOSS Licenses';
  fossLisenceDescription = 'FOSS Licenses Details';

  temp: string = " ";
  titles: string[] = ['Corporate information', 'Legal notice', 'Data protection notice', this.fossLisenceTitle];
  descriptions: string[] = ['Responsible for the internet pages of Robert Bosch GmbH',
    'For users of the internet pages of Robert Bosch GmbH', 'Data protection notice of Robert Bosch GmbH',
    this.fossLisenceDescription];

  titlesDe: string[] = ['Impressum', 'Rechtliche Hinweise', 'Datenschutzhinweise', this.fossLisenceTitle];
  descriptionsDe: string[] = ['Verantwortlich für die Internetseiten der Robert Bosch GmbH', 'Rechtliche Hinweise der Robert Bosch GmbH',
    'Datenschutzhinweise der Robert Bosch GmbH', this.fossLisenceDescription];


  titlesFr: string[] = ['Information corporate', 'Informations légales', 'Information sur la protection des données', this.fossLisenceTitle];
  descriptionsFr: string[] = ['Responsable des pages Internet de Robert Bosch GmbH',
    'Pour les utilisateurs des pages Internet de Robert Bosch GmbH',
    'Information sur la protection des données de Robert Bosch GmbH',
    this.fossLisenceDescription];

  titlesPo: string[] = ['Informação Corporativa', 'Informação legal', 'Informação de proteção de dados', this.fossLisenceTitle];
  descriptionsPo: string[] = ['Responsável pelas páginas web da Robert Bosch GmbH', 'Para utilizadores de páginas web da Robert Bosch GmbH',
    'Informação de proteção de dados da Robert Bosch GmbH', this.fossLisenceDescription];

  titlesEs: string[] = ['Informação Corporativa', 'Informação legal', 'Informação de proteção de dados', this.fossLisenceTitle];
  descriptionsEs: string[] = ['Responsável pelas páginas web da Robert Bosch GmbH', 'Para utilizadores de páginas web da Robert Bosch GmbH',
    'Informação de proteção de dados da Robert Bosch GmbH', this.fossLisenceDescription];

  title: string = this.titles[0];
  description: string = this.descriptions[0];

  //titleDe: string = this.titlesDe[0];
  //descriptionDe: string = this.descriptionDe[0];


  constructor(private translate: TranslateService, public utilityService: UtilityService) { }

  isEnglish: Boolean = false;
  selectedlanguage: string;
  selectedCountry: string;


  ngOnInit() {

    this.selectedlanguage = this.utilityService.getSelectedCountryCode();

    if (this.selectedlanguage == undefined) {
      this.selectedlanguage = localStorage.getItem("lang");
    }

    //if (this.selectedlanguage == "en" || this.selectedlanguage == null) {
    //  this.isEnglish = true;
    //  this.title = this.titles[0];
    //  this.description = this.descriptions[0];
    //} else {
    //  this.title = this.titlesDe[0];
    //  this.description = this.descriptionsDe[0];
    //}
    console.log('imprints component Selected language : ' + this.selectedlanguage);
    console.log('imprints component Selected language From service : ' + this.utilityService.selectedLang);

    console.log('imprints component Selected Country Code : ' + this.utilityService.selectedCountryCode);

    console.log('utilityService.selected Country Code : ' + this.utilityService.getSelectedCountryCode());
    console.log('utilityService.selected Country : ' + this.utilityService.selectedCountry);

    this.selectedCountry = localStorage.getItem("country");
    console.log('localstorage selected Country : ' + this.selectedCountry);


    if (this.selectedCountry == environment.COUNTRY_NAME_GERMANY && this.selectedlanguage == environment.COUNTRY_CODE_GERMANY) {

      this.title = this.titlesDe[0];
      this.description = this.descriptionsDe[0];
    } else if (this.selectedCountry == environment.COUNTRY_NAME_FRANCE && this.selectedlanguage == environment.COUNTRY_CODE_FRANCE) {
      this.title = this.titlesFr[0];
      this.description = this.descriptionsFr[0];
    } else if (this.selectedCountry == environment.COUNTRY_NAME_PORTUGAL && this.selectedlanguage == environment.COUNTRY_CODE_PORTUGAL) {
      this.title = this.titlesPo[0];
      this.description = this.descriptionsPo[0];
    } else if (this.selectedCountry == environment.COUNTRY_NAME_SPAIN && this.selectedlanguage == environment.COUNTRY_CODE_SPAIN) {
      this.title = this.titlesEs[0];
      this.description = this.descriptionsEs[0];
    } else {
      this.title = this.titles[0];
      this.description = this.descriptions[0];

    }

  }

  tabChangeEvent($event) {
    console.log('index : ' + $event.index);
    this.title = this.titles[$event.index];
    this.description = this.descriptions[$event.index];
  }

  tabChangeEventForDE($event) {
    console.log('index : ' + $event.index);
    this.title = this.titlesDe[$event.index];
    this.description = this.descriptionsDe[$event.index];
  }

  tabChangeEventForFR($event) {
    console.log('index : ' + $event.index);
    this.title = this.titlesFr[$event.index];
    this.description = this.descriptionsFr[$event.index];
  }

  tabChangeEventForPO($event) {
    console.log('index : ' + $event.index);
    this.title = this.titlesPo[$event.index];
    this.description = this.descriptionsPo[$event.index];
  }


  tabChangeEventForES($event) {
    console.log('index : ' + $event.index);
    this.title = this.titlesEs[$event.index];
    this.description = this.descriptionsEs[$event.index];
  }
}
