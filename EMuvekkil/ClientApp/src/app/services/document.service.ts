import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { DocumentViewModel } from '../shared/models';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {

  private appUrl = environment.appUrl;

  constructor(private http: HttpClient,private authService:AuthService) { }

  addDocument(model:DocumentViewModel):Observable<DocumentViewModel> {  
    let formData=new FormData();
    formData.append('file',model.file,model.file.name);
    formData.append('ownerUserName',model.ownerUserName);
    formData.append('date',model.date.toDateString());
    formData.append('description',model.description);
    formData.append('davaId',model.davaId.toString());

    return this.http.post<DocumentViewModel>(this.appUrl + '/api/Document/AddDocument',formData);
  }

  getDocumentsByDavaId(id: number): Observable<DocumentViewModel[]> {
    return this.http.get<DocumentViewModel[]>(this.appUrl + '/api/Document/GetDocumentsbyDavaId', { params: { 'id': id.toString() } });
  }


  getDocuments() {
    let user = this.authService.getUser();
    return this.http.get<DocumentViewModel[]>(this.appUrl + '/api/Document/GetDocuments', { params: { 'email': user.email } });
  }

  changeStatus(model:DocumentViewModel){
    return this.http.post<DocumentViewModel>(this.appUrl+'/api/Document/ChangeStatus',model);
  }


  downloadDocument(id:number){
    return this.http.get(this.appUrl+'/api/Document/DowloadFile',{params:{'docId':id.toString()},responseType:'blob' as 'json'});
  }

}
