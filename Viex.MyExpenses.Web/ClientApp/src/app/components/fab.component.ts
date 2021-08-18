import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

const template = /*html*/`
<div class="fab-container">
  <button class="btn fab-button" [ngClass]="fabClass" (click)="onClicked()">
    <i class="bi" [ngClass]="iconClass" [ngStyle]="iconStyle"></i>
  </button>
</div>
`

const styles = [/*css*/`
.fab-container {
  position: fixed;
  bottom: 0;
  right: 0;
  padding: 10px;
}

.fab-button {
  border-radius: 100%;
  width: 60px;
  height: 60px;
  display: flex;
  justify-content: center;
  align-items: center;
}

.fab-button:hover {
  cursor: pointer;
}

.fab-button .bi {
  color: whitesmoke;
  font-size: 36px;
}
`]

@Component({
  selector: 'app-fab',
  template,
  styles,
})
export class FabComponent implements OnInit {

  @Input() icon: string
  @Input() iconStyle: any
  @Input() variant: "primary" | "secondary" | "success" | "danger" = "primary"

  @Output() clicked = new EventEmitter<void>();

  constructor() { }

  ngOnInit(): void { }

  get iconClass() {
    return [ `bi-${this.icon}` ];
  }
  
  get fabClass() {
    return [ `btn-${this.variant}` ];
  }

  onClicked() {
    this.clicked.emit();
  }

}
