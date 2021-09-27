import { Injectable } from '@angular/core';
import { AuthApiService } from './auth-api.service';
import { DescriptorsService } from './descriptors.service';
import { UsersApiService } from './users-api.service';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(
    public auth: AuthApiService,
    public descriptors: DescriptorsService,
    public users: UsersApiService,
  ) { }
}
