import { DatePipe } from '@angular/common';
import { Component, inject, Input } from '@angular/core';
import { CompletedItemsReportDto } from '../../models/CompletedItemsReportDto';
import { CompletedItemsRowDto } from '../../models/CompletedItemsRowDto';
import { Router } from '@angular/router';

@Component({
  selector: 'app-completed-items-report',
  imports: [DatePipe],
  templateUrl: './completed-items-report.html',
  styleUrl: './completed-items-report.css',
})
export class CompletedItemsReport {
  @Input() report: CompletedItemsReportDto | null = null;
  private readonly router = inject(Router);

  openItem(item: CompletedItemsRowDto) {
    let url = `/media/view/${item.mediaType.toLocaleLowerCase()}/${item.id}`
    console.log(url)
    this.router.navigate([url]);
  }
}
