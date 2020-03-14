import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { MaterialDesignModule } from "./material-design/material-design.module";
import { MuvekkillerComponent } from './muvekkiller/muvekkiller.component';
import { MuvekkilComponent } from './muvekkil/muvekkil.component';
import { AvukatlarComponent } from './avukatlar/avukatlar.component';
import { AvukatComponent } from './avukat/avukat.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { DavalarComponent } from './davalar/davalar.component';
import { DavaComponent } from './dava/dava.component';
import { MesajlarComponent } from './mesajlar/mesajlar.component';
import { NewMasrafDialogComponent } from './new-masraf-dialog/new-masraf-dialog.component';
import { NewDocumentDialogComponent } from './new-document-dialog/new-document-dialog.component';
import { NewMessageDialogComponent } from './new-message-dialog/new-message-dialog.component';
import { DokumanlarComponent } from './dokumanlar/dokumanlar.component';
import { LoginComponent } from './login/login.component';
import { SettingsComponent } from './settings/settings.component';
import { AuthInterceptor } from './services/auth.intercepter';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthGuard } from './services/auth.guard';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { CalendarComponent } from './calendar/calendar.component';
import { RoleGuard } from './services/role.guard';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { OwnerDirective } from './shared/owner.directive';
import { IsAdminDirective } from './shared/is-admin.directive';
import { SirketlerComponent } from './sirketler/sirketler.component';
import { ReportComponent } from './report/report.component';
import { DeleteConfirmationDialogComponent } from './delete-confirmation-dialog/delete-confirmation-dialog.component';
import { WarningDialogComponent } from './warning-dialog/warning-dialog.component';
import localeTr from "@angular/common/locales/tr";
import { registerLocaleData } from '@angular/common';

import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { NewEventComponent } from './new-event/new-event.component';


export function tokenGetter() {
  return localStorage.getItem('token');
}

registerLocaleData(localeTr);


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    MuvekkillerComponent,
    MuvekkilComponent,
    AvukatlarComponent,
    AvukatComponent,
    NotFoundComponent,
    DavalarComponent,
    DavaComponent,
    MesajlarComponent,
    NewMasrafDialogComponent,
    NewDocumentDialogComponent,
    NewMessageDialogComponent,
    DokumanlarComponent,
    LoginComponent,
    SettingsComponent,
    CalendarComponent,
    UnauthorizedComponent,
    OwnerDirective,
    IsAdminDirective,
    SirketlerComponent,
    ReportComponent,
    DeleteConfirmationDialogComponent,
    WarningDialogComponent,
    NewEventComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', pathMatch: 'full', redirectTo: 'home' },
      { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
      { path: 'muvekkiller', component: MuvekkillerComponent, canActivate: [AuthGuard, RoleGuard] },
      { path: 'muvekkil/:id', component: MuvekkilComponent, canActivate: [AuthGuard, RoleGuard] },
      { path: 'muvekkil', component: MuvekkilComponent, canActivate: [AuthGuard, RoleGuard] },
      { path: 'avukatlar', component: AvukatlarComponent, canActivate: [AuthGuard, RoleGuard] },
      { path: 'avukat', component: AvukatComponent, canActivate: [AuthGuard, RoleGuard] },
      { path: 'avukat/:id', component: AvukatComponent, canActivate: [AuthGuard, RoleGuard] },
      { path: 'davalar', component: DavalarComponent, canActivate: [AuthGuard] },
      { path: 'dava', component: DavaComponent, canActivate: [AuthGuard, RoleGuard] },
      { path: 'dava/:id', component: DavaComponent, canActivate: [AuthGuard] },
      { path: 'dava/edit/:id', component: DavaComponent, canActivate: [AuthGuard, RoleGuard] },
      { path: 'mesajlar', component: MesajlarComponent, canActivate: [AuthGuard] },
      { path: 'dokumanlar', component: DokumanlarComponent, canActivate: [AuthGuard] },
      { path: 'sirketler', component: SirketlerComponent, canActivate: [AuthGuard, RoleGuard] },
      { path: 'login', component: LoginComponent },
      { path: 'settings', component: SettingsComponent, canActivate: [AuthGuard] },
      { path: 'takvim', component: CalendarComponent, canActivate: [AuthGuard] },
      { path: 'unauthorized', component: UnauthorizedComponent },
      { path: 'rapor', component: ReportComponent, canActivate: [AuthGuard] },
      { path: 'event', component: NewEventComponent, canActivate: [AuthGuard] },
      { path: '**', component: NotFoundComponent },

    ]),
    MaterialDesignModule,
    NgbModule,
    ReactiveFormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter
      }
    })
  ],
  entryComponents: [NewMasrafDialogComponent, NewDocumentDialogComponent, NewMessageDialogComponent, DeleteConfirmationDialogComponent, WarningDialogComponent],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
