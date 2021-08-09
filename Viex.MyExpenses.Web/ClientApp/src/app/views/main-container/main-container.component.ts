import { Component, OnInit } from '@angular/core';

const template = /*html*/`
<router-outlet></router-outlet>
`

@Component({
  selector: 'app-main-container',
  template,
})
export class MainContainerComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
