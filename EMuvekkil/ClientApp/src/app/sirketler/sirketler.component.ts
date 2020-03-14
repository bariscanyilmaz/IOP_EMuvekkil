import { Component, OnInit } from '@angular/core';
import { CompanyViewModel } from '../shared/models';
import { MatTableDataSource, MatDialog } from '@angular/material';
import { CompanyService } from '../services/company.service';
import { DeleteConfirmationDialogComponent } from '../delete-confirmation-dialog/delete-confirmation-dialog.component';
import { WarningDialogComponent } from '../warning-dialog/warning-dialog.component';

@Component({
  selector: 'app-sirketler',
  templateUrl: './sirketler.component.html',
  styleUrls: ['./sirketler.component.css']
})
export class SirketlerComponent implements OnInit {

  companyModel: CompanyViewModel = { name: '', id: 0, dependecies: 0 };
  companies: CompanyViewModel[] = [];
  displayedColumns: string[] = ['id', 'name', 'action'];
  dataSource = new MatTableDataSource(this.companies);

  constructor(private companySevice: CompanyService, private dialog: MatDialog) { }

  ngOnInit() {
    this.companySevice.getCompanies().subscribe(r => { this.companies = r; this.dataSource = new MatTableDataSource(this.companies); }, err => (err));
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  save() {
    this.companySevice.addNew(this.companyModel).subscribe(r => { this.companies.push(r); this.dataSource = new MatTableDataSource(this.companies); }, err => console.log(err));
  }

  delete(model: CompanyViewModel) {
    if (model.dependecies > 0) {
      const dialogRef= this.dialog.open(WarningDialogComponent, { minWidth: '300px',data:{message:'Şirket kullanıldığı için silemezsiniz'} });
    } else {
      const dialogRef = this.dialog.open(DeleteConfirmationDialogComponent, { minWidth: '300xp' });
      dialogRef.afterClosed().subscribe(res => {
        if (res) {
          this.companySevice.deleteCompany(model).subscribe(r => {

            let index = this.companies.findIndex(d => d.id == r.id);
            this.companies.splice(index, 1);
            this.dataSource = new MatTableDataSource(this.companies);
          }, err => (err));
        }
      })
    }


  }

}
