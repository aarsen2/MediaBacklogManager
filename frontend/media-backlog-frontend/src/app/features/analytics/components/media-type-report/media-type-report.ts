import { DatePipe } from '@angular/common';
import { Component, Input } from '@angular/core';
import { MediaTypeReportDto } from '../../models/MediaTypeReportDto';

@Component({
  selector: 'app-media-type-report',
  imports: [DatePipe],
  templateUrl: './media-type-report.html',
  styleUrl: './media-type-report.css',
})
export class MediaTypeReport {
  @Input() report: MediaTypeReportDto | null = null;

}
