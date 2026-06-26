import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DashboardDto } from '../models/DashboardDto';

@Injectable({
  providedIn: 'root',
})
export class DashbaordApi {
  private http = inject(HttpClient)

  private baseUrl = 'https://localhost:7170/api/backlog/dashboard';


  public getDashboard(): Observable<DashboardDto> {
    console.log("Getting Dashboard")
    return this.http.get<DashboardDto>(this.baseUrl);
  }



}

