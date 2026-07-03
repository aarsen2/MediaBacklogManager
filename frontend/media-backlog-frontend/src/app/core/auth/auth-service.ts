import { inject, Injectable, signal } from '@angular/core';
import { AuthApi } from './auth-api';
import { map, Observable, tap } from 'rxjs';
import { AuthResponse } from './models/AuthResponse';
import { Router } from '@angular/router';
import { UserService } from '../user/services/user-service';
import { RegisterDto } from './models/RegisterDto';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private tokenkey = "auth_token";
  private expirationKey = "token_expiration";
  private refreshTokenKey = "refresh_token";
  private authAPI = inject(AuthApi);
  private router = inject(Router)
  constructor() {
    this.isLoggedInSignal.set(this.isLoggedIn())
  }

  isLoggedIn(): boolean {
    const token = this.getToken();

    if (token != null) {
      const now = new Date().getTime();
      const expirationString = this.getExpiration();
      if (expirationString == null) {
        return false;
      }
      const expirationTimestamp = new Date(expirationString).getTime()
      if (now < expirationTimestamp) {
        console.log(now)
        console.log(expirationTimestamp)
        return true
      }
    }
    return false;
  }

  isLoggedInSignal = signal<boolean>(false)


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
    this.setExpiration(token.expiresIn);
    localStorage.setItem(this.refreshTokenKey, token.refreshToken ?? "");
  }

  private setExpiration(expiresIn: number) {
    let expirationTime = new Date();
    expirationTime.setSeconds(expirationTime.getSeconds() + expiresIn);
    localStorage.setItem(this.expirationKey, expirationTime.toISOString());
  }

  private clearToken(): void {
    localStorage.removeItem(this.tokenkey);
    localStorage.removeItem(this.expirationKey);
    localStorage.removeItem(this.refreshTokenKey);
  }

  login(username: string, password: string): Observable<AuthResponse> {
    return this.authAPI.login(username, password).pipe(
      tap(response => {
        this.setToken(response)
        this.isLoggedInSignal.set(true);
      })
    );
  }


  register(registerDto: RegisterDto) {
    return this.authAPI.register(registerDto).pipe(
      tap(response => {
        this.setToken(response)
        this.isLoggedInSignal.set(true);
      })
    )
  }

  logout(): void {
    this.clearToken()
    this.router.navigate(['/logout']);
    this.isLoggedInSignal.set(false);
  }
}
