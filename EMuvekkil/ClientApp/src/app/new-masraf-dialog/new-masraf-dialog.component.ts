import { Component, Inject,  ViewChild, ElementRef } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, DateAdapter, MAT_DATE_FORMATS } from '@angular/material';
import { MasrafViewModel } from '../shared/models';
import { APP_DATE_FORMATS, AppDateAdapter } from '../shared/format-datepicker';


@Component({
  selector: 'app-new-masraf-dialog',
  templateUrl: './new-masraf-dialog.component.html',
  styleUrls: ['./new-masraf-dialog.component.css'],
  providers:[{ provide: DateAdapter, useClass: AppDateAdapter },
    { provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS }]
})
export class NewMasrafDialogComponent {

  @ViewChild('file') fileElement:ElementRef;

  fileData:File;
  
  constructor(
    public dialogRef: MatDialogRef<NewMasrafDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data?: MasrafViewModel) {
      
    }

  onNoClick(): void {
    this.dialogRef.close();
  }

  

}
