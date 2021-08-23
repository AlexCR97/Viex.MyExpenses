import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ValidationErrors } from 'fluentvalidation-ts/dist/ValidationErrors';
import { LoadingModalService } from '../components/modals/LoadingModal/loading-modal.service';
import { UserCredentials, UserCredentialsValidator } from '../models/UserCredentials';
import { ApiService } from '../services/api/api.service';
import timers from '../utils/timers';

const template = /*html*/`
<div class="d-flex justify-content-center align-items-center" style="height: 100vh;">
  <div class="container">
    <h1 class="text-center mb-5">My Expenses</h1>
    
    <div class="card shadow">
      <div class="card-body p-4">

        <app-text-field icon="envelope" placeholder="Email" type="email" [errorMessage]="credentialValidations.email" [(value)]="credentials.email" (enter)="onKeyEnterPressed()"></app-text-field>
        <div class="mb-4"></div>
        
        <app-text-field icon="lock" placeholder="Password" type="password" [errorMessage]="credentialValidations.password" [(value)]="credentials.password" (enter)="onKeyEnterPressed()"></app-text-field>
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

  credentials = new UserCredentials()
  credentialValidations: ValidationErrors<UserCredentials> = {}

  private credentialsValidator = new UserCredentialsValidator()

  constructor(
    private api: ApiService,
    private loadingModal: LoadingModalService,
    private router: Router,
  ) { }

  ngOnInit(): void { }

  onKeyEnterPressed() {
    console.log("onKeyEnterPressed PARENT");
    this.attemptAuthentication()
  }

  onLoginClicked() {
    this.attemptAuthentication()
  }

  private async authenticate(email: string, password: string) {
    this.loadingModal.open({ message: 'Signing In' })
    await timers.wait(1000)
    await this.api.auth.authenticate({ email, password })
    this.router.navigateByUrl('/app')
    this.loadingModal.close()
  }

  private async attemptAuthentication() {
    this.credentialValidations = this.credentialsValidator.validate(this.credentials)
    const credentialsAreValid = this.credentialsValidator.isValid(this.credentialValidations)

    console.log("credentialValidations:", this.credentialValidations);

    if (!credentialsAreValid)
      return
    
    this.authenticate(this.credentials.email, this.credentials.password)
  }

}
