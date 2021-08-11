import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

const template = /*html*/`
<div class="fab-container">
  <button class="btn fab-button" [ngClass]="fabClass">
    <i class="bi" [ngClass]="iconClass"></i>
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
  width: 50px;
  height: 50px;
  display: flex;
  justify-content: center;
  align-items: center;
}

.fab-button:hover {
  cursor: pointer;
}

.fab-button .bi {
  color: whitesmoke;
  font-size: 28px;
}
`]

@Component({
  selector: 'app-fab',
  template,
  styles,
})
export class FabComponent implements OnInit {

  @Input() icon: string
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
