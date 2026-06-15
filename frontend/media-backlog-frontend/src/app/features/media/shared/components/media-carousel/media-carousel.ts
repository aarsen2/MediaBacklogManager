import { Component, inject, Input } from '@angular/core';
import { ReadMediaDto } from '../../../models/read/ReadMediaDto';
import { Router } from '@angular/router';

@Component({
  selector: 'app-media-carousel',
  imports: [],
  templateUrl: './media-carousel.html',
  styleUrl: './media-carousel.css',
})
export class MediaCarousel {
  @Input()  media: ReadMediaDto[] | null = [];
  private router = inject(Router);


  viewMovie(movie: ReadMediaDto) {
    console.log("clicked");
    console.log(movie);
    this.router.navigate(['/media/view', movie.type, movie.id])
  }

}
