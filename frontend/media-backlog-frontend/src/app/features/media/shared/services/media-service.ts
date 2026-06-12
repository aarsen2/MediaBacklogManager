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

  createMedia<T>(mediaDto: CreateMediaDto): Observable<string> {
    return this.mediaApi.createMovie(mediaDto);
  }

  readMedia<T>(id: string): Observable<ReadMediaDto> {
    return this.mediaApi.getMovie(id);
  }

  readAllMedia<T>(): Observable<ReadMediaDto[]>{
    return this.mediaApi.getMovies();
  }
}
