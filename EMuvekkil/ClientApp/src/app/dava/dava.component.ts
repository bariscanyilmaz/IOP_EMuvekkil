import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog, MatTableDataSource } from '@angular/material';
import { NewMasrafDialogComponent } from '../new-masraf-dialog/new-masraf-dialog.component';
import { NewDocumentDialogComponent } from '../new-document-dialog/new-document-dialog.component';
import { NewMessageDialogComponent } from '../new-message-dialog/new-message-dialog.component';
import { UserModel, DavaViewModel, MasrafViewModel, MessageViewModel, DocumentViewModel, DavaStateViewModel } from '../shared/models';
import { UserService } from '../services/user.service';
import { DavaService } from '../services/dava.service';
import { MasrafService } from '../services/masraf.service';
import { MessageService } from '../services/message.service';
import { AuthService } from '../services/auth.service';
import { DocumentService } from '../services/document.service';
import { DeleteConfirmationDialogComponent } from '../delete-confirmation-dialog/delete-confirmation-dialog.component';


@Component({
  selector: 'app-dava',
  templateUrl: './dava.component.html',
  styleUrls: ['./dava.component.css']
})
export class DavaComponent implements OnInit {

  isNewRecord: boolean;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  thirdFormGroup: FormGroup;
  isAdmin: boolean = false;

  muvekkils: UserModel[] = [];
  lawyers: UserModel[] = [];
  davaStates: DavaStateViewModel[] = [];

  documents: DocumentViewModel[] = [];
  messages: MessageViewModel[] = [];
  masrafs: MasrafViewModel[] = [];

  masrafSource = new MatTableDataSource(this.masrafs);
  documentSource = new MatTableDataSource(this.documents);
  messageSource = new MatTableDataSource(this.messages);

  davaModel: DavaViewModel = { id: 0, avukatId: '', avukatName: '', muvekkilId: '', muvekkilName: '', name: '', davaStateId: 0, davaStateText: '' };

