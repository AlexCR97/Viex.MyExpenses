import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from 'src/app/models/User.model';
import { BaseApiService } from './BaseApiService';

@Injectable({
  providedIn: 'root'
})
export class UsersApiService extends BaseApiService {

  endpoint = 'users'

  constructor(
    private http: HttpClient,
  ) {
    super()
  }

  getAll = () => this.http.get<User[]>(this.url).toPromise();
}
