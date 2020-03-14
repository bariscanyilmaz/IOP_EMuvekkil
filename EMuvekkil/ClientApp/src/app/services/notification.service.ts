import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { NotificationViewModel } from '../shared/models';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  private appUrl = environment.appUrl;
  notifcations:NotificationViewModel[]=[{id:1,isRead:false,message:'Toplantı 24.12.2019 16:45 '},{id:2,isRead:true,message:'Halı saha maçı  24.12.2019 16:45'}]
  constructor(private http:HttpClient,private auhtService:AuthService) { 

  }

  getNotifications(id:string){
    //7return of(this.notifcations);
    return this.http.get<NotificationViewModel[]>(this.appUrl+'/api/Notification/GetNotifications', { params: { id: id } });
  }

  markReaded(model:NotificationViewModel){
    //return of(model);
    return this.http.post<NotificationViewModel>(this.appUrl+'/api/Notification/MarkReaded',model);
  }



}
