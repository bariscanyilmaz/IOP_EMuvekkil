import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material';
import { UserModel } from '../shared/models';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-avukatlar',
  templateUrl: './avukatlar.component.html',
  styleUrls: ['./avukatlar.component.css']
})
export class AvukatlarComponent implements OnInit {

  constructor(private router:Router,private userService:UserService) { }

  ngOnInit() {
    
    this.userService.getLawyers().subscribe(av => {
      this.users = av; 
      this.dataSource = new MatTableDataSource(this.users);
    }, err => console.log(err));
  }

  users: UserModel[] = [];
  displayedColumns: string[] = ['index', 'name', 'email'];
  dataSource = new MatTableDataSource(this.users);

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  goDetail(row:UserModel){
    this.router.navigate(['avukat/'+row.id]);
  }

  goNew(){
    this.router.navigate(['avukat']);
  }

}
