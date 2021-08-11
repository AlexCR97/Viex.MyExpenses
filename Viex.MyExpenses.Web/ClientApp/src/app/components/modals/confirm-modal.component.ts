import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Modal } from 'src/app/plugins/bootstrap.plugin';
import { notNull } from 'src/app/utils/validators';

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

  private _opened: boolean;
  get opened() { return this._opened }
  @Input() set opened(opened: boolean) {
    this._opened = opened

    if (notNull(this.modal)) {
      if (opened == true)
        this.open()
      else
        this.close()
    }
  }
  @Output() openedChange = new EventEmitter<boolean>();

  @Input() title = "Are you sure?"
  @Input() message = "Do you really want to do this?"

  @Output() canceled = new EventEmitter<void>();
  @Output() closed = new EventEmitter<void>();
  @Output() confirmed = new EventEmitter<void>();

  private modalHtmlElement: HTMLElement
  private modal: any
  
  constructor() { }

  ngOnInit(): void {
    this.modalHtmlElement = document.getElementById('confirmModal')

    this.modal = new Modal(this.modalHtmlElement, {
      backdrop: 'static',
      keyboard: false,
    })

    this.modalHtmlElement.addEventListener('hidden.bs.modal', () => {
      this._opened = false // Do not trigger inner @Input
      this.openedChange.emit(this.opened)
      this.closed.emit()
    })
  }

  onCancelClicked() {
    this.close()
    this.canceled.emit()
  }

  onConfirmClicked() {
    this.close()
    this.confirmed.emit()
  }

  private close() {
    this.modal.hide()
  }

  private open() {
    this.modal.show()
  }
}
