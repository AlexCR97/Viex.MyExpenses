import { Component } from '@angular/core';

const template = /*html*/`
<app-action-drawer></app-action-drawer>
<app-confirm-modal></app-confirm-modal>
<app-loading-modal></app-loading-modal>
<app-toast></app-toast>

<router-outlet></router-outlet>
`;

@Component({
  selector: 'app-root',
  template,
})
export class AppComponent {
  
}
