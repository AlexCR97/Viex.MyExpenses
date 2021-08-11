import { Component, OnInit } from '@angular/core';

const template = /*html*/`
<div style="height: 50px">
  <app-header title="Home"></app-header>
</div>
`

@Component({
  selector: 'app-home',
  template,
})
export class HomeComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
