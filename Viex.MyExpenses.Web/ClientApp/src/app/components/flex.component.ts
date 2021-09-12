import { Component, Input, OnInit } from '@angular/core';

const template = /*html*/`
<div class="d-flex" [ngClass]="flexContainerClass">
  <ng-content></ng-content>
</div>
`

@Component({
  selector: 'app-flex',
  template,
})
export class FlexComponent implements OnInit {

  @Input() align: AlignItems
  @Input() justify: JustifyContent = 'start'
  @Input() padding = 0
  @Input() paddingX = 0
  @Input() paddingY = 0

  constructor() { }

  ngOnInit(): void {
  }

  get flexContainerClass() {
    return [
      `align-items-${this.align}`,
      `justify-content-${this.justify}`,
      `p-${this.padding}`,
      `px-${this.paddingX}`,
      `py-${this.paddingY}`,
    ]
  }

}

export type AlignItems = 'center' | 'end' | 'start'
export type JustifyContent = 'center' | 'end' | 'start'
