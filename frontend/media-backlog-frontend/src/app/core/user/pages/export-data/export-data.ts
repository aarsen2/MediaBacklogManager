import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { MediaBacklogService } from '../../../../features/backlog/services/media-backlog-service';

@Component({
  selector: 'app-export-data',
  imports: [],
  templateUrl: './export-data.html',
  styleUrl: './export-data.css',
})
export class ExportData {
  selectedFile: File | null = null;
  isImporting = false;
  private mediaBacklogService = inject(MediaBacklogService)
  

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;

    if (!input.files || input.files.length === 0) {
      this.selectedFile = null;
      return;
    }

    this.selectedFile = input.files[0];
  }

  export() {
    this.mediaBacklogService.exportBacklog()
      .subscribe({
        next: (blob) => {
          const url = window.URL.createObjectURL(blob);
          const a = document.createElement('a');

          a.href = url;
          a.download = 'backlog-export.json';
          a.click();

          window.URL.revokeObjectURL(url);
        },
        error: () => {
          alert('Failed to export data.');
        }
      });
  }

}
