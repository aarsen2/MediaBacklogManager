import { inject, Injectable } from '@angular/core';
import { CreateMediaDto } from '../../models/create/CreateMediaDto';
import { MediaApi } from './media-api';
import { ReadMediaDto } from '../../models/read/ReadMediaDto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MediaService {

  private readonly mediaApi = inject(MediaApi);


  private readMedia: Record<MediaType, (id: string) => Observable<any>> = {
    movie: id => this.mediaApi.getMovie(id),
    show: id => this.mediaApi.getMovie(id),
    game: id => this.mediaApi.getMovie(id),
    book: id => this.mediaApi.getMovie(id),
    album: id => this.mediaApi.getMovie(id),
    song: id => this.mediaApi.getMovie(id),
  };

  private readAllMedia: Record<MediaType, () => Observable<ReadMediaDto[]>> = {
    movie: () => this.mediaApi.getMovies(),
    show: () => this.mediaApi.getMovies(),
    game: () => this.mediaApi.getMovies(),
    book: () => this.mediaApi.getMovies(),
    album: () => this.mediaApi.getMovies(),
    song: () => this.mediaApi.getMovies(),
  };

private buildMedia: Record<MediaType, (dto: CreateMediaDto) => Observable<string>> = {
    movie: dto => this.mediaApi.createMovie(dto),
    show: dto => this.mediaApi.createMovie(dto),
    game: dto => this.mediaApi.createMovie(dto),
    book: dto => this.mediaApi.createMovie(dto),
    album: dto => this.mediaApi.createMovie(dto),
    song: dto => this.mediaApi.createMovie(dto),
  };

  createMedia(type: MediaType, mediaDto: CreateMediaDto): Observable<string> {
    return this.buildMedia[type](mediaDto);
  }

  getMedia(type: MediaType, id: string): Observable<ReadMediaDto> {
    return this.readMedia[type](id);
  }

  getAllMedia(type: MediaType): Observable<ReadMediaDto[]> {
    return this.readAllMedia[type]();
  }
}
