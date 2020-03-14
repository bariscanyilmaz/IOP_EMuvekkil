import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef} from "@angular/material";
@Component({
  selector: 'app-delete-confirmation-dialog',
  templateUrl: './delete-confirmation-dialog.component.html',
  styleUrls: ['./delete-confirmation-dialog.component.css']
})
export class DeleteConfirmationDialogComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<DeleteConfirmationDialogComponent>) { }

  ngOnInit() {

  }

  confirm(resp:boolean){
    this.dialogRef.close(resp);
  }

}
