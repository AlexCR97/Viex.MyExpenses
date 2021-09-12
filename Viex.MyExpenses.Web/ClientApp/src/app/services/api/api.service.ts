import { Injectable } from '@angular/core';
import { AuthApiService } from './auth-api.service';
import { UsersApiService } from './users-api.service';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(
    public auth: AuthApiService,
    public users: UsersApiService,
  ) { }
}
