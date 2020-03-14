import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserModel, RegisterViewModel } from '../shared/models';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  appUrl:string=environment.appUrl;
  constructor(private http: HttpClient) {
    
  }

  getUser(userid: string) {
    return this.http.get('/api/Account/GetUser', { params: { id: userid } });
  }
  getAllUsers(){
    return this.http.get<UserModel[]>('/api/Account/GetAllUsers');
  }

  getMuvekkils():Observable<UserModel[]> {
    return this.http.get<UserModel[]>(this.appUrl+'/api/Account/GetMuvekkils');
  }

  getLawyers():Observable<UserModel[]> {
    return this.http.get<UserModel[]>(this.appUrl+'/api/Account/GetLawyers');
  }

  updateUser(user:RegisterViewModel){
    return this.http.post(this.appUrl+'/api/Account/UpdateUser',user);
  }

  createNewMuvekkil(user:RegisterViewModel){
    return this.http.post(this.appUrl+'/api/Account/RegisterUser',user,{params:{role:'muvekkil'}});
  }
  
  createNewLawyer(user:RegisterViewModel){
    return this.http.post(this.appUrl+'/api/Account/RegisterUser',user,{params:{role:'avukat'}});
  }

  deleteUser(user:RegisterViewModel){
    return this.http.post(this.appUrl+'/api/Account/DeleteUser',user);
  }

  getUserDependencies(user:RegisterViewModel,role:'muvekkil'|'avukat'){
    return this.http.get<number>(this.appUrl+'/api/Account/GetUserDependencies',{params:{id:user.id,role:role}});
  }
}
