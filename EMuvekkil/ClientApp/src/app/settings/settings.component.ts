import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { UserInfoModel, RegisterViewModel } from '../shared/models';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  hide: boolean = true;
  userInfo: UserInfoModel;
  password: string;
  constructor(private authService: AuthService, private userService: UserService, private router: Router) { }

  ngOnInit() {
    this.authService.getUserInfo().subscribe(u => { this.userInfo = u; }, err => (err));
  }

  save() {
    let registerModel: RegisterViewModel = {
      id: this.userInfo.id,
      email: this.userInfo.email,
      identityNumber: this.userInfo.identityNumber,
      nameSurname: this.userInfo.nameSurname,
      password: this.password,
      companyId:0
    };

    this.userService.updateUser(registerModel).subscribe(r => {setTimeout(()=>this.authService.logout(),300);}, err =>(err));

  }



}
