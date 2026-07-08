import { Component, EventEmitter, Input, Output, signal } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MediaSearchService } from '../../../../search/Services/media-search-service';
import { CreationSearchQuery } from '../../../../search/models/CreationSearchQuery';
import { ReadMediaDto } from '../../../models/read/ReadMediaDto';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-metadata-search',
  imports: [ReactiveFormsModule, DatePipe],
  templateUrl: './metadata-search.html',
  styleUrl: './metadata-search.css',
})
export class MetadataSearch {

  @Input() mediaType!: MediaType;
  @Input() initialTitle!: string;
  @Output() itemSelected = new EventEmitter<ReadMediaDto>();
  searchControl = new FormControl<string>('', { nonNullable: true });
  results = signal<ReadMediaDto[]>([]);
  isLoading = signal(true);
  hasSearched = signal(false);

  ngOnInit() {
    if (this.initialTitle) {
      this.searchControl.setValue(this.initialTitle);

      this.search();
    }
  }

  constructor(private searchService: MediaSearchService) { }

  search() {
    const title = this.searchControl.value;
    if (!title) return;

    this.isLoading.set(true);

    const query: CreationSearchQuery = {
      title,
      mediaType: this.mediaType
    };

    this.searchService.creationSearch(query).subscribe({
      next: (res) => {
        this.results.set(res);
        this.isLoading.set(false);
      },
      error: () => this.isLoading.set(false)
    });
  }

  selectItem(item: ReadMediaDto) {
    this.itemSelected.emit(item);
  }
}
