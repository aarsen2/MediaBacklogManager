import { Component, effect, inject, signal } from '@angular/core';
import { CompletedItemsReportDto } from '../../models/CompletedItemsReportDto';
import { MediaTypeReportDto } from '../../models/MediaTypeReportDto';
import { PriorityItemsReportDto } from '../../models/PriorityItemsReportDto';
import { TimeToCompleteReportDto } from '../../models/TimeToCompleteReportDto';
import { toSignal } from '@angular/core/rxjs-interop';
import { AnalyticsService } from '../../services/analytics-service';
import { CompletedItemsRowDto } from '../../models/CompletedItemsRowDto';
import { DatePipe } from '@angular/common';
import { MediaTypeReport } from "../../components/media-type-report/media-type-report";
import { PriorityItemsReport } from "../../components/priority-items-report/priority-items-report";
import { CompletedItemsReport } from "../../components/completed-items-report/completed-items-report";
import { TimeToCompleteReport } from "../../components/time-to-complete-report/time-to-complete-report";

@Component({
  selector: 'app-analytics',
  imports: [DatePipe, MediaTypeReport, PriorityItemsReport, CompletedItemsReport, TimeToCompleteReport],
  templateUrl: './analytics.html',
  styleUrl: './analytics.css',
})
export class Analytics {

  analyticsService = inject(AnalyticsService)


  completedItemsReport = toSignal(
    this.analyticsService.getCompletedItemsReport(),
    {
      initialValue: {
        mediaItems: [],
        title: "",
        generatedAt: ""
      } as CompletedItemsReportDto
    }
  );
    mediaTypeReport = toSignal(
    this.analyticsService.getMediaTypeReport(),
    {
      initialValue: {
        mediaTypeRows: [],
        title: "",
        generatedAt: ""
      } as MediaTypeReportDto
    }
  );
    priorityItemsReport = toSignal(
    this.analyticsService.getPriorityItemsReport(),
    {
      initialValue: {
        mediaItems: [],
        title: "",
        generatedAt: ""
      } as PriorityItemsReportDto
    }
  );
    timeToCompleteReport = toSignal(
    this.analyticsService.getTimeToCompleteReport(),
    {
      initialValue: {
        timeToCompleteRows: [],
        title: "",
        generatedAt: ""
      } as TimeToCompleteReportDto
    }
  );

  constructor() {
  }
}
