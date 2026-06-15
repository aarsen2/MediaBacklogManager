import { inject, Injectable } from '@angular/core';
import { AlbumApi } from './album-api';
import { CreateAlbumDto } from '../../models/create/CreateAlbumDto';
import { Observable } from 'rxjs';
import { ReadAlbumDto } from '../../models/read/ReadAlbumDto';

@Injectable({
  providedIn: 'root',
})
export class AlbumSerivce {
  private readonly AlbumApi = inject(AlbumApi);

  createMedia(AlbumDto: CreateAlbumDto): Observable<string> {
    return this.AlbumApi.createAlbum(AlbumDto);
  }

  getAlbum(id: string): Observable<ReadAlbumDto> {  
    return this.AlbumApi.getAlbum(id);
  }

  getAllAlbums(): Observable<ReadAlbumDto[]> {
    return this.AlbumApi.getAlbums();
  }
}
