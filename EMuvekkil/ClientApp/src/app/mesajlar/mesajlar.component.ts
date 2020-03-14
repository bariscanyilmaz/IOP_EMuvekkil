import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { MessageViewModel, DocumentViewModel } from '../shared/models';
import { MessageService } from '../services/message.service';


@Component({
  selector: 'app-mesajlar',
  templateUrl: './mesajlar.component.html',
  styleUrls: ['./mesajlar.component.css']
})
export class MesajlarComponent implements OnInit {

  messages: MessageViewModel[] = [];
  displayedColumns: string[] = ['date', 'user', 'message', 'dava', 'active', 'action'];
  dataSource = new MatTableDataSource(this.messages);


  constructor(private messageService: MessageService) { }

  ngOnInit() {
    this.messageService.getMessages().subscribe(m => { this.messages = m; this.dataSource = new MatTableDataSource(this.messages); }, err => (err));
  }

  changeStatus(model: MessageViewModel) {
    this.messages.map((v) => { if (v.id == model.id) { v.isActive = !model.isActive; } });
    
    this.dataSource = new MatTableDataSource(this.messages);
    this.messageService.changeStatus(model).subscribe(r => (r), err => (err));
  }

  getStatus(isActive: boolean): string {
    return isActive ? 'Aktif' : 'Pasif';
  }

}
