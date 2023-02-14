import { Component, OnInit, Inject } from '@angular/core';

import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { DatePipe } from '@angular/common';
import { UtilityService } from '../../services/utility.service';
@Component({
  selector: 'app-help-content',
  templateUrl: './help-content.component.html',
  styleUrls: ['./help-content.component.css']
})
export class HelpContentComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<HelpContentComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string,
    public utilityService: UtilityService) { }

  isenglish: Boolean = false;
  selectedlanguage: string;
  selectedCountry: string;

  ngOnInit() {
    this.selectedlanguage = localStorage.getItem("lang");
    if (this.selectedlanguage == "en") {
      this.isenglish = true;
    } else {
      this.isenglish = false;
    }

    this.selectedCountry = this.utilityService.selectedCountry;
    console.log('Help Component selected-language : '+this.selectedlanguage);
  }

  onNoClick(): void {
    var d = new Date();
    //var time = this.datePipe.transform(d, "yyyy-MM-ddTHH:mm:ss");
    var time = '' + d.getTime();
    // console.log("timeStamp : "+time);
    // var time = '' + d.toDateString();
    console.log("time-stamp : " + time);
    this.dialogRef.close();
  }
}
