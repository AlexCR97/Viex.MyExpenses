import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/User.model';
import { DynamicRoutes } from 'src/app/Routes';
import { ApiService } from 'src/app/services/api/api.service';
import random from 'src/app/utils/random';
import timers from 'src/app/utils/timers';
import { ActionDrawerService } from '../ActionDrawer/action-drawer.service';
import { LoadingModalService } from '../modals/LoadingModal/loading-modal.service';

const template = /*html*/`
<div class="list-group-item p-0 py-2 px-3" (click)="onClicked()">
  <app-flex align="center">
    
    <img class="rounded-circle me-3" style="width: 50px; height: 50px;" [src]="profileImg">

    <div>
      <h6>{{username}}</h6>
      <h6 style="color: gray">{{email}}</h6>
    </div>

  </app-flex>
</div>
`

@Component({
  selector: 'app-user-item',
  template,
})
export class UserItemComponent implements OnInit {

  @Input() user: User

  profileImg: string

  constructor(
    private actionDrawer: ActionDrawerService,
    private api: ApiService,
    private loadingModal: LoadingModalService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    const seed = random.int(0, 99)
    this.profileImg = `https://avatars.dicebear.com/api/identicon/${seed}.svg`
  }

  get email() {
    return this.user.email
  }

  get username() {
    return this.user.userName
  }

  onClicked() {
    this.actionDrawer.open({
      actionItems: [
        {
          icon: 'box-arrow-in-right',
          label: 'Impersonate',
          action: () => this.onActionImpersonateClicked(),
        },
        {
          icon: 'trash',
          label: 'Delete',
          variant: 'danger',
          action: () => {},
        },
      ],
    })
  }

  async onActionImpersonateClicked() {
    try {
      this.loadingModal.open({ message: 'Signing In...' })
      await timers.wait(1000)
      await this.api.auth.impersonate(this.user.userId)
      await this.router.navigateByUrl(DynamicRoutes.home())
      window.location.reload()
    } catch {
      this.loadingModal.close()
    }
  }

}
