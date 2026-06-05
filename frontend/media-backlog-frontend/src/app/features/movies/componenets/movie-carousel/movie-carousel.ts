import { Component, Input } from '@angular/core';
import { MovieDto } from '../../models/MovieDto';
import { ReadMovieDto } from '../../models/ReadMovieDto';

@Component({
  selector: 'app-movie-carousel',
  imports: [],
  templateUrl: './movie-carousel.html',
  styleUrl: './movie-carousel.css',
})
export class MovieCarousel {
@Input() movies: ReadMovieDto[] | null = [];
}
