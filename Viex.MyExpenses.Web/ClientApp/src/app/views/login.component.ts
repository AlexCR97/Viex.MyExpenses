import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

const template = /*html*/`
<div class="d-flex justify-content-center align-items-center" style="height: 100vh;">
  <div class="container">
    <h1 class="text-center mb-5">My Expenses</h1>
    
    <div class="card shadow">
      <div class="card-body p-4">
  
        <div class="d-flex align-items-center mb-3">  
          <i class="bi bi-envelope"></i>
          <div class="mx-2"></div>
          <input class="form-control" placeholder="Email" type="email">
        </div>
  
        <div class="d-flex align-items-center mb-3">
          <i class="bi bi-lock"></i>
          <div class="mx-2"></div>
          <input class="form-control" placeholder="Password" type="password">
        </div>
  
        <button class="btn btn-primary mb-3" style="width: 100%" (click)="onLoginClicked()">Login</button>
  
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
