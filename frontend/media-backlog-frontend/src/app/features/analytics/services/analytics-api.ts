import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CompletedItemsReportDto } from '../models/CompletedItemsReportDto';
import { MediaTypeReportDto } from '../models/MediaTypeReportDto';
import { PriorityItemsReportDto } from '../models/PriorityItemsReportDto';
import { TimeToCompleteReportDto } from '../models/TimeToCompleteReportDto';

@Injectable({
  providedIn: 'root',
})
export class AnalyticsApi {
  
  private http = inject(HttpClient)

  private baseUrl = 'https://localhost:7170/api/analytics';


  public getCompletedItemsReport(): Observable<CompletedItemsReportDto> {
    console.log("Getting Completed Items Report")
    return this.http.get<CompletedItemsReportDto>(`${this.baseUrl}/completed`);
  }
  public getMediaTypeReport(): Observable<MediaTypeReportDto> {
    console.log("Getting Media Type Report")
    return this.http.get<MediaTypeReportDto>(`${this.baseUrl}/media-type`);
  }
  public getPriorityItemsReport(): Observable<PriorityItemsReportDto> {
    console.log("Getting Priority Items Report")
    return this.http.get<PriorityItemsReportDto>(`${this.baseUrl}/priority`);
  }
  public getTimeToCompleteReport(): Observable<TimeToCompleteReportDto> {
    console.log("Getting Completion Time Report")
    return this.http.get<TimeToCompleteReportDto>(`${this.baseUrl}/completion-time`);
  }


}