  constructor(private _formBuilder: FormBuilder, private route: ActivatedRoute,
    public dialog: MatDialog, private userService: UserService,
    private davaService: DavaService, private router: Router, private masrafService: MasrafService,
    private messageService: MessageService, private authService: AuthService,
    private documentService: DocumentService) { }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get('id');
    this.isAdmin = (this.authService.getUserRole() == 'admin');
    this.isNewRecord = id ? false : true;
    if (!this.isNewRecord) {

      this.davaService.getDava(id).subscribe(d => this.davaModel = d, err =>(err));
      this.masrafService.getMasrafsByDavaId(parseInt(id)).subscribe(m => { this.masrafs = m; this.masrafSource = new MatTableDataSource(this.masrafs) }, err => (err));
      this.messageService.getMessagesByDavaId(parseInt(id)).subscribe(m => { this.messages = m; this.messageSource = new MatTableDataSource(this.messages) }, err => (err));
      this.documentService.getDocumentsByDavaId(parseInt(id)).subscribe(m => { this.documents = m; this.documentSource = new MatTableDataSource(this.documents) }, err =>(err));

    }

    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: ['', Validators.required]
    });
    this.secondFormGroup = this._formBuilder.group({
      secondCtrl: ['', Validators.required]
    });
    this.thirdFormGroup = this._formBuilder.group({
      thirdCtrl: ['', Validators.required]
    });

    this.userService.getLawyers().subscribe(s => this.lawyers = s);
    this.userService.getMuvekkils().subscribe(s => this.muvekkils = s);
    this.davaService.getDavaStates().subscribe(r => this.davaStates = r);
  }

  masrafColumns: string[] = ['date', 'user', 'amount', 'description', 'action'];
  documentColumns: string[] = ['date', 'user', 'name', 'description', 'active', 'action'];
  messageColumns: string[] = ['date', 'user', 'message', 'active', 'action'];


  addNewMasraf() {
    const masrafDialogRef = this.dialog.open(NewMasrafDialogComponent, {
      minWidth: '300px',
      data: {
        id: 0,
        ownerUserName: '',
        ownerName: '',
        davaId: 0,
        amount: 0,
        description: '',
        date: new Date(),
      }
    });

    masrafDialogRef.afterClosed().subscribe((result: MasrafViewModel) => {
      if (result) {
        result.davaId = this.davaModel.id;
        this.masrafService.addMasraf(result).subscribe(res => { this.masrafs.push(res); this.masrafSource = new MatTableDataSource(this.masrafs) }, err => console.log(err));
      }

    })
  }

  addNewDocument() {
    const documentDialogRef = this.dialog.open(NewDocumentDialogComponent, {
      minWidth: '300px',
      data: { id: 0, date: new Date(), name: '', description: '', file: null, davaId: this.davaModel.id, ownerUserName: this.authService.getUser().email, }
    });

    documentDialogRef.afterClosed().subscribe((result: DocumentViewModel) => {

      this.documentService.getDocumentsByDavaId(this.davaModel.id).subscribe(r => {
        this.documents = r;
        this.documentSource = new MatTableDataSource(this.documents);
      });

    })
  }

  addNewMessage() {
    const documentDialogRef = this.dialog.open(NewMessageDialogComponent, {
      minWidth: '300px',
      data: { user: '', date: new Date(), message: '' }
    });

    documentDialogRef.afterClosed().subscribe((result) => {
      if (result) {
        let messagevm: MessageViewModel = {
          davaId: this.davaModel.id,
          date: result.date,
          id: 0,
          text: result.message,
          davaName: this.davaModel.name,
          ownerName: this.authService.getUser().nameSurname,
          ownerUserName: this.authService.getUser().email,
          isActive: true
        };

        this.messageService.addMessage(messagevm).subscribe(r => { this.messages.push(r); this.messageSource = new MatTableDataSource(this.messages) }, err => console.log(err));
      }

    })
  }

  getTotalCost() {
    return this.masrafs.map(t => t.amount).reduce((acc, value) => acc + value, 0);
  }

  saveNew() {
    let dava = {
      name: (this.firstFormGroup.get('firstCtrl').value),
      muvekkilId: (this.secondFormGroup.get('secondCtrl').value),
      avukatId: (this.thirdFormGroup.get('thirdCtrl').value),
    } as DavaViewModel;

    this.davaService.createNew(dava).subscribe(res => setTimeout(() => {
      this.router.navigate(['/davalar']);
    }, 300), err => console.log(err));

  }

  updateDava() {
    this.davaService.updateDava(this.davaModel).subscribe(res => (res), err => (err));
  }

  deleteDava() {
    const dialogRef = this.dialog.open(DeleteConfirmationDialogComponent, { minWidth: '300xp' });
    dialogRef.afterClosed().subscribe(res=>{
      if (res) {
        this.davaService.deleteDava(this.davaModel).subscribe(res => setTimeout(() => { this.router.navigate(['/davalar']) }, 300), err => console.log(err));    
      }
    })
    
  }

  download(model: DocumentViewModel) {
    this.documentService.downloadDocument(model.id).subscribe(r => {
      var a = document.createElement("a");
      a.href = URL.createObjectURL(r);
      a.download = model.fileName;
      // start download
      a.click();
    }, err => { console.log(err) });
  }

  changeMessageStatus(model: MessageViewModel) {
    this.messages.map((v) => { if (v.id == model.id) { v.isActive = !model.isActive } })
    this.messageSource = new MatTableDataSource(this.messages);
    this.messageService.changeStatus(model).subscribe(r => r, err => console.log(err));
  }

  changeDocumentStatus(model: DocumentViewModel) {
    this.documents.map((v) => { if (v.id == model.id) { v.isActive = !model.isActive } });
    this.documentSource = new MatTableDataSource(this.documents);
    this.documentService.changeStatus(model).subscribe(r => r, err => console.log(err));
  }

  deleteMasraf(element: MasrafViewModel) {
    const dialogRef = this.dialog.open(DeleteConfirmationDialogComponent, { minWidth: '300xp' });
    dialogRef.afterClosed().subscribe(res => {
      if (res) {
        this.masrafService.deleteMasraf(element).subscribe(r => {
          let index = this.masrafs.findIndex(d => d.id == r.id);
          if (index > -1) {
            this.masrafs.splice(index, 1);
            this.masrafSource = new MatTableDataSource(this.masrafs);
          }
        }, err => console.log(err));
      }
    })

  }

  getStatus(isActive: boolean): string {
    return isActive ? 'Aktif' : 'Pasif';
  }

}
