import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { DocumentViewModel } from '../shared/models';
import { DocumentService } from '../services/document.service';

@Component({
  selector: 'app-dokumanlar',
  templateUrl: './dokumanlar.component.html',
  styleUrls: ['./dokumanlar.component.css']
})
export class DokumanlarComponent implements OnInit {

  documents: DocumentViewModel[] = [];
  displayedColumns: string[] = ['date', 'user', 'document', 'dava','active', 'action'];
  dataSource = new MatTableDataSource(this.documents);

  constructor(private documentService: DocumentService) { }

  ngOnInit() {
    this.documentService.getDocuments().subscribe(r => { this.documents = r; this.dataSource = new MatTableDataSource(this.documents) }, err => console.log(err));
  }

  changeStatus(model:DocumentViewModel) {
    this.documents.map((v) => { if (v.id == model.id) { v.isActive = !model.isActive; } });
    this.dataSource = new MatTableDataSource(this.documents);
    this.documentService.changeStatus(model).subscribe(r=>(r),err=>(err));
  }

  

  getStatus(isActive: boolean): string {
    return isActive ? 'Aktif' : 'Pasif';
  }
  


}
