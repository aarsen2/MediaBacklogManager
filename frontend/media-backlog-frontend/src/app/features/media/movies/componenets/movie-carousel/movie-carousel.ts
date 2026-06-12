import { Component, inject, Input } from '@angular/core';
import { ReadMovieDto } from '../../../models/read/ReadMovieDto';
import { Router } from '@angular/router';
import { ReadMediaDto } from '../../../models/read/ReadMediaDto';

@Component({
  selector: 'app-movie-carousel',
  imports: [],
  templateUrl: './movie-carousel.html',
  styleUrl: './movie-carousel.css',
})
export class MovieCarousel {
  @Input() movies: ReadMediaDto[] | null = [];
  private router = inject(Router);


  viewMovie(movie: ReadMediaDto) {
    console.log("clicked");
    console.log(movie);
    this.router.navigate(['/media/view', "movie", movie.id])
  }

}
