import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Modal } from '../plugins/bootstrap.plugin';
import arrays from '../utils/arrays';
import dates, { DaysInMonth, MonthNames } from '../utils/dates';

const template = /*html*/`
<app-text-field [readonly]="true" action="calendar3" [value]="dateStr" (actionClicked)="onActionClicked()"></app-text-field>

<div id="datePickerModal" class="modal fade" tabindex="-1">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">

      <div class="modal-header">
        <h5 class="modal-title">Select a date</h5>
      </div>

      <div class="modal-body py-4">
        <div class="row">
          <div class="col p-1">
            <app-select label="Month" [items]="months" [(value)]="month" (valueChange)="onMonthChanged()"></app-select>
          </div>
          <div class="col p-1">
            <app-select label="Day" [items]="days" [(value)]="day"></app-select>
          </div>
          <div class="col p-1">
            <app-select label="Year" [items]="years" [(value)]="year"></app-select>
          </div>
        </div>
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
  selector: 'app-date-picker',
  template,
})
export class DatePickerComponent implements OnInit {

  private _value: Date = new Date();
  get value() { return this._value }
  @Input() set value(value: Date) { this._value = value }
  @Output() valueChange = new EventEmitter<Date>();

  @Input() errorMessage: string

  month = MonthNames[this.value.getMonth()]
  day = this.value.getDate()
  year = this.value.getFullYear()

  months = MonthNames
  days = arrays.fromRange(1, 32)
  years = arrays.fromRange(1900, new Date().getFullYear() + 1).reverse()

  private modalHtmlElement: HTMLElement
  private modal: any

  constructor() { }

  ngOnInit(): void {
    this.modalHtmlElement = document.getElementById('datePickerModal')
    this.modal = new Modal(this.modalHtmlElement)
  }

  get dateStr() {
    return this.value.toDateString()
  }

  onActionClicked() {
    this.openDatePickerModal()
  }

  onCancelClicked() {
    this.closeDatePickerModal()
  }

  onConfirmClicked() {
    const monthIndex = this.months.indexOf(this.month)
    this.value.setMonth(monthIndex)
    this.value.setDate(this.day)
    this.value.setFullYear(this.year)
    this.closeDatePickerModal()
  }

  onMonthChanged() {
    const days = DaysInMonth.get(this.month)
    this.days = arrays.fromRange(1, days + 1)
    this.day = 1;
  }

  private closeDatePickerModal() {
    this.modal.hide()
  }

  private openDatePickerModal() {
    this.modal.show()
  }

}
