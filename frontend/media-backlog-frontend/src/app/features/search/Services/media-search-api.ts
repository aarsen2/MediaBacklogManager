import { inject, Injectable } from '@angular/core';
import { SearchParameters } from '../models/SearchParameters';
import { SearchResultsDto } from '../models/SearchResultsDto';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';
import { CreationSearchQuery } from '../models/CreationSearchQuery';
import { ReadMediaDto } from '../../media/models/read/ReadMediaDto';

@Injectable({
  providedIn: 'root',
})
export class MediaSearchApi {
  private http = inject(HttpClient)
  private apiUrl = environment.apiUrl;
  private baseUrl = this.apiUrl + "/search";

  search(parameters: SearchParameters) {
    let httpParams = new HttpParams()

    if (parameters.genericSearch) {
      httpParams = httpParams.set('q', parameters.genericSearch);
    }

    if (parameters.genreSearch) {
      httpParams = httpParams.set('genre', parameters.genreSearch);
    }

    if (parameters.platformSearch) {
      httpParams = httpParams.set('platform', parameters.platformSearch);
    }

    if (parameters.recommenderSearch) {
      httpParams = httpParams.set('rec', parameters.recommenderSearch);
    }

    return this.http.get<SearchResultsDto>(`${this.baseUrl}`, { params: httpParams });
  }


  creationSearch(query: CreationSearchQuery) {
    let httpParams = new HttpParams()

    let url = `${this.baseUrl}/create/${query.mediaType}`
    console.log(url);
    httpParams = httpParams.set('title', query.title);

    return this.http.get<ReadMediaDto>(url, { params: httpParams });
  }


}
