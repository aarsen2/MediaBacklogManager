import { inject, Injectable } from '@angular/core';
import { AnalyticsApi } from './analytics-api';
import { Observable } from 'rxjs';
import { CompletedItemsReportDto } from '../models/CompletedItemsReportDto';
import { MediaTypeReportDto } from '../models/MediaTypeReportDto';
import { PriorityItemsReportDto } from '../models/PriorityItemsReportDto';
import { TimeToCompleteReportDto } from '../models/TimeToCompleteReportDto';

@Injectable({
  providedIn: 'root',
})
export class AnalyticsService {
    private readonly analyticsApi = inject(AnalyticsApi);

    public getCompletedItemsReport(): Observable<CompletedItemsReportDto> {
      return this.analyticsApi.getCompletedItemsReport()
    }
    public getMediaTypeReport(): Observable<MediaTypeReportDto> {
      return this.analyticsApi.getMediaTypeReport()
    }
    public getPriorityItemsReport(): Observable<PriorityItemsReportDto> {
      return this.analyticsApi.getPriorityItemsReport()
    }
    public getTimeToCompleteReport(): Observable<TimeToCompleteReportDto> {
      return this.analyticsApi.getTimeToCompleteReport()
    }
}
