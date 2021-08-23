import { Component, Input, OnInit } from '@angular/core';
import { notNull, notNullNorWhitespace } from 'src/app/utils/validators';
import { LoadingModalOptions, LoadingModalService } from './loading-modal.service';

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

  @Input() options: LoadingModalOptions

  constructor(
    private modal: LoadingModalService,
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

  get hasMessage() {
    return notNullNorWhitespace(this.message)
  }
}

