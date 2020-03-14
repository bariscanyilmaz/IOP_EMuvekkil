import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { JwtHelperService } from "@auth0/angular-jwt";
import { LoginViewModel, UserInfoModel } from '../shared/models';
import { of, Observable, BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private appUrl: string=environment.appUrl;
  constructor(private http: HttpClient, private router: Router, private jwtHelper: JwtHelperService) {
    
  }

  isLogIn$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  userInfo$: BehaviorSubject<UserInfoModel> = new BehaviorSubject<UserInfoModel>(null);

  isLoggedIn(): Observable<boolean> {
    const token = localStorage.getItem('token');
    if (!token) {
      this.isLogIn$.next(false);
    } else {
      let validToken = !this.jwtHelper.isTokenExpired(token);
      this.isLogIn$.next(validToken);
    }

    return (this.isLogIn$.asObservable());
  }

  login(loginModel: LoginViewModel) {
    return this.http.post(this.appUrl+'/api/Account/Login', loginModel);
  }

  setLogin() {
    const token = localStorage.getItem('token');
    const model: UserInfoModel = this.jwtHelper.decodeToken(token) as UserInfoModel;
    this.isLogIn$.next(true);
    this.userInfo$.next(model);
  }

  logout() {
    localStorage.removeItem('token');
    this.isLogIn$.next(false);
    this.router.navigate(['/login']);
  }


  getUserInfo(): Observable<UserInfoModel> {
    const token = localStorage.getItem('token');
    const model: UserInfoModel = this.jwtHelper.decodeToken(token) as UserInfoModel;
    this.userInfo$.next(model);
    return this.userInfo$.asObservable();
  }

  getUser() {
    const token = localStorage.getItem('token');
    return this.jwtHelper.decodeToken(token) as UserInfoModel;
  }

  getUserRole() {
    const token = localStorage.getItem('token');
    return (this.jwtHelper.decodeToken(token) as UserInfoModel).role;
  }

}
