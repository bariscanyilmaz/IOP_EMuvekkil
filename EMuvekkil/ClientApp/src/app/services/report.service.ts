import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ReportViewModel, ReportListViewModel } from '../shared/models';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  private appUrl = environment.appUrl;
  constructor(private http:HttpClient) { }

  getList(model:ReportViewModel){
    return this.http.get<ReportListViewModel>(this.appUrl+'/api/Report/GetList',{params:{
      startDate:model.startDate.toDateString(),
      endDate:model.endDate.toDateString(),
      avukatId:model.avukatId,
      muvekkilId:model.muvekkilId,
      davaId:model.davaId.toString(),
      dateDisabled:`${model.dateDisabled}`
    }});
  }

  getReport(){

  }

}
