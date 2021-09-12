import { Component, Input, OnInit } from '@angular/core';
import { User } from 'src/app/models/User.model';

const template = /*html*/`
<div class="list-group list-group-flush">
  <app-user-item *ngFor="let item of users"
    [user]="item">
  </app-user-item>
</div>
`
@Component({
  selector: 'app-user-list',
  template,
})
export class UserListComponent implements OnInit {

  @Input() users: User[]

  constructor() { }

  ngOnInit(): void {
  }

}
