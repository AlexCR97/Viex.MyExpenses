import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

const template = /*html*/`
<button
  class="btn d-flex justify-content-center align-items-center rounded-circle p-2"
  [disabled]="disabled"
  style="width: 40px; height: 40px;"
  (clicked)="onClicked()">
  <i [ngClass]="iconClass"></i>
</button>
`

@Component({
  selector: 'app-icon-button',
  template,
})
export class IconButtonComponent implements OnInit {

  @Input() disabled: boolean
  @Input() icon: string

  @Output() clicked = new EventEmitter<void>()

  constructor() { }

  ngOnInit(): void {
  }

  get iconClass() {
    return ['bi', `bi-${this.icon}`]
  }

  onClicked() {
    this.clicked.emit()
  }

}
