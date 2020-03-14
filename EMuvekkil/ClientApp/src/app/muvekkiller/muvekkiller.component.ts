import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { Router } from '@angular/router';
import { UserModel } from '../shared/models';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-muvekkiller',
  templateUrl: './muvekkiller.component.html',
  styleUrls: ['./muvekkiller.component.css']
})

export class MuvekkillerComponent implements OnInit {


  users: UserModel[] = [];
  displayedColumns: string[] = ['index', 'name', 'email','company'];
  dataSource = new MatTableDataSource(this.users);
  

  constructor(private router: Router, private userService: UserService) { }

  ngOnInit() {
    this.userService.getMuvekkils().subscribe(mv => {
      this.users = mv; 
      this.dataSource = new MatTableDataSource(this.users);
    }, err => (err));
  }


  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  goDetail(row: UserModel) {
    this.router.navigate(['muvekkil/' + row.id]);
  }

  goNew() {
    this.router.navigate(['muvekkil']);
  }
}
