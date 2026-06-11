import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MoviesApi } from '../../services/movies-api';
import { ReadMovieDto } from '../../../models/read/ReadMovieDto';

@Component({
  selector: 'app-movie-view',
  imports: [],
  templateUrl: './movie-view.html',
  styleUrl: './movie-view.css',
})
export class MovieView {
  private route = inject(ActivatedRoute);
  private mediaService = inject(MoviesApi);

  releaseDate = signal<Date | null>(null);
  media = signal<ReadMovieDto | null>(null);


  ngOnInit() {
    console.log("init")
    const type = this.route.snapshot.paramMap.get('type')!
    const id = this.route.snapshot.paramMap.get('id')!
    this.mediaService.getMovie(id).subscribe(data => {
      this.media.set(data);
      this.releaseDate.set(new Date(data.releaseDate))
    });

  }
}
