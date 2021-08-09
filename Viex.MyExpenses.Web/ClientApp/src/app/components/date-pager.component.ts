import { Component, OnInit } from '@angular/core';
import dates from '../utils/dates';

const template = /*html*/`
<h3 class="text-center mb-4">{{month}}</h3>

<div class="d-flex justify-content-center align-items-center">
  
  <button class="btn rounded-circle d-flex justify-content-center align-items-center" style="width: 40px; height: 40px;">
    <i class="bi bi-chevron-left"></i>
  </button>

  <div class="mx-3">
    <p class="text-center m-0">
      <b>{{dateFrom}}</b> to <b>{{dateTo}}</b>
    </p>
    <p class="text-center m-0">Week {{currentWeek}}</p>
  </div>

  <button class="btn rounded-circle d-flex justify-content-center align-items-center" style="width: 40px; height: 40px;">
    <i class="bi bi-chevron-right"></i>
  </button>

</div>
`

@Component({
  selector: 'app-date-pager',
  template,
})
export class DatePagerComponent implements OnInit {

  startDate = dates.firstOfCurrentWeek()
  endDate = dates.lastOfCurrentWeek()
  currentWeek = dates.currentWeek()

  constructor() { }

  ngOnInit(): void {
  }

  get dateFrom() {
    return dates.short(this.startDate)
  }

  get dateTo() {
      return dates.short(this.endDate)
  }

  get month() {
    return dates.monthName(this.startDate)
  }

}
