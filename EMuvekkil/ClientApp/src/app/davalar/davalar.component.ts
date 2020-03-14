import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material';
import { DavaService } from '../services/dava.service';
import { DavaViewModel } from '../shared/models';

@Component({
  selector: 'app-davalar',
  templateUrl: './davalar.component.html',
  styleUrls: ['./davalar.component.css']
})
export class DavalarComponent implements OnInit {



  davas: DavaViewModel[] = [];
  displayedColumns: string[] = ['id', 'name', 'state','lawyer', 'muvekkil'];
  dataSource = new MatTableDataSource(this.davas);

  constructor(private router: Router, private davaService: DavaService) { }

  ngOnInit() {
    this.davaService.getDavas().subscribe(d => {
      this.davas = d;
      this.dataSource = new MatTableDataSource(this.davas);
      
    }, err => console.log(err));

  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  goDetail(row: DavaViewModel) {
    this.router.navigate(['dava/' + row.id]);
  }

  goNew() {
    this.router.navigate(['dava']);
  }

}

