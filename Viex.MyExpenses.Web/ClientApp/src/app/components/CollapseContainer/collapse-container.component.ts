import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

const template = /*html*/`
<app-list-item [card]="true" [title]="title" (clicked)="onCollapseHeaderClicked()">
  <div slot="trailing" [ngStyle]="iconContainerStyle">
    <app-icon-button icon="caret-down" size="xs"></app-icon-button>
  </div>
</app-list-item>

<div [ngStyle]="collapseContainerStyle">
  <ng-content></ng-content>
</div>
`

@Component({
  selector: 'app-collapse-container',
  template,
})
export class CollapseContainerComponent implements OnInit {

  private _opened: boolean;
  get opened() { return this._opened }
  @Input() set opened(opened: boolean) { this._opened = opened }
  @Output() openedChange = new EventEmitter<boolean>();

  @Input() title: string

  constructor() { }

  ngOnInit(): void {}

  get collapseContainerStyle() {
    return {
      'max-height': this.opened
        ? '100vh'
        : '0px',
      'opacity': this.opened
        ? 1
        : 0,
      'overflow-y': this.opened
        ? 'default'
        : 'hidden',
      'transition': this.opened
        ? 'all 0.80s ease'
        : 'all 0.40s ease',
    }
  }

  get iconContainerStyle() {
    return {
      'transform': this.opened
        ? 'rotate(180deg)'
        : 'rotate(0deg)',
      'transition': 'all 0.40s ease',
    }
  }

  onCollapseHeaderClicked() {
    this.opened = !this.opened
    this.openedChange.emit(this.opened)
  }

}
