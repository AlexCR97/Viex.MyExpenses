import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core'
import { Router } from '@angular/router';
import storage from 'src/app/storage';
import { Offcanvas } from '../../plugins/bootstrap.plugin'
import { DefaultSideNavigationItems, SideNavigationItem } from './SideNavigationItem';

const template = /*html*/`
<div class="offcanvas offcanvas-start" style="max-width: 70vw;" tabindex="-1" id="appDrawer" aria-labelledby="appDrawerLabel">

  <div class="offcanvas-header">
    <app-user-dropdown></app-user-dropdown>
  </div>

  <div class="offcanvas-body p-0">
    <div class="list-group list-group-flush">
      <div *ngFor="let item of navigationItems" class="list-group-item list-group-item-action py-3" [ngClass]="getListItemClass(item)" (click)="onListItemClicked(item)">
        <div class="d-flex align-items-center">
          <i [ngClass]="getIconClass(item)"></i>
          <div class="mx-2"></div>
          <h6 class="m-0">{{item.label}}</h6>
        </div>
      </div>
    </div>
  </div>

</div>
`

@Component({
  selector: 'app-side-navigator',
  template,
})
export class DrawerComponent implements OnInit {

  private _opened: boolean = false;
  get opened() { return this._opened }
  @Input() set opened(opened: boolean) {
    this._opened = opened;

    if (this.drawer != undefined && this.drawer != null) {
      if (opened == true)
        this.open()
      else
        this.close()
    }
  }
  @Output() openedChange = new EventEmitter<boolean>();

  @Input() navigationItems: SideNavigationItem[] = [
    ...DefaultSideNavigationItems,
    {
      icon: 'box-arrow-right',
      label: 'Sign Out',
      click: () => this.onSignOutClicked(),
    },
  ]
  
  private drawerHtmlElement: HTMLElement
  private drawer: any

  constructor(
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.drawerHtmlElement = document.getElementById('appDrawer')
    this.drawer = new Offcanvas(this.drawerHtmlElement)
    this.drawerHtmlElement.addEventListener('hidden.bs.offcanvas', () => {
      this.opened = false
      this.openedChange.emit(this.opened)
    })
  }

  getIconClass(item: SideNavigationItem) {
    return [ 'bi', `bi-${item.icon}`];
  }

  getListItemClass(item: SideNavigationItem) {
    return {
      'active': this.router.url.includes(item.path),
    };
  }

  onListItemClicked(item: SideNavigationItem) {
    if (item.path)
      this.router.navigateByUrl(item.path);
    else if (item.click)
      item.click()
    
    this.close();
  }

  onSignOutClicked() {
    storage.local.removeAccessToken()
    this.router.navigateByUrl('/login')
  }

  private open() {
    this.drawer.show()
  }

  private close() {
    this.drawer.hide()
  }
}
