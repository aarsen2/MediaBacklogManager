import { Component, Input } from '@angular/core';
import { ReadMovieDto } from '../../../models/read/ReadMovieDto';

@Component({
  selector: 'app-movie-details',
  imports: [],
  templateUrl: './movie-details.html',
  styleUrl: './movie-details.css',
})
export class MovieDetails {
  @Input() movie!: ReadMovieDto | null;
}
