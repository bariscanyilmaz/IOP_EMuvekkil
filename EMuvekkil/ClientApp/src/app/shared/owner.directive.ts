import { Directive, Input, ViewContainerRef, TemplateRef } from '@angular/core';
import { AuthService } from '../services/auth.service';


@Directive({
  selector: '[appOwner]'
})
export class OwnerDirective {



  @Input() set appOwner(value: string) {
    if (this.authService.getUser().email != value) {
      this.viewContainer.clear();
    } else {
      this.viewContainer.createEmbeddedView(this.templateRef);
    }
  }

  constructor(private viewContainer: ViewContainerRef, private templateRef: TemplateRef<any>, private authService: AuthService) {


  }






}
