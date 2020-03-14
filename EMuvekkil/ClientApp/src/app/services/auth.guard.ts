import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from "../services/auth.service";
import { JwtHelperService } from '@auth0/angular-jwt';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  /**
   *
   */
  constructor(private authService: AuthService, private router: Router,private jwtHelper:JwtHelperService) {


  }

  canActivate(): boolean {
    const token = localStorage.getItem('token');
    if (!token) {
      this.router.navigate(['/login']);
      return false;
    } else {
      let validToken=!this.jwtHelper.isTokenExpired(token);
      return validToken;
    }


  }
}
