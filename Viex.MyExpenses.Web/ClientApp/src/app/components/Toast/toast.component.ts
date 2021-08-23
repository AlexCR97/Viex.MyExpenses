import { Component, Input, OnInit } from '@angular/core';
import { notNull } from 'src/app/utils/validators';
import { ToastOptions, ToastService } from './toast.service';

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

  @Input() options: ToastOptions

  constructor(
    private toast: ToastService,
  ) { }

  ngOnInit(): void {
    this.options = this.toast.getDefaultToastOptions()

    this.toast.watchOptions().subscribe({
      next: options => {
        if (notNull(options))
          this.options = options
      },
    })
  }

  get message() {
    return this.options.message
  }

  get toastClass() {
    const clazz = [ `bg-${this.options.variant}` ]

    if (this.options.variant != 'info')
      clazz.push('text-white')

    return clazz
  }
}
