import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { notNullNorWhitespace } from '../utils/validators';

const template = /*html*/`
<h6 *ngIf="hasLabel" class="ms-1">{{label}}</h6>
<select class="form-select" [(ngModel)]="value" (change)="onValueChanged()">
  <option *ngFor="let item of items" selected>{{item}}</option>
</select>
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
  selector: 'app-select',
  template,
  styles,
})
export class SelectComponent implements OnInit {

  private _value: any;
  get value() { return this._value }
  @Input() set value(value: any) { this._value = value }
  @Output() valueChange = new EventEmitter<any>();

  @Input() errorMessage: string
  @Input() items: any
  @Input() label: string

  constructor() { }

  ngOnInit(): void { }

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

  get hasErrorMessage() {
    return notNullNorWhitespace(this.errorMessage)
  }

  get hasLabel() {
    return notNullNorWhitespace(this.label)
  }

  onValueChanged() {
    this.valueChange.emit(this.value)
  }

}
