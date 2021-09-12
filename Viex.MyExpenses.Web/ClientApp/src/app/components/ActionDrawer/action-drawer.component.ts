import { Component, Input, OnInit } from '@angular/core';
import { notNull } from 'src/app/utils/validators';
import { ActionDrawerService } from './action-drawer.service';
import { ActionDrawerItem } from './ActionDrawerItem';
import { ActionDrawerOptions } from './ActionDrawerOptions';

const template = /*html*/`
<div class="offcanvas offcanvas-bottom" id="actionDrawer" tabindex="-1" style="height: fit-content">
  <div class="offcanvas-body p-0">
    
  <div class="list-group">
    <div *ngFor="let item of actionItems" class="list-group-item list-group-item-action py-3" (click)="onActionItemClicked(item)">
      <app-flex align="center">
        <i class="bi ms-2 me-3" [ngClass]="getIconClass(item)" style="font-size: 22px"></i>
        <p class="m-0" [ngClass]="getLabelClass(item)">{{item.label}}</p>
      </app-flex>
    </div>
  </div>

  </div>
</div>
`

@Component({
  selector: 'app-action-drawer',
  template,
})
export class ActionDrawerComponent implements OnInit {

  @Input() options: ActionDrawerOptions

  constructor(
    private drawer: ActionDrawerService,
  ) { }

  ngOnInit(): void {
    this.options = this.drawer.defaultDrawerOptions

    this.drawer.watchOptions().subscribe({ next: options => {
      if (notNull(options))
        this.options = options
    }})
  }

  get actionItems() {
    return this.options.actionItems
  }

  getIconClass(item: ActionDrawerItem) {
     return [ 'bi', `bi-${item.icon}`, `text-${item.variant}` ]
  }

  getLabelClass(item: ActionDrawerItem) {
    return [ `text-${item.variant}` ]
  }

  onActionItemClicked(item: ActionDrawerItem) {
    item.action()
  }

}
