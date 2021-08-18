import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

const template = /*html*/`
<div class="d-flex justify-content-center align-items-center" style="height: 100vh;">
  <div class="container">
    <h1 class="text-center mb-5">My Expenses</h1>
    
    <div class="card shadow">
      <div class="card-body p-4">

        <app-text-field icon="envelope" placeholder="Email" type="email"></app-text-field>
        <div class="mb-4"></div>
        
        <app-text-field icon="lock" placeholder="Password" type="password"></app-text-field>
        <div class="mb-4"></div>
  
        <button class="btn btn-primary mb-4" style="width: 100%" (click)="onLoginClicked()">Login</button>
  
        <div class="d-flex justify-content-center">
          <button class="btn btn-link m-0">
            Or create an account
          </button>
        </div>
  
      </div>
    </div>
  </div>
</div>
`

@Component({
  selector: 'app-login',
  template,
})
export class LoginComponent implements OnInit {

  constructor(
    private router: Router,
  ) { }

  ngOnInit(): void {
  }

  onLoginClicked() {
    this.router.navigateByUrl('/app');
  }

}
