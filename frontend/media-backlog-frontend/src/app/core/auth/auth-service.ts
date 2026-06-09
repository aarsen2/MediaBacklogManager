import { inject, Injectable } from '@angular/core';
import { AuthApi } from './auth-api';
import { map, Observable, tap } from 'rxjs';
import { AuthResponse } from './models/AuthResponse';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private tokenkey = "auth_token";
  private expirationKey = "token_expiration";
  private refreshTokenKey = "refresh_token";
  private authAPI = inject(AuthApi);
  constructor() { }

  isLoggedIn(): boolean {
    const token = this.getToken();
    return !!token;
  }


  getToken(): string | null {
    return localStorage.getItem(this.tokenkey) ?? null;
  }

  getExpiration(): string | null {
    return localStorage.getItem(this.expirationKey);
  }
  getRefreshToken(): string | null {
    return localStorage.getItem(this.refreshTokenKey);
  }



  private setToken(token: AuthResponse): void {
    localStorage.setItem(this.tokenkey, token.token);
    localStorage.setItem(this.expirationKey, token.expiresIn?.toString() ?? "");
    localStorage.setItem(this.refreshTokenKey, token.refreshToken ?? "");
  }
  private clearToken(): void {
    localStorage.removeItem(this.tokenkey);
    localStorage.removeItem(this.expirationKey);
    localStorage.removeItem(this.refreshTokenKey);
  }

  login(username: string, password: string): Observable<AuthResponse> {
    return this.authAPI.login(username, password).pipe(
      tap(response => this.setToken(response))
    );
  }

  logout(): void {
    this.clearToken()
  }
}
