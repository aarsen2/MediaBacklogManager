// core/services/user.service.ts
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { UserApi } from './user-api';
import { UserDto } from '../models/UserDto';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private userApi = inject(UserApi)


  private user: UserDto | null = null;

  setUser(user: UserDto) {
    this.user = user;
  }

  getUser(): UserDto | null {
    return this.user;
  }

  clearUser() {
    this.user = null;
  }


  loadUser() {
    this.userApi.getUser().subscribe(user => {
      this.user = user;
    });
  }
}
