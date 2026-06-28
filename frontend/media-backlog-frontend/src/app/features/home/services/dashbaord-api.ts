import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DashboardDto } from '../models/DashboardDto';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class DashbaordApi {
  private http = inject(HttpClient)
  private apiUrl = environment.apiUrl;
  private baseUrl = this.apiUrl + "/backlog/dashboard";


  public getDashboard(): Observable<DashboardDto> {
    console.log("Getting Dashboard")
    return this.http.get<DashboardDto>(this.baseUrl);
  }



}

