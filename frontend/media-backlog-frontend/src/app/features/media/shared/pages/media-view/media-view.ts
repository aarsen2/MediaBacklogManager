import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ReadMovieDto } from '../../../models/read/ReadMovieDto';
import { MoviesApi } from '../../../movies/services/movies-api';
import { MovieView } from "../../../movies/componenets/movie-view/movie-view";

@Component({
  selector: 'app-media-view',
  imports: [MovieView],
  templateUrl: './media-view.html',
  styleUrl: './media-view.css',
})
export class MediaView {
}