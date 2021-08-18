import { Component, OnInit } from '@angular/core';

const template = /*html*/`
<div style="height: 50px">
  <app-header title="Settings" variant="menu"></app-header>
</div>
`

@Component({
  selector: 'app-settings',
  template,
})
export class SettingsComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
