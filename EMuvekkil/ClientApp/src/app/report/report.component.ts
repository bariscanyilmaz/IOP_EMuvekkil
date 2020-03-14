import { Component, OnInit } from '@angular/core';
import { UserModel, DavaViewModel, ReportViewModel, ReportListViewModel } from '../shared/models';
import { UserService } from '../services/user.service';
import { DavaService } from '../services/dava.service';
import { ReportService } from '../services/report.service';
import { MatTableDataSource, DateAdapter, MAT_DATE_FORMATS } from '@angular/material';
import { AppDateAdapter, APP_DATE_FORMATS } from '../shared/format-datepicker';
import  * as XLSX  from "xlsx";



@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css'],
  providers: [{ provide: DateAdapter, useClass: AppDateAdapter },
  { provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS }]

})
export class ReportComponent implements OnInit {

  model: ReportViewModel = {
    startDate: new Date(),
    endDate: new Date(),
    avukatId: '',
    muvekkilId: '',
    davaId: 0,
    dateDisabled: true
  }
  selectedDava:DavaViewModel;
  isShow = false;
  isActive: boolean = false;
  muvekkils: UserModel[] = [];
  avukats: UserModel[] = [];
  davas: DavaViewModel[] = [];

  reportList: ReportListViewModel = { documents: [], masrafs: [], messages: [] };

  masrafSource = new MatTableDataSource(this.reportList.masrafs);
  documentSource = new MatTableDataSource(this.reportList.documents);
  messageSource = new MatTableDataSource(this.reportList.messages);

  masrafColumns: string[] = ['date', 'user', 'amount', 'description'];
  documentColumns: string[] = ['date', 'user', 'name', 'description', 'active'];
  messageColumns: string[] = ['date', 'user', 'message', 'active'];

  constructor(private userService: UserService,
    private davaService: DavaService, private reportService: ReportService) { }

  ngOnInit() {

    this.userService.getMuvekkils().subscribe(r => {
      this.muvekkils = r;
    }, err => (err))
    this.userService.getLawyers().subscribe(r => {
      this.avukats = r;
    }, err => err);
  }

  onMuvekkilChange() {

  }

  onSelectCahnge() {

    if (this.model.avukatId.length > 1 && this.model.muvekkilId.length > 1) {
      this.davaService.getDavasBy(this.model.muvekkilId, this.model.avukatId).subscribe(r => {
        this.davas = r;

      }, err => err);
    } else if (this.model.muvekkilId.length > 1 && !(this.model.avukatId.length > 1)) {



      this.davaService.getDavasByMuvekkil(this.model.muvekkilId).subscribe(r => {
        this.davas = r;
      }, err => err);

    } else if (!(this.model.muvekkilId.length > 1) && (this.model.avukatId.length > 1)) {

      this.davaService.getDavasByAvukat(this.model.avukatId).subscribe(r => {
        this.davas = r;
      }, err => err);

    }

  }

  getReport() {

    let wb=XLSX.utils.book_new();
      wb = {
      SheetNames: ["Dava"],
      Sheets: {
        Dava: {
          "!ref":"A1:C4",
          A1: { t:"s", v:"Dava " },  
          B1: { t:"s", v:this.selectedDava.name },      

          A2: { t:"s", v:"Avukat" },
          B2: { t:"s", v: this.selectedDava.avukatName},

          A3: { t:"s", v: "Müvekkil"},
          B3: { t:"s", v: this.selectedDava.muvekkilName},

          A4: { t:"s", v: "Tarih Aralığı"},
          B4: { t:"s", v: this.model.startDate.toLocaleDateString()},
          C4: { t:"s", v: this.model.endDate.toLocaleDateString()},
        }
      }
    }
    
    let element=document.getElementById('masraf-table');
    let ws=XLSX.utils.table_to_sheet(element);
    XLSX.utils.book_append_sheet(wb,ws,'Masraflar');

    element=document.getElementById('document-table');
    ws=XLSX.utils.table_to_sheet(element);
    XLSX.utils.book_append_sheet(wb,ws,'Dökümanlar');

    element=document.getElementById('message-table');
    ws=XLSX.utils.table_to_sheet(element);
    
    XLSX.utils.book_append_sheet(wb,ws,'Mesajlar');
    XLSX.writeFile(wb,'Rapor.xlsx');
  }

  getList() {
    this.reportService.getList(this.model).subscribe(r => {
      this.reportList = r;
      this.masrafSource = new MatTableDataSource(this.reportList.masrafs);
      this.documentSource = new MatTableDataSource(this.reportList.documents);
      this.messageSource = new MatTableDataSource(this.reportList.messages);
      this.isShow = true;

    }, err => err, () => {

    });


  }

  onDavaSelect() {

    if (this.model.davaId != 0) {
      this.selectedDava=this.davas.find(d=>d.id==this.model.davaId);
      this.isActive = true
    } else {
      this.isActive = false;
    }

  }

  getTotalCost() {
    if (this.reportList.masrafs.length > 0) {
      return this.reportList.masrafs.map(t => t.amount).reduce((acc, value) => acc + value, 0);
    } else {
      return "";
    }
  }

  getStatus(isActive: boolean): string {
    return isActive ? 'Aktif' : 'Pasif';
  }

}
