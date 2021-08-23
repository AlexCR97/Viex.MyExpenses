import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OAuthResponse } from 'src/app/models/OAuthResponse';
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

  async authenticate(credentials: { email: string, password: string }) {
    const request = {
      email: credentials.email,
      password: credentials.password,
      grantType: "client_credentials",
    }

    const response = await this.http.post<OAuthResponse>(`${this.url}/authenticate`, request).toPromise()
    storage.local.setAccessToken(response.access_token)
  }
}
