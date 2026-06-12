import { Component, computed, inject, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, switchMap } from 'rxjs';
import { MovieDetails } from '../../../movies/componenets/movie-details/movie-details';
import { ShowDetails } from '../../../shows/componenets/show-details/show-details';
import { toSignal } from '@angular/core/rxjs-interop';
import { MediaService } from '../../services/media-service';

@Component({
  selector: 'app-media-view',
  imports: [MovieDetails, ShowDetails],
  templateUrl: './media-view.html',
  styleUrl: './media-view.css',
})
export class MediaView {
  private route = inject(ActivatedRoute);
  private mediaService = inject(MediaService);

  private mediaFetchers: Record<MediaType, (id: string) => Observable<any>> = {
    movie: id => this.mediaService.readMedia(id),
    show: id => this.mediaService.readMedia(id),
    game: id => this.mediaService.readMedia(id),
    book: id => this.mediaService.readMedia(id),
    album: id => this.mediaService.readMedia(id),
    song: id => this.mediaService.readMedia(id),
  };



  media = toSignal(
    this.route.paramMap.pipe(
      switchMap(params => {
        const type: MediaType = params.get('type')! as MediaType;
        const id = params.get('id')!;
        return this.mediaFetchers[type](id);
      })
    ),

  );


  releaseDate = computed(() => {
    const m = this.media()
    return m ? new Date(m.releaseDate) : null
  })

}