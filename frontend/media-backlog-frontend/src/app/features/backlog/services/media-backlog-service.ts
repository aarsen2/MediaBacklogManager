import { inject, Injectable } from '@angular/core';
import { CreateUserMediaDto } from '../../media/models/create/CreateUserMediaDto';
import { HttpClient } from '@angular/common/http';
import { Observable, of, switchMap } from 'rxjs';
import { BaseForm } from '../../media/models/forms/BaseForm';
import { MediaForm } from '../../media/models/forms/MediaForm';
import { MovieService } from '../../media/movies/services/movie-service';
import { ShowService } from '../../media/shows/services/show-service';
import { AlbumSerivce } from '../../media/albums/services/album-serivce';
import { BookService } from '../../media/books/services/book-service';
import { GamesService } from '../../media/games/services/games-service';
import { SongService } from '../../media/songs/services/song-service';
import { UserMediaService } from '../../media/shared/services/user-media-service';
import { ReadMediaDto } from '../../media/models/read/ReadMediaDto';
import { ReadBacklogItemDto } from '../../media/models/read/ReadBacklogItemDto';
import { BacklogApi } from './backlog-api';
import { UpdateBacklogItemDto } from '../../media/models/update/UpdateBacklogItemDto';
import { CreateMediaDto } from '../../media/models/create/CreateMediaDto';
import { UpdateMediaDto } from '../../media/models/update/UpdateMediaDto';

@Injectable({
  providedIn: 'root',
})
export class MediaBacklogService {
  private movieService = inject(MovieService);
  private showService = inject(ShowService);
  private albumService = inject(AlbumSerivce);
  private bookService = inject(BookService);
  private gameService = inject(GamesService);
  private songService = inject(SongService);
  private userMediaService = inject(UserMediaService)
  private backlogApi = inject(BacklogApi)
  //Submition lobic


  creationHandlers: Record<string, (form: any) => Observable<ReadMediaDto>> = {
    movie: (p) => this.movieService.createMovie(p),
    show: (p) => this.showService.createShow(p),
    album: (p) => this.albumService.createAlbum(p),
    book: (p) => this.bookService.createBook(p),
    game: (p) => this.gameService.createGame(p),
    song: (p) => this.songService.createSong(p),
  };

  getMediaHandlers: Record<string, (mediaId: any) => Observable<ReadMediaDto>> = {
    movie: (p) => this.movieService.getMovie(p),
    show: (p) => this.showService.getShow(p),
    album: (p) => this.albumService.getAlbum(p),
    book: (p) => this.bookService.getBook(p),
    game: (p) => this.gameService.getGame(p),
    song: (p) => this.songService.getSong(p),
  };




  CreateAndAddItem(form: BaseForm): Observable<any> {

    let mediaType = form.mediaType

    const handler = this.creationHandlers[mediaType];

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

  UpdateItem(form: MediaForm, mediaId: string) {
    let updateDto = this.buildDto(form, mediaId)
    console.log(form)
    return this.backlogApi.updateBacklogItem(updateDto, mediaId);
  }

  public getBacklogItem(id: string): Observable<ReadBacklogItemDto> {
    return this.backlogApi.getBacklogItem(id);
  }


  toggleFavorite(id: number): Observable<ReadBacklogItemDto> {
    return this.backlogApi.togglePriority(id);
  }


  mapAddDto(mediaForm: BaseForm, mediaId: number): CreateUserMediaDto {
    let priority: boolean = mediaForm.prioritized.toLowerCase() == "yes" ? true : false;
    return {
      mediaId: mediaId,
      status: mediaForm.status,
      prioritized: priority,
      userRating: mediaForm.userRating,
      notes: mediaForm.notes
    }
  }

  buildDto(form: MediaForm, mediaId: string): UpdateBacklogItemDto {
    let priority: boolean = form.prioritized === 'yes' ? true : false

    if (form.mediaType == 'book') {
      console.log(form.language);
    }
    return {
      mediaId: mediaId,
      status: form.status,
      prioritized: priority,
      userRating: form.userRating,
      notes: form.notes,
      media: this.buildMedia(form, mediaId)
    };
  }

  buildMedia(form: MediaForm, mediaId: string): UpdateMediaDto {
    const base = {
      id: Number.parseInt(mediaId),
      title: form.title,
      generalRating: 0,
      assets: [],
      description: form.description,
      releaseDate: form.releaseDate,
      genres: form.genres
    };

    switch (form.mediaType) {
      case 'movie':
        return {
          type: 'movie',
          ...base,
          director: form.director,
          language: form.language,
          runTime: form.runTime,
          contentRating: form.contentRating
        };

      case 'show':
        return {
          type: 'show',
          ...base,
          seasonCount: form.seasonCount,
          episodeCount: form.episodeCount,
          contentRating: form.contentRating
        };

      case 'album':
        return {
          type: 'album',
          ...base,
          artist: form.artist,
          trackCount: form.trackCount,
          runTime: form.runTime
        };

      case 'book':
        return {
          type: 'book',
          ...base,
          author: form.author,
          pageCount: form.pageCount,
          language: form.language
        };

      case 'game':
        return {
          type: 'game',
          ...base,
          platforms: form.platforms,
          contentRating: form.contentRating,
          studio: form.studio
        };

      case 'song':
        return {
          type: 'song',
          ...base,
          artist: form.artist,
          runTime: form.runTime
        };

      default:
        throw new Error(`Unsupported media type:`);
    }
  }
}

