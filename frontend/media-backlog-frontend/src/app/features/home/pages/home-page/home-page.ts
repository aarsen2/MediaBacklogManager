import { Component, inject } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { RouterLink } from "@angular/router";
import { MovieService } from '../../../media/movies/services/media-service';
import { toSignal } from '@angular/core/rxjs-interop';
import { MediaCarousel } from "../../../media/shared/components/media-carousel/media-carousel";
import { ShowService } from '../../../media/shows/services/show-service';
import { AlbumSerivce } from '../../../media/albums/services/album-serivce';
import { BookService } from '../../../media/books/services/book-service';
import { GamesService } from '../../../media/games/services/games-service';
import { SongService } from '../../../media/songs/services/song-service';

@Component({
  selector: 'app-home-page',
  imports: [RouterLink, MediaCarousel],
  templateUrl: './home-page.html',
  styleUrl: './home-page.css',
})
export class HomePage {
  private readonly movieService = inject(MovieService)
  private readonly showService = inject(ShowService)
  private readonly albumService = inject(AlbumSerivce)
  private readonly bookService = inject(BookService)
  private readonly gameService = inject(GamesService)
  private readonly songService = inject(SongService)

  movies = toSignal(
    this.movieService.getAllMovies() , {initialValue: null}
  )
  shows = toSignal(
    this.showService.getAllShows() , {initialValue: null}
  )
  albums = toSignal(
    this.albumService.getAllAlbums() , {initialValue: null}
  )
  books = toSignal(
    this.bookService.getAllBooks() , {initialValue: null}
  )
  games = toSignal(
    this.gameService.getAllGames() , {initialValue: null}
  )
  songs = toSignal(
    this.songService.getAllSongs() , {initialValue: null}
  )




}
