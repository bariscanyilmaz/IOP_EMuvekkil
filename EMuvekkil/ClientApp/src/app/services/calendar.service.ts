import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EventViewModel } from '../shared/models';
import { environment } from "src/environments/environment";
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';

export interface IMonth {
  id: number;
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class CalendarService {


  constructor(private http: HttpClient,private router:Router) {
    this.fetchEvents();
  }

  private months: IMonth[] = [{ id: 0, name: 'Ocak' }, { id: 1, name: 'Şubat' }, { id: 2, name: 'Mart' }, { id: 3, name: 'Nisan' }, { id: 4, name: 'Mayıs' }, { id: 5, name: 'Haziran' }, { id: 6, name: 'Temmuz' }, { id: 7, name: 'Ağustos' }, { id: 8, name: 'Eylül' }, { id: 9, name: 'Ekim' }, { id: 10, name: 'Kasım' }, { id: 11, name: 'Aralık' }];
  private events$ = new BehaviorSubject<EventViewModel[]>(null);
  private appUrl = environment.appUrl;

  getMonth(month: number) {

    if (month > 11 || month < 0) {
      throw "böyle bir ay değeri yok";
    }

    return this.months.find((v) => v.id == month);
  }

  fetchEvents() {
    this.http.get<EventViewModel[]>(this.appUrl + '/api/Event/GetEvents').subscribe(r => {
      this.events$.next(r);
    },err=>err);

  }


  addEvent(model: EventViewModel) {
    
    this.http.post<EventViewModel[]>(this.appUrl + '/api/Event/AddEvent', model).subscribe(r => {
      this.events$.next(r); 

      setTimeout(() => {
        this.router.navigate(['/takvim']);
      }, 300);
      
    },err=>err);

  }

  deleteEvent(model: EventViewModel) {
    this.http.post<EventViewModel[]>(this.appUrl + '/api/Event/DeleteEvent', model).subscribe(r => {
      this.events$.next(r);
    },err=>err);

  }

  getEvents() {
    return this.events$.asObservable();
  }


}

