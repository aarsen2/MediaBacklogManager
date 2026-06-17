import { Component, computed, inject, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, of, switchMap } from 'rxjs';
import { MovieDetails } from '../../../movies/components/movie-details/movie-details';
import { ShowDetails } from '../../../shows/components/show-details/show-details';
import { toSignal } from '@angular/core/rxjs-interop';
import { MovieService } from '../../../movies/services/media-service';
import { ShowService } from '../../../shows/services/show-service';
import { AlbumSerivce } from '../../../albums/services/album-serivce';
import { BookService } from '../../../books/services/book-service';
import { GamesService } from '../../../games/services/games-service';
import { SongService } from '../../../songs/services/song-service';
import { AlbumDetails } from '../../../albums/components/album-details/album-details';
import { BookDetails } from '../../../books/components/book-details/book-details';
import { GameDetails } from '../../../games/components/game-details/game-details';
import { SongDetails } from '../../../songs/components/song-details/song-details';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-media-view',
  imports: [MovieDetails, ShowDetails, AlbumDetails, BookDetails, GameDetails, SongDetails, DatePipe],
  templateUrl: './media-view.html',
  styleUrl: './media-view.css',
})
export class MediaView {
  private readonly route = inject(ActivatedRoute);
  private readonly movieService = inject(MovieService);
  private readonly showService = inject(ShowService);
  private readonly albumService = inject(AlbumSerivce)
  private readonly bookService = inject(BookService)
  private readonly gameService = inject(GamesService)
  private readonly songService = inject(SongService)




  media = toSignal(
    this.route.paramMap.pipe(
      switchMap(params => {
        const type: MediaType = params.get('type')! as MediaType;
        const id = params.get('id')!;

        switch (type) {
          case 'movie':
            return this.movieService.getMovie(id);

          case 'show':
            return this.showService.getShow(id);

          case 'album':
            return this.albumService.getAlbum(id);

          case 'book':
            return this.bookService.getBook(id);

          case 'game':
            return this.gameService.getGame(id);

          case 'song':
            return this.songService.getSong(id);

          default:
            return new Observable<null>;
        }
      })
    ),
    { initialValue: null }

  );


  releaseDate = computed(() => {
    const m = this.media()
    return m ? new Date(m.releaseDate) : null
  })

}