import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CredentialDto } from './models/CredentialDto';
import { AuthResponse } from './models/AuthResponse';
import { RegisterDto } from './models/RegisterDto';
import { environment } from '../../../environments/environment.development';


@Injectable({
  providedIn: 'root',
})
export class AuthApi {
  private apiUrl = environment.apiUrl;
  private baseUrl = this.apiUrl + "/auth";

  private http = inject(HttpClient)


  login(username: string, password: string): Observable<AuthResponse> {
    console.log("Logging in...");

    const dto: CredentialDto = {
      username: username,
      password: password
    };

    return this.http.post<AuthResponse>(`${this.baseUrl}/login`, dto)

  }

  register(registerDto: RegisterDto): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.baseUrl}/register`, registerDto)
  }

}
