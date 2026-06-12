import { Component, inject } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { RouterLink } from "@angular/router";
import { MovieCarousel } from '../../../media/movies/componenets/movie-carousel/movie-carousel';
import { MediaService } from '../../../media/shared/services/media-service';
import { toSignal } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-home-page',
  imports: [MovieCarousel, RouterLink],
  templateUrl: './home-page.html',
  styleUrl: './home-page.css',
})
export class HomePage {
  private readonly mediaService = inject(MediaService)

  movies = toSignal(
    this.mediaService.getAllMedia('movie') , {initialValue: null}
  )


}
