import { Component, OnInit } from '@angular/core';

const template = /*html*/`
<div class="d-flex align-items-center">
  <img class="rounded-circle" src="https://via.placeholder.com/100" alt="Profile image" style="width: 50px; height: 50px;">
  <div class="mx-2"></div>
  <div>
    <p class="m-0">Username</p>
    <p class="m-0">
      <span>Total Balance: </span><b>$1500.00</b>
    </p>
  </div>
</div>
`

@Component({
  selector: 'app-user-dropdown',
  template,
})
export class UserDropdownComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
