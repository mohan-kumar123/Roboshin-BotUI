import { Component, OnInit, Inject } from '@angular/core';

import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { DatePipe } from '@angular/common';
import { UtilityService } from '../../services/utility.service';
@Component({
  selector: 'app-help-certificate',
  templateUrl: './help-certificate.component.html',
  styleUrls: ['./help-certificate.component.css']
})
export class HelpCertificateComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<HelpCertificateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string, public utilityService: UtilityService) { }
  isenglish: Boolean = false;
  selectedlanguage: string;

  ngOnInit() {
    this.selectedlanguage = localStorage.getItem("lang");
    if (this.selectedlanguage == "en") {
      this.isenglish = true;
    } else {
      this.isenglish = false;
    }
    console.log(this.selectedlanguage);
  }

  onOkClick(): void {
    var d = new Date();
    //var time = this.datePipe.transform(d, "yyyy-MM-ddTHH:mm:ss");
    var time = '' + d.getTime();
    // console.log("timeStamp : "+time);
    // var time = '' + d.toDateString();
    console.log("Help-Certificate : time-stamp : " + time);
    this.dialogRef.close();
  }
}
