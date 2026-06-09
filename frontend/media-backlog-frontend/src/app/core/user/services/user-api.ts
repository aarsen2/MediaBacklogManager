import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { UserDto } from "../models/UserDto";

@Injectable({
  providedIn: 'root',
})
export class UserApi {


  private http = inject(HttpClient)
  private baseUrl = "https://localhost:7170/api/User";



  getUser(): Observable<UserDto> {
    console.log("Getting User Info")
    return this.http.get<UserDto>(`${this.baseUrl}/me`)
  }
}
