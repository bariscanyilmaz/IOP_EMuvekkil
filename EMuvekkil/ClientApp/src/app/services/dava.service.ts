import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DavaViewModel, DavaStateViewModel } from '../shared/models';
import { AuthService } from './auth.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DavaService {

  appUrl: string = environment.appUrl;
  constructor(private http: HttpClient, private authService: AuthService) {

  }



  getDava(id: string): Observable<DavaViewModel> {
    return this.http.get<DavaViewModel>(this.appUrl + '/api/Dava/GetDava', { params: { 'id': id } });
  }
  
  getDavas(): Observable<DavaViewModel[]> {
    let user = this.authService.getUser();
    return this.http.get<DavaViewModel[]>(this.appUrl + '/api/Dava/GetDavas', { params: { 'role': user.role, 'email': user.email } });

  }

  getDavasBy(muvekkilId: string, avukatId: string): Observable<DavaViewModel[]> {
    return this.http.get<DavaViewModel[]>(this.appUrl + '/api/Dava/GetDavasBy', { params: { 'muvekkilId': muvekkilId, 'avukatId': avukatId } });
  }

  getDavasByMuvekkil(muvekkilId: string): Observable<DavaViewModel[]> {
    return this.http.get<DavaViewModel[]>(this.appUrl + '/api/Dava/GetDavasByMuvekkil', { params: { 'muvekkilId': muvekkilId} });
  }
  
  getDavasByAvukat(avukatId: string): Observable<DavaViewModel[]> {
    return this.http.get<DavaViewModel[]>(this.appUrl + '/api/Dava/GetDavasByAvukat', { params: { 'avukatId': avukatId} });
  }
  

  createNew(dava: DavaViewModel): Observable<DavaViewModel> {
    return this.http.post<DavaViewModel>(this.appUrl + '/api/Dava/CreateDava', dava);
  }

  updateDava(dava: DavaViewModel): Observable<DavaViewModel> {
    return this.http.post<DavaViewModel>(this.appUrl + '/api/Dava/UpdateDava', dava);
  }

  deleteDava(dava: DavaViewModel) {
    return this.http.post(this.appUrl + '/api/Dava/DeleteDava', dava);
  }

  getDavaStates(): Observable<DavaStateViewModel[]> {
    return this.http.get<DavaStateViewModel[]>(this.appUrl + '/api/Dava/GetDavaStates');
  }


}
