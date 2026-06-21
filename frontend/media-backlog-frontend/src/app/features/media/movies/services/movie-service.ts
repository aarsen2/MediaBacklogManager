import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateMovieDto } from '../../models/create/CreateMovieDto';
import { ReadMovieDto } from '../../models/read/ReadMovieDto';
import { MovieApi } from './movie-api';
import { MovieForm } from '../../models/forms/MovieForm';

@Injectable({
  providedIn: 'root',
})
export class MovieService {

  private readonly movieApi = inject(MovieApi);

  createMovie(movieForm: MovieForm): Observable<ReadMovieDto> {

    let movieDto = this.mapCreateDto(movieForm);
    return this.movieApi.createMovie(movieDto);
  }


  getMovie(id: string): Observable<ReadMovieDto> {
    return this.movieApi.getMovie(id);
  }

  getAllMovies(): Observable<ReadMovieDto[]> {
    return this.movieApi.getMovies();
  }

  mapCreateDto(movieForm: MovieForm): CreateMovieDto {
     return {
        // MediaBase / CreateMediaBase fields
        title: movieForm.title,
        description: movieForm.description,
        releaseDate: movieForm.releaseDate,
        genres: movieForm.genres ?? [],
        generalRating: movieForm.userRating,
        assets: [],

        // Movie-specific fields
        director: movieForm.director,
        runTime: movieForm.runTime,
        language: movieForm.language,
        contentRating: movieForm.contentRating
    };
  }


}
