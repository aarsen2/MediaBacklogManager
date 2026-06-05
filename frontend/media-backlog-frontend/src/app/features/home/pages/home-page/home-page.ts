import { Component } from '@angular/core';
import { ChangeDetectorRef } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { Observable } from 'rxjs';
import { MoviesApi } from '../../../movies/services/movies-api';
import { ReadMovieDto } from '../../../movies/models/ReadMovieDto';
import { MovieCarousel } from "../../../movies/componenets/movie-carousel/movie-carousel";
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-home-page',
  imports: [AsyncPipe, MovieCarousel, RouterLink],
  templateUrl: './home-page.html',
  styleUrl: './home-page.css',
})
export class HomePage {

  movies$?: Observable<ReadMovieDto[]>;

  constructor(private api: MoviesApi, private cdr: ChangeDetectorRef) {
    this.GetMovies();
  }


  GetMovies() {
    this.movies$ = this.api.getMovies();
  }
}
