import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { UserDto } from "../models/UserDto";
import { environment } from "../../../../environments/environment.development";

@Injectable({
  providedIn: 'root',
})
export class UserApi {
  private apiUrl = environment.apiUrl;

  private http = inject(HttpClient)
  private baseUrl = this.apiUrl + "/user";



  getUser(): Observable<UserDto> {
    console.log("Getting User Info")
    return this.http.get<UserDto>(`${this.baseUrl}/me`)
  }
}
