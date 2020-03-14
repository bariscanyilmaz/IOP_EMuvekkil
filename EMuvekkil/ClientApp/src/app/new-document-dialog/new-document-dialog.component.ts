import { Component, OnInit, Inject, ViewChild, ElementRef } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, DateAdapter, MAT_DATE_FORMATS } from '@angular/material';
import { DocumentService } from '../services/document.service';
import { DocumentViewModel } from '../shared/models';
import { APP_DATE_FORMATS, AppDateAdapter } from '../shared/format-datepicker';



@Component({
  selector: 'app-new-document-dialog',
  templateUrl: './new-document-dialog.component.html',
  styleUrls: ['./new-document-dialog.component.css'],
  providers:[{ provide: DateAdapter, useClass: AppDateAdapter },
    { provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS }]
})
export class NewDocumentDialogComponent {

  fileName:string='';
  isValidfile:boolean=false;
  @ViewChild('file') fileEl:ElementRef;


  constructor(
    public dialogRef: MatDialogRef<NewDocumentDialogComponent>,private documentService:DocumentService,
    @Inject(MAT_DIALOG_DATA) public data?: DocumentViewModel,) { }



  onNoClick(): void {
    this.dialogRef.close();
  }

  upload(files:File[]){
    if(files[0]){
      this.data.fileName=files[0].name;
      this.fileName=files[0].name;
      this.data.file=files[0];
      this.isValidfile=true;
    }else{
      this.isValidfile=false;
      this.fileName='';
    };

  }

  save(){
    let document:DocumentViewModel={id:0,date:this.data.date,davaId:this.data.davaId,davaName:'',description:this.data.description,fileName:this.data.description,ownerName:'',ownerUserName:this.data.ownerUserName,file:this.data.file,isActive:true};
    this.isValidfile=false;
    this.documentService.addDocument(document).subscribe(r=>{this.data=r; this.dialogRef.close()},err=>console.log(err));
  }

  

}
