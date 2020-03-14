import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { DAYS_OF_WEEK, CalendarEvent, CalendarEventAction, CalendarView } from 'angular-calendar';
import { addDays, subDays, startOfDay, endOfMonth, addHours, endOfDay, isSameMonth } from 'date-fns'
import { CalendarService, IMonth } from '../services/calendar.service';
import { MatDialog } from '@angular/material';
import { colors, EventViewModel } from '../shared/models';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';



@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {

  @ViewChild('modalContent') modalContent: TemplateRef<any>;

  viewDate: Date = new Date();

  locale: string = 'tr';
  weekStartsOn: number = DAYS_OF_WEEK.MONDAY;
  weekendDays: number[] = [DAYS_OF_WEEK.SATURDAY, DAYS_OF_WEEK.SUNDAY];
  refresh: Subject<any> = new Subject<any>();
  view: CalendarView = CalendarView.Month;

  CalendarView = CalendarView;


  modalData: {
    action: string;
    event: CalendarEvent;
  };


  myEvents: EventViewModel[] = [];

  activeDayIsOpen: boolean = false;

  displayedColumns: string[] = ['no', 'date', 'name', 'action'];
  dataSource = [];

  constructor(private calendarService: CalendarService, public dialog: MatDialog, private router: Router) {

  }

  ngOnInit(): void {
    this.calendarService.getEvents().subscribe(r => {
      if (r != null) {
        r.map(v=>{
          v.start=new Date(v.start);
          v.rememberDate=new Date(v.rememberDate);
        })
        this.myEvents = r;
      }

    })
  }


  dayClicked({ date, events }: { date: Date; events: EventViewModel[] }): void {
    this.dataSource = events;
  }

  handleEvent(action: string, event: CalendarEvent): void {

  }

  eventTimesChanged($event: any) {

  }

  newEvent(): void {
    this.router.navigate(['event']);
  }

  deleteEvent(eventToDelete: CalendarEvent) {

  }

  setView(view: CalendarView) {
    this.view = view;
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }

  getMonth() {
    return this.calendarService.getMonth(this.viewDate.getMonth()).name;
  }

  delete(model: EventViewModel) {
    const index = this.dataSource.indexOf(model);
    if (index > -1) {
      this.calendarService.deleteEvent(model);
      this.dataSource.splice(index, 1);
      this.refresh.next();
    }


  }


}
