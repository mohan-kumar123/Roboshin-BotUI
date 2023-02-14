import { Component, OnInit,Inject } from '@angular/core';
import { MAT_DIALOG_DATA,MatDialogRef } from '@angular/material';
import { TranslateService } from '@ngx-translate/core';
import { ResponseDetails } from 'src/app/models/ResponseData';

@Component({
  selector: 'app-verifydialog',
  templateUrl: './verifydialog.component.html',
  styleUrls: ['./verifydialog.component.css']
})
export class VerifydialogComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<VerifydialogComponent>,
    @Inject(MAT_DIALOG_DATA) public responseData: ResponseDetails,protected translate : TranslateService) {}

  ngOnInit() {
  }
  onCloseClick():void{

  this.dialogRef.close();
  
  }
}
