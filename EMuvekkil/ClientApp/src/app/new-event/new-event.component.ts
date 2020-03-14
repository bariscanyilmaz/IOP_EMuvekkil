import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { MatAutocompleteSelectedEvent, MatChipInputEvent, MatAutocomplete, DateAdapter, MAT_DATE_FORMATS } from '@angular/material';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { EventViewModel, UserModel } from '../shared/models';
import { UserService } from '../services/user.service';
import { AppDateAdapter, APP_DATE_FORMATS } from '../shared/format-datepicker';
import { Router } from '@angular/router';
import { CalendarService } from '../services/calendar.service';

@Component({
  selector: 'app-new-event',
  templateUrl: './new-event.component.html',
  styleUrls: ['./new-event.component.css'],
  providers: [{ provide: DateAdapter, useClass: AppDateAdapter },
  { provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS }]
})
export class NewEventComponent implements OnInit {

  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  userCtrl = new FormControl();
  filteredUsers: Observable<UserModel[]>;
  users: UserModel[] = [];

  @ViewChild('fruitInput') fruitInput: ElementRef<HTMLInputElement>;
  @ViewChild('auto') matAutocomplete: MatAutocomplete;


  spinners = false;
  startTime = { hour: new Date().getHours(), minute: new Date().getMinutes() };
  endTime = { hour: ((new Date().getHours() - 1) < 0 ? 23 : (new Date().getHours() - 1)), minute: new Date().getMinutes() };
  data: EventViewModel = {
    start: new Date(),
    id: 0,
    rememberDate: new Date(),
    title: '',
    users: []
  };

  constructor(private userService: UserService,private router:Router,private calendarService:CalendarService) {
    this.filteredUsers = this.userCtrl.valueChanges.pipe(
      startWith(null),
      map((fruit: string | null) => fruit ? this._filter(fruit) : this.users.slice()));
  }

  ngOnInit(): void {
    this.userService.getAllUsers().subscribe((r) => this.users = r, err => err);

  }

  add(event: MatChipInputEvent): void {

    if (!this.matAutocomplete.isOpen) {
      const input = event.input;
      const value = event.value;

      // Add our fruit
      if ((value || '').trim()) {
        //this.data.users.push(value.trim());
      }

      // Reset the input value
      if (input) {
        input.value = '';
      }

      this.userCtrl.setValue(null);
    }
  }

  remove(user: UserModel): void {
    const index = this.data.users.indexOf(user);

    if (index >= 0) {
      this.data.users.splice(index, 1);
    }
  }

  selected(event: MatAutocompleteSelectedEvent): void {

    this.data.users.push(event.option.value);
    this.fruitInput.nativeElement.value = '';
    this.userCtrl.setValue(null);
  }

  private _filter(value: string): UserModel[] {
    return this.users.filter(v => v.nameSurname.toLowerCase().indexOf(value) === 0);
  }

  addEveryOne() {
    this.data.users = this.users;
  }

  cancel(){
    this.router.navigate(['/takvim']);
  }

  save(){
    this.data.start.setHours(this.startTime.hour,this.startTime.minute);
    this.data.rememberDate.setHours(this.endTime.hour,this.endTime.minute);
    
    this.calendarService.addEvent(this.data);
  }
  
}
