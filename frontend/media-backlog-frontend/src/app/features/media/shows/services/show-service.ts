import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateShowDto } from '../../models/create/CreateShowDto';
import { ReadShowDto } from '../../models/read/ReadShowDto';
import { ShowApi } from './show-api';

@Injectable({
  providedIn: 'root',
})
export class ShowService {

  private readonly movieApi = inject(ShowApi);

  createShow(movieDto: CreateShowDto): Observable<string> {
    return this.movieApi.createShow(movieDto);
  }

  getShow(id: string): Observable<ReadShowDto> {
    return this.movieApi.getShow(id);
  }

  getAllShows(): Observable<ReadShowDto[]> {
    return this.movieApi.getShows();
  }
}
