import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { MasrafViewModel } from '../shared/models';
import { Observable } from 'rxjs';
import { UserService } from './user.service';
import { AuthService } from './auth.service';


@Injectable({
  providedIn: 'root'
})
export class MasrafService {

  appUrl: string = environment.appUrl;
  constructor(private http: HttpClient, private authService: AuthService) { }

  addMasraf(model: MasrafViewModel): Observable<MasrafViewModel> {
    model.ownerUserName = this.authService.getUser().email;
    return this.http.post<MasrafViewModel>(this.appUrl + '/api/Masraf/AddMasraf', model);
  }

  deleteMasraf(model:MasrafViewModel):Observable<MasrafViewModel> {
    return this.http.post<MasrafViewModel>(this.appUrl + '/api/Masraf/DeleteMasraf',model);
  }

  getMasrafsByDavaId(id: number): Observable<MasrafViewModel[]> {
    return this.http.get<MasrafViewModel[]>(this.appUrl + '/api/Masraf/GetMasrafsbyDavaId', { params: { 'id': id.toString() } });
  }
}
