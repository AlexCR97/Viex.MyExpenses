import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { notNullNorWhitespace } from '../utils/validators';

const template = /*html*/`
<h6 *ngIf="hasLabel" class="ms-1">{{label}}</h6>
<div class="input-group">
  <span *ngIf="hasIcon" class="input-group-text" [ngClass]="iconClass"></span>
  <input class="form-control" [placeholder]="placeholderValue" [readonly]="readonly" [type]="type" [(ngModel)]="value" (change)="onValueChanged()" (keyup.enter)="onKeyEnterPressed()">
  <button *ngIf="hasAction" class="btn btn-outline-secondary d-flex justify-content-center align-items-center" (click)="onActionClicked()">
    <span [ngClass]="actionClass"></span>
  </button>
</div>
<p class="error-message text-danger m-0" [ngClass]="errorMessageClass" [ngStyle]="errorMessageStyle">{{errorMessage}}</p>
`

const styles = [/*css*/`
.error-message {
  transition: all ease 0.5s;
}

.error-message-shake {
  animation: shake 0.82s cubic-bezier(.36,.07,.19,.97) both;
  transform: translate3d(0, 0, 0);
  backface-visibility: hidden;
  perspective: 1000px;
}

@keyframes shake {
  10%, 90% {
    transform: translate3d(-1px, 0, 0);
  }
  
  20%, 80% {
    transform: translate3d(2px, 0, 0);
  }

  30%, 50%, 70% {
    transform: translate3d(-4px, 0, 0);
  }

  40%, 60% {
    transform: translate3d(4px, 0, 0);
  }
}
`]

@Component({
  selector: 'app-text-field',
  template,
  styles,
})
export class TextFieldComponent implements OnInit {

  private _value: any;
  get value() { return this._value }
  @Input() set value(value: any) { this._value = value }
  @Output() valueChange = new EventEmitter<any>();

  @Input() action: string
  @Input() errorMessage: string
  @Input() icon: string
  @Input() label: string
  @Input() placeholder: string
  @Input() readonly: boolean
  @Input() type: string

  @Output() actionClicked = new EventEmitter<void>()
  @Output() enter = new EventEmitter<void>()

  constructor() { }

  ngOnInit(): void { }

  get actionClass() {
    return [ 'bi' , `bi-${this.action}` ]
  }

  get errorMessageClass() {
    return {
      'error-message-shake': this.hasErrorMessage,
    }
  }

  get errorMessageStyle() {
    return {
      'width': !this.hasErrorMessage ? 0 : undefined,
      'height': !this.hasErrorMessage ? 0 : undefined,
      'opacity': !this.hasErrorMessage ? 0 : 1,
    }
  }

  get hasAction() {
    return notNullNorWhitespace(this.action)
  }

  get hasErrorMessage() {
    return notNullNorWhitespace(this.errorMessage)
  }

  get hasIcon() {
    return notNullNorWhitespace(this.icon)
  }

  get hasLabel() {
    return notNullNorWhitespace(this.label)
  }

  get placeholderValue() {
    return notNullNorWhitespace(this.placeholder)
      ? this.placeholder
      : ''
  }

  get iconClass() {
    return [ 'bi', `bi-${this.icon}`]
  }

  onActionClicked() {
    this.actionClicked.emit()
  }

  onKeyEnterPressed() {
    console.log("onKeyEnterPressed CHILD");
    this.enter.emit()
  }

  onValueChanged() {
    this.valueChange.emit(this.value)
  }

}
