import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DashboardDto } from '../pages/home-page/models/DashboardDto';

@Injectable({
  providedIn: 'root',
})
export class DashbaordApi {


  private baseUrl = 'https://localhost:7170/api/dashboard';
  
  constructor(private http: HttpClient) { }
  
  public getDashboard(): Observable<DashboardDto> {
    console.log("Getting Dashboard")
    return this.http.get<DashboardDto>(this.baseUrl);
  }



}

