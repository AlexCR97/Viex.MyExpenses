import { EventEmitter, Input, Output } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Offcanvas } from '../plugins/bootstrap';
import { SideNavigationItem } from './SideNavigator/SideNavigationItem';

const template = /*html*/`
<div class="offcanvas offcanvas-bottom" tabindex="-1" id="appBottomDrawer" style="height: fit-content">

  <div class="offcanvas-body p-0">
    <ng-content></ng-content>
  </div>

</div>
`

@Component({
  selector: 'app-bottom-drawer',
  template,
})
export class BottomDrawerComponent implements OnInit {

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

  private drawerHtmlElement: HTMLElement
  private drawer: any

  constructor(
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.drawerHtmlElement = document.getElementById('appBottomDrawer')
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

  private open() {
    this.drawer.show()
  }

  private close() {
    this.drawer.hide()
  }

}
