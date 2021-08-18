import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Toast } from '../plugins/bootstrap.plugin';
import { notNull } from '../utils/validators';

const template = /*html*/`
<div class="position-fixed bottom-0 end-0 p-3" style="z-index: 11; width: 100%">
  <div id="toast" class="toast" [ngClass]="toastClass" role="alert" aria-live="assertive" aria-atomic="true" data-bs-delay="1500">
    <div class="d-flex">
      <div class="toast-body">
        {{message}}
      </div>
      <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
    </div>
  </div>
</div>
`

@Component({
  selector: 'app-toast',
  template,
})
export class ToastComponent implements OnInit {

  private _opened: boolean;
  get opened() { return this._opened }
  @Input() set opened(opened: boolean) {
    this._opened = opened

    if (notNull(this.toast)) {
      if (opened == true)
        this.open()
      else
        this.close()
    }
  }
  @Output() openedChange = new EventEmitter<boolean>();

  @Input() message: string
  @Input() variant: "primary" | "secondary" | "dark" | "light" | "info" | "success" | "danger"

  private toastHtmlElement: HTMLElement
  private toast: any

  constructor() { }

  ngOnInit(): void {
    this.toastHtmlElement = document.getElementById('toast')
    this.toast = new Toast(this.toastHtmlElement)

    this.toastHtmlElement.addEventListener('hidden.bs.toast', () => {
      this.opened = false
      this.openedChange.emit(this.opened)
    })
  }

  get toastClass() {
    const clazz = [ `bg-${this.variant}` ]

    if (this.variant != 'info')
      clazz.push('text-white')

    return clazz
  }

  close() {
    this.toast.hide()
  }

  open() {
    this.toast.show()
  }

}
