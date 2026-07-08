import { inject, Injectable } from '@angular/core';
import { SearchParameters } from '../models/SearchParameters';
import { MediaSearchApi } from './media-search-api';
import { SearchResultsDto } from '../models/SearchResultsDto';
import { Observable } from 'rxjs';
import { CreationSearchQuery } from '../models/CreationSearchQuery';
import { ReadMediaDto } from '../../media/models/read/ReadMediaDto';

@Injectable({
  providedIn: 'root',
})
export class MediaSearchService {
  private readonly searchApi = inject(MediaSearchApi)

  search(parameters: SearchParameters): Observable<SearchResultsDto> {
    return this.searchApi.search(parameters);
  }

  creationSearch(query: CreationSearchQuery): Observable<ReadMediaDto[]> {
    return this.searchApi.creationSearch(query);
  }
}
