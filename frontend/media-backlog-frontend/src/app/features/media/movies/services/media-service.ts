import { inject, Injectable } from '@angular/core';
import { MovieApi } from './media-api';
import { Observable } from 'rxjs';
import { CreateMovieDto } from '../../models/create/CreateMovieDto';
import { ReadMovieDto } from '../../models/read/ReadMovieDto';

@Injectable({
  providedIn: 'root',
})
export class MovieService {

  private readonly movieApi = inject(MovieApi);

  createMedia(movieDto: CreateMovieDto): Observable<string> {
    return this.movieApi.createMovie(movieDto);
  }

  getMovie(id: string): Observable<ReadMovieDto> {
    return this.movieApi.getMovie(id);
  }

  getAllMovies(): Observable<ReadMovieDto[]> {
    return this.movieApi.getMovies();
  }
}
