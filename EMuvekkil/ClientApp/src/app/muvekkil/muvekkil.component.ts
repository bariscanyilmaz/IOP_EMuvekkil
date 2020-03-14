import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { RegisterViewModel, CompanyViewModel } from '../shared/models';
import { CompanyService } from '../services/company.service';
import { MatDialog } from '@angular/material';
import { WarningDialogComponent } from '../warning-dialog/warning-dialog.component';
import { DeleteConfirmationDialogComponent } from '../delete-confirmation-dialog/delete-confirmation-dialog.component';

@Component({
  selector: 'app-muvekkil',
  templateUrl: './muvekkil.component.html',
  styleUrls: ['./muvekkil.component.css']
})
export class MuvekkilComponent implements OnInit {

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

  companies: CompanyViewModel[] = [];

  constructor(private route: ActivatedRoute, private userService: UserService, private router: Router, private companyService: CompanyService, private dialog: MatDialog) { }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get('id');
    this.isNewRecord = id ? false : true;
    this.companyService.getCompanies().subscribe(r => { this.companies = r }, err => console.log(err));
    if (!this.isNewRecord) {
      this.userService.getUser(id).subscribe((user: RegisterViewModel) => { this.model = user; }, err => console.log(err));
    }

  }

  save() {
    if (this.isNewRecord) {
      this.userService.createNewMuvekkil(this.model).subscribe(res => setTimeout(() => {
        this.router.navigate(['/muvekkiller']);
      }, 300), err => console.log(err));
    } else {
      this.userService.updateUser(this.model).subscribe(res => setTimeout(() => {
        this.router.navigate(['/muvekkiller']);
      }, 300), err => console.log(err));
    }
  }

  delete() {
    this.userService.getUserDependencies(this.model, 'muvekkil').subscribe(r => {
      if (r > 0) {
        this.dialog.open(WarningDialogComponent, { minWidth: '300px', data: { message: 'Müvekkil davalarda bulunduğu için silinemez' } });
      } else {
        const dialogRef = this.dialog.open(DeleteConfirmationDialogComponent, { minWidth: '300xp' });
        dialogRef.afterClosed().subscribe((r) => {
          if (r) {
            this.userService.deleteUser(this.model).subscribe(r => setTimeout(() => {
              this.router.navigate(['/muvekkiller']);
            }, 300), err => console.log(err))
          }
        })

        
      }
    });


  }



}
