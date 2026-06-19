import { inject, Injectable } from '@angular/core';
import { DashboardDto } from '../pages/home-page/models/DashboardDto';
import { DashbaordApi } from './dashbaord-api';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DashbaordService {
  private readonly dashboardApi = inject(DashbaordApi)

  public getDashboard(): Observable<DashboardDto> {
    const response = this.dashboardApi.getDashboard();
    console.log(response);
    return response;
  }

}
