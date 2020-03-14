import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';
import { CompanyViewModel } from '../shared/models';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  appUrl: string = environment.appUrl;
  constructor(private http: HttpClient) {

  }

  addNew(model:CompanyViewModel):Observable<CompanyViewModel>{
    return this.http.post<CompanyViewModel>(this.appUrl+'/api/Company/AddCompany',model);
  }

  deleteCompany(model:CompanyViewModel):Observable<CompanyViewModel>{
    
    return this.http.post<CompanyViewModel>(this.appUrl+'/api/Company/DeleteCompany',model);
  }

  getCompanies():Observable<CompanyViewModel[]>{
    return this.http.get<CompanyViewModel[]>(this.appUrl+'/api/Company/GetCompanies');
  }

}
