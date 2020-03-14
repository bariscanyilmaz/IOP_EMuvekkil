import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { MessageViewModel } from '../shared/models';
import { AuthService } from './auth.service';


@Injectable({
  providedIn: 'root'
})
export class MessageService {

  private appUrl = environment.appUrl;
  constructor(private http: HttpClient, private authService: AuthService) {

  }

  getMessagesByDavaId(id: number): Observable<MessageViewModel[]> {
    return this.http.get<MessageViewModel[]>(this.appUrl + '/api/Message/GetMessagesByDavaId', { params: { 'id': id.toString() } });
  }

  getMessages() {
    let user = this.authService.getUser();
    return this.http.get<MessageViewModel[]>(this.appUrl + '/api/Message/GetMessages', { params: { 'email': user.email } });
  }

  changeStatus(model:MessageViewModel) {
    return this.http.post<MessageViewModel>(this.appUrl + '/api/Message/ChangeStatus',model);
  }

  addMessage(model:MessageViewModel){
    return this.http.post<MessageViewModel>(this.appUrl + '/api/Message/AddMessage',model);
  }

}
