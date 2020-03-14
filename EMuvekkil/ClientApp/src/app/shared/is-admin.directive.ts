import { Directive, ViewContainerRef, TemplateRef, OnInit,  ElementRef } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Directive({
  selector: '[appIsAdmin]'
})
export class IsAdminDirective implements OnInit {



  constructor(private viewContainer: ViewContainerRef, private templateRef: TemplateRef<any>, private authService: AuthService) {

  }

  ngOnInit(): void {
    if (this.authService.getUserRole() != 'admin') {
      this.viewContainer.clear();
    }else{
      this.viewContainer.createEmbeddedView(this.templateRef);
    }
  }

}
