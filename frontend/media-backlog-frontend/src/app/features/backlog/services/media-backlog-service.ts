import { inject, Injectable } from '@angular/core';
import { CreateUserMediaDto } from '../../media/models/create/CreateUserMediaDto';
import { HttpClient } from '@angular/common/http';
import { Observable, of, switchMap } from 'rxjs';
import { MediaForm } from '../../media/models/forms/MediaForm';
import { MovieService } from '../../media/movies/services/movie-service';
import { ShowService } from '../../media/shows/services/show-service';
import { AlbumSerivce } from '../../media/albums/services/album-serivce';
import { BookService } from '../../media/books/services/book-service';
import { GamesService } from '../../media/games/services/games-service';
import { SongService } from '../../media/songs/services/song-service';
import { UserMediaService } from '../../media/shared/services/user-media-service';
import { ReadMediaDto } from '../../media/models/read/ReadMediaDto';

@Injectable({
  providedIn: 'root',
})
export class MediaBacklogService {
  private movieSerivce = inject(MovieService);
  private showService = inject(ShowService);
  private albumService = inject(AlbumSerivce);
  private bookService = inject(BookService);
  private gameService = inject(GamesService);
  private songService = inject(SongService);
  private userMediaService = inject(UserMediaService)
  //Submition lobic
  submitHandlers: Record<string, (form: any) => Observable<ReadMediaDto>> = {
    movie: (p) => this.movieSerivce.createMovie(p),
    show: (p) => this.showService.createShow(p),
    album: (p) => this.albumService.createAlbum(p),
    book: (p) => this.bookService.createBook(p),
    game: (p) => this.gameService.createGame(p),
    song: (p) => this.songService.createSong(p),
  };


  CreateAndAddItem(form: MediaForm): Observable<any> {

    let mediaType = form.mediaType

    const handler = this.submitHandlers[mediaType];

    if (!handler) {
      console.error('The Handler does not support this data type')
      return of();
    }

    return handler(form).pipe(
      switchMap((mediaRes) => {
        const userMediaDto = this.mapAddDto(form, mediaRes.id);
        return this.userMediaService.addItem(userMediaDto);
    }
    ))

  }


  mapAddDto(movieForm: MediaForm, mediaId: number): CreateUserMediaDto {
    let priority: boolean = movieForm.prioritized.toLowerCase() == "yes" ? true : false;
    return {
      mediaId: mediaId,
      status: movieForm.status,
      prioritized: priority,
      userRating: movieForm.userRating,
      notes: movieForm.notes
    }
  }


}
