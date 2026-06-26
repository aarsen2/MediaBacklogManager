import { DatePipe } from '@angular/common';
import { Component, inject, Input } from '@angular/core';
import { PriorityItemsReportDto } from '../../models/PriorityItemsReportDto';
import { Router } from '@angular/router';
import { PriorityItemsRowDto } from '../../models/PriorityItemsRowDto';

@Component({
  selector: 'app-priority-items-report',
  imports: [DatePipe],
  templateUrl: './priority-items-report.html',
  styleUrl: './priority-items-report.css',
})
export class PriorityItemsReport {
  @Input() report: PriorityItemsReportDto | null = null;
  private readonly router = inject(Router);

  openItem(item: PriorityItemsRowDto) {
    let url = `/media/view/${item.mediaType.toLowerCase()}/${item.id}`
    console.log(url)
    this.router.navigate([url]);
  }
}
