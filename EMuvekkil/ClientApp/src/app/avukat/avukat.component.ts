import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RegisterViewModel } from '../shared/models';
import { UserService } from '../services/user.service';
import { WarningDialogComponent } from '../warning-dialog/warning-dialog.component';
import { MatDialog } from '@angular/material';
import { DeleteConfirmationDialogComponent } from '../delete-confirmation-dialog/delete-confirmation-dialog.component';

@Component({
  selector: 'app-avukat',
  templateUrl: './avukat.component.html',
  styleUrls: ['./avukat.component.css']
})
export class AvukatComponent implements OnInit {


  isNewRecord: boolean;
  hide: boolean;
  model: RegisterViewModel = {
    email: '',
    password: '',
    nameSurname: '',
    id: '',
    identityNumber: '',
    companyId: 0
  };

  constructor(private route: ActivatedRoute, private userService: UserService, private router: Router, private dialog: MatDialog) { }


  ngOnInit() {
    let id = this.route.snapshot.paramMap.get('id');
    this.isNewRecord = id ? false : true;
    if (!this.isNewRecord) {
      this.userService.getUser(id).subscribe((user: RegisterViewModel) => { this.model = user; }, err => console.log(err));
    }

  }

  save() {
    if (this.isNewRecord) {
      this.userService.createNewLawyer(this.model).subscribe(res => setTimeout(() => { this.router.navigate(['/avukatlar']) }, 300), err => console.log(err));
    } else {
      this.userService.updateUser(this.model).subscribe(res => setTimeout(() => { this.router.navigate(['/avukatlar']) }, 300), err => console.log(err));
    }
  }

  delete() {

    this.userService.getUserDependencies(this.model, 'avukat').subscribe(r => {
      if (r > 0) {
        this.dialog.open(WarningDialogComponent, { minWidth: '300px', data: { message: 'Avukat davalarda bulunduğu için silinemez' } });
      } else {
        const dialogRef = this.dialog.open(DeleteConfirmationDialogComponent, { minWidth: '300xp' });
        dialogRef.afterClosed().subscribe((r) => {
          if (r) {
            this.userService.deleteUser(this.model).subscribe(r => setTimeout(() => {
              this.router.navigate(['/muvekkiller']);
            }, 300), err => (err))
          }
        })

      }
    })


  }


}
