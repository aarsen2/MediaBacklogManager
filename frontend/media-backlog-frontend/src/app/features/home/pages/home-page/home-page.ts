import { Component } from '@angular/core';
import { ChangeDetectorRef } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { Observable } from 'rxjs';
import { RouterLink } from "@angular/router";
import { MoviesApi } from '../../../media/movies/services/movies-api';
import { ReadMovieDto } from '../../../media/models/read/ReadMovieDto';
import { MovieCarousel } from '../../../media/movies/componenets/movie-carousel/movie-carousel';

@Component({
  selector: 'app-home-page',
  imports: [AsyncPipe, MovieCarousel, RouterLink],
  templateUrl: './home-page.html',
  styleUrl: './home-page.css',
})
export class HomePage {

  movies$?: Observable<ReadMovieDto[]>;

  constructor(private api: MoviesApi) {
    this.GetMovies();
  }


  GetMovies() {
    this.movies$ = this.api.getMovies();
  }
}
