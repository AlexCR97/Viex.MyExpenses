import { Component, Input, OnInit } from '@angular/core';

const template = /*html*/`
<div class="fixed-top shadow py-1" style="background-color: white;">
  <div class="row m-0 p-0">

    <div class="col p-0 px-1">
      <button class="btn d-flex justify-content-center align-items-center rounded-circle p-2" style="width: 40px; height: 40px;" (click)="onMenuClicked()">
        <i class="bi bi-list"></i>
      </button>
    </div>

    <div class="col d-flex justify-content-center align-items-center p-0">
      <p class="fw-bold m-0">{{title}}</p>
    </div>

    <div class="col d-flex justify-content-end p-0 px-1">
      <button class="btn d-flex justify-content-center align-items-center rounded-circle p-2" style="width: 40px; height: 40px;">
        <i class="bi bi-three-dots-vertical"></i>
      </button>
    </div>

  </div>
</div>

<br><br><br>
<br><br><br>

<app-side-navigator [(opened)]="drawerOpened"></app-side-navigator>
`

@Component({
  selector: 'app-header',
  template,
})
export class HeaderComponent implements OnInit {

  @Input() title: string
  
  drawerOpened = false

  constructor() { }

  ngOnInit(): void {
  }

  onMenuClicked() {
    this.drawerOpened = true
  }

}
