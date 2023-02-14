import { Component, OnInit,Inject } from '@angular/core';

import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';
import { DatePipe } from '@angular/common';
import { UtilityService } from '../../services/utility.service';
@Component({
  selector: 'app-agree-diaglog',
  templateUrl: './agree-diaglog.component.html',
  styleUrls: ['./agree-diaglog.component.css']
})
export class AgreeDiaglogComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<AgreeDiaglogComponent>,
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
    console.log('Agree-Dialog selected Language : ' + this.selectedlanguage);
   
    this.selectedCountry = this.utilityService.selectedCountry;
    console.log('Agree-Dialog selected country : ' + this.utilityService.selectedCountry);
  }

  onNoClick(): void {
    var d = new Date();
    //var time = this.datePipe.transform(d, "yyyy-MM-ddTHH:mm:ss");
    var time = ''+d.getTime();
   // console.log("timeStamp : "+time);
   // var time = '' + d.toDateString();
    console.log("time-stamp : " + time);
    this.dialogRef.close({ action: 1, data: time });
  }
}
