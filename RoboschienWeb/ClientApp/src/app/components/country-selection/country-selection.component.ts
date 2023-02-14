import { Component, OnInit, Inject } from '@angular/core';

import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { DatePipe } from '@angular/common';
import { UtilityService } from '../../services/utility.service';

@Component({
  selector: 'app-country-selection',
  templateUrl: './country-selection.component.html',
  styleUrls: ['./country-selection.component.css']
})

export class CountrySelectionComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<CountrySelectionComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string, public utilityService: UtilityService) { }
  isenglish: Boolean = false;
  selectedlanguage: string;

  germany: string;
  france: string;
  portugel: string;
  countrySelected: boolean;
  ngOnInit() {
    this.selectedlanguage = localStorage.getItem("lang");
    if (this.selectedlanguage == "en") {
      this.isenglish = true;
    } else {
      this.isenglish = false;
    }
    console.log(this.selectedlanguage);
    this.utilityService.selectedLang = this.selectedlanguage;
  }

  onOkClick(): void {
    var d = new Date();
    
    var time = '' + d.getTime();
    
    console.log("Country-Selection  Continue: time-stamp : " + time);
    this.dialogRef.close({ action: 1, data: time });
  }

  onCancel(): void {

    var d = new Date();

    var time = '' + d.getTime();

    console.log("Country-Selection cancel : time-stamp : " + time);
    this.dialogRef.close({ action: 1, data: time });
  }

  onItemChange(value) {
    localStorage.setItem("country", value);
    console.log("56 Value is : ", value);
    this.countrySelected = true;
    this.utilityService.selectedCountry = value;

    console.log('utilityService Selected Country : ' + this.utilityService.selectedCountry)
  }
}
