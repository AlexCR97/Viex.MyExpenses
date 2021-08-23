import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Modal } from 'src/app/plugins/bootstrap.plugin';
import { notNull } from 'src/app/utils/validators';
import { ConfirmModalOptions, ConfirmModalService } from './confirm-modal.service';

const template = /*html*/`
<div id="confirmModal" class="modal fade" tabindex="-1">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">

      <div class="modal-header">
        <h5 class="modal-title" id="staticBackdropLabel">{{title}}</h5>
      </div>

      <div class="modal-body py-4">
        <p class="m-0">{{message}}</p>
      </div>

      <div class="modal-footer p-2">
        <button type="button" class="btn text-primary" (click)="onConfirmClicked()">Confirm</button>
        <button type="button" class="btn text-danger" (click)="onCancelClicked()">Cancel</button>
      </div>

    </div>
  </div>
</div>
`

@Component({
  selector: 'app-confirm-modal',
  template,
})
export class ConfirmModalComponent implements OnInit {

  @Input() options: ConfirmModalOptions
  
  constructor(
    private modal: ConfirmModalService,
  ) { }

  ngOnInit(): void {
    this.options = this.modal.getDefaultModalOptions()
    
    this.modal.watchOptions().subscribe({
      next: options => {
        if (notNull(options))
          this.options = options
      },
    })
  }

  get message() {
    return this.options.message
  }

  get title() {
    return this.options.title
  }

  async onConfirmClicked() {
    if (notNull(this.options.onConfirm)) {
      this.options.onConfirm()
      this.modal.close()
      return
    }

    if (notNull(this.options.onConfirmAsync)) {
      await this.options.onConfirmAsync()
      this.modal.close()
      return
    }
  }

  onCancelClicked() {
    this.modal.close()
  }
}
