import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthService } from '../services/auth.service';
import { LoginViewModel } from '../shared/models';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  hide: boolean = true;
  isError: boolean = false;
  errorMessage: string = "";

  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(6)])
  })

  closeAlert() {
    this.isError = false;
  }

  constructor(private authServic: AuthService, private route: Router) { }

  ngOnInit() {

  }

  login() {
    let loginModel = this.loginForm.value as LoginViewModel;
    this.authServic.login(loginModel).subscribe((resp: any) => {
      localStorage.setItem('token', resp.token);
      this.isError = false;
      this.authServic.setLogin();
      this.errorMessage = "Oturum başarılı bir şekilde açıldı.";
      this.route.navigate(['/']); 

    }, (err: HttpErrorResponse) => {
      this.isError = true;
      this.errorMessage = err.error;
    });

  }


}
