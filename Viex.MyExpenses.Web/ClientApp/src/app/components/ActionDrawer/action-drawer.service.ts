import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Offcanvas } from 'src/app/plugins/bootstrap.plugin';
import { isNull, notNull } from 'src/app/utils/validators';
import { ActionDrawerOptions } from './ActionDrawerOptions';

@Injectable({
  providedIn: 'root'
})
export class ActionDrawerService {

  defaultDrawerOptions: ActionDrawerOptions = {
    actionItems: [],
  }

  private drawerHtmlElement: HTMLElement
  private drawer: any
  private optionsSubject = new BehaviorSubject<ActionDrawerOptions>(null)

  open(options?: ActionDrawerOptions) {
    if (isNull(this.drawerHtmlElement)) {
      this.drawerHtmlElement = document.getElementById('actionDrawer')
      this.drawer = new Offcanvas(this.drawerHtmlElement)
    }

    const modalOptions = notNull(options)
      ? options
      : this.defaultDrawerOptions

    this.optionsSubject.next(modalOptions)
    this.drawer.show()
  }

  close() {
    this.drawer.hide()
  }

  watchOptions() {
    return this.optionsSubject.asObservable()
  }
}

export interface DrawerConfigurations {

}
