import { EventEmitter, Input, Output } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Offcanvas } from '../plugins/bootstrap.plugin';
import { notNull } from '../utils/validators';
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

    if (notNull(this.drawer)) {
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
      this._opened = false // Do not trigger inner @Input
      this.openedChange.emit(this.opened)
    })
  }

  private open() {
    this.drawer.show()
  }

  private close() {
    this.drawer.hide()
  }

}
