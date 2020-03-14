import { Component, OnInit, ViewChild, ElementRef, OnChanges, SimpleChanges } from '@angular/core';
import { AuthService } from './services/auth.service';
import { UserInfoModel, NotificationViewModel, EventViewModel } from './shared/models';
import { Subscription, Observable } from 'rxjs';
import { MatSidenav, MatDialog } from '@angular/material';
import { NotificationService } from './services/notification.service';
import { WarningDialogComponent } from './warning-dialog/warning-dialog.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  


  title = 'app';
  opened: boolean;
  isLoggedIn: boolean = false;
  userInfo: UserInfoModel;
  userInfoSub: Subscription;
  notifications: NotificationViewModel[] = [];
  unReadCount: number = 0;
  @ViewChild(MatSidenav) sidenav: MatSidenav;


  constructor(private authService: AuthService, private notificationService: NotificationService, private dialog: MatDialog) {

  }


  ngOnInit(): void {
    this.authService.isLoggedIn().subscribe(res => {
      this.isLoggedIn = res;
    });
    this.userInfoSub = this.authService.getUserInfo().subscribe(res => { this.userInfo = res; });

    this.notificationService.getNotifications(this.userInfo.id).subscribe(r => {
      console.log(r);
      this.notifications = r;
      (this.notifications.map(v => {
        if (!v.isRead) { this.unReadCount++ }
      }));
    }, err => err,()=>{
      
    });


  }

  logout() {
    //this.userInfoSub.unsubscribe();
    this.authService.logout();
  }

  show(model: NotificationViewModel) {
    this.dialog.open(WarningDialogComponent, { minWidth: '300px', data: { message: model.message } });
    if (!model.isRead) {
      this.notificationService.markReaded(model).subscribe(r => {
        
        this.notifications.map(i => {
          if (i.id == r.id) {
            i.isRead = true;
          }
        });
        
      },err=>err,()=>{
        this.unReadCount--;
      });
    }


  }

  calculateUnReadCount() {
    (this.notifications.map(v => {
      if (!v.isRead) { this.unReadCount++ }
    }));
  }


}
