import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CredentialDto } from './models/CredentialDto';
import { AuthResponse } from './models/AuthResponse';


@Injectable({
  providedIn: 'root',
})
export class AuthApi {

  private http = inject(HttpClient)
  private baseUrl =  "https://localhost:7170/api/Auth";


  login(username: string, password: string): Observable<AuthResponse> {
    console.log("Logging in...");

    const dto: CredentialDto = {
      username: username,
      password: password
    };

    return this.http.post<AuthResponse>(`${this.baseUrl}/login`, dto)

  }
}
