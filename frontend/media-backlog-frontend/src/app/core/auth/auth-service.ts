import { inject, Injectable } from '@angular/core';
import { AuthApi } from './auth-api';
import { map, Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private tokenkey = "auth_token";
  private authAPI = inject(AuthApi);
  constructor() { }

  isLoggedIn() {
    return !!localStorage.getItem(this.tokenkey);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenkey)
  }
  setToken(token: string): void {
    localStorage.setItem(this.tokenkey, token);
  }
  clearToken(): void {
    localStorage.removeItem(this.tokenkey);
  }


  login(username: string, password: string): Observable<void> {
    return this.authAPI.login(username, password).pipe(
      tap(response => {
        this.setToken(response.token);
      }
      ), map(() => { }));
  }
  logout(): void {
    this.clearToken()
  }
}
