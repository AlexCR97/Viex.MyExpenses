import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OAuthResponse } from 'src/app/models/OAuthResponse.model';
import { UserCredentials } from 'src/app/models/UserCredentials.model';
import storage from 'src/app/storage';
import { BaseApiService } from './BaseApiService';

@Injectable({
  providedIn: 'root'
})
export class AuthApiService extends BaseApiService {

  endpoint: string = 'auth'

  constructor(
    private http: HttpClient,
  ) {
    super()
  }

  async authenticate(credentials: UserCredentials) {
    const request = {
      email: credentials.email,
      password: credentials.password,
      grantType: "client_credentials",
    }

    const response = await this.http.post<OAuthResponse>(`${this.url}/authenticate`, request).toPromise()
    storage.local.setAccessToken(response.access_token)
  }

  async impersonate(userId: number) {
    const response = await this.http.post<OAuthResponse>(`${this.url}/impersonate`, { userId }).toPromise()
    storage.local.setAccessToken(response.access_token)
  }
}
