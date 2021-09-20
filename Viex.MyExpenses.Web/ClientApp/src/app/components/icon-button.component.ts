import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { getButtonSize, SizeVariant } from '../types/SizeVariant.type';

const template = /*html*/`
<button
  class="btn d-flex justify-content-center align-items-center rounded-circle p-2"
  [disabled]="disabled"
  [ngStyle]="buttonStyle"
  (click)="onClicked()">
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
  @Input() size: SizeVariant = 'sm'

  @Output() clicked = new EventEmitter<void>()

  constructor() { }

  ngOnInit(): void {
  }

  get buttonStyle() {
    return {
      'width': getButtonSize(this.size),
      'height': getButtonSize(this.size),
    }
  }

  get iconClass() {
    return ['bi', `bi-${this.icon}`]
  }

  onClicked() {
    this.clicked.emit()
  }

}
