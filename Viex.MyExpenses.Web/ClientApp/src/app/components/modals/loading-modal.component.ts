import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Modal } from 'src/app/plugins/bootstrap.plugin';
import { notNull, notNullNorWhitespace } from 'src/app/utils/validators';

const template = /*html*/`
<div id="loadingModal" class="modal fade" tabindex="-1">
  <div class="modal-dialog modal-dialog-centered">

    <div style="width: 100%">
      <h5 *ngIf="hasMessage" class="text-center text-light">{{message}}</h5>
      <div class="d-flex justify-content-center" style="width: 100%">
        <div class="spinner-grow text-info" style="width: 3rem; height: 3rem;" role="status">
          <span class="visually-hidden">Loading...</span>
        </div>
      </div>
    </div>

  </div>
</div>
`

@Component({
  selector: 'app-loading-modal',
  template,
})
export class LoadingModalComponent implements OnInit {

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

  @Input() message: string

  private modalHtmlElement: HTMLElement
  private modal: any

  constructor() { }

  ngOnInit(): void {
    this.modalHtmlElement = document.getElementById('loadingModal')

    this.modal = new Modal(this.modalHtmlElement, {
      backdrop: 'static',
      keyboard: false,
    })

    this.modalHtmlElement.addEventListener('hidden.bs.modal', () => {
      this._opened = false // Do not trigger inner @Input
      this.openedChange.emit(this.opened)
    })
  }

  get hasMessage() {
    return notNullNorWhitespace(this.message)
  }

  private close() {
    this.modal.hide()
  }

  private open() {
    this.modal.show()
  }

}
