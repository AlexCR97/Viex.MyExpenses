import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/User.model';
import { ApiService } from 'src/app/services/api/api.service';

const template = /*html*/`
<div style="height: 50px;">
  <app-header title="Users" variant="menu"></app-header>
</div>

<div class="py-1">
  <app-user-list [users]="users"></app-user-list>
</div>
`

@Component({
  selector: 'app-users-page',
  template,
})
export class UsersPageComponent implements OnInit {

  users: User[]
  loading = false

  constructor(
    private api: ApiService,
  ) { }

  ngOnInit(): void {
    this.init();
  }

  private async init() {
    this.loading = true
    this.users = await this.api.users.getAll()
    this.loading = false
  }

}
