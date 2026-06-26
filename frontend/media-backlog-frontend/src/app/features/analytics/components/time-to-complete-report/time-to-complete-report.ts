import { DatePipe, DecimalPipe } from '@angular/common';
import { Component, Input } from '@angular/core';
import { TimeToCompleteReportDto } from '../../models/TimeToCompleteReportDto';

@Component({
  selector: 'app-time-to-complete-report',
  imports: [DatePipe, DecimalPipe],
  templateUrl: './time-to-complete-report.html',
  styleUrl: './time-to-complete-report.css',
})
export class TimeToCompleteReport {
  @Input() report: TimeToCompleteReportDto | null = null;
}
