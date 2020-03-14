import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MAT_DATE_FORMATS, DateAdapter } from '@angular/material';
import { AppDateAdapter, APP_DATE_FORMATS } from '../shared/format-datepicker';


export interface MessageData {
  date: string;
  user: string;
  message: string;

}

@Component({
  selector: 'app-new-message-dialog',
  templateUrl: './new-message-dialog.component.html',
  styleUrls: ['./new-message-dialog.component.css'],
  providers: [{ provide: DateAdapter, useClass: AppDateAdapter },
  { provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS }]
})
export class NewMessageDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<NewMessageDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data?: MessageData) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
