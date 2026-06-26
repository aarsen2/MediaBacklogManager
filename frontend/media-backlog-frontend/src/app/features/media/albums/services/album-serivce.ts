import { inject, Injectable } from '@angular/core';
import { AlbumApi } from './album-api';
import { CreateAlbumDto } from '../../models/create/CreateAlbumDto';
import { Observable } from 'rxjs';
import { ReadAlbumDto } from '../../models/read/ReadAlbumDto';
import { AlbumForm } from '../../models/forms/AlbumForm';

@Injectable({
  providedIn: 'root',
})
export class AlbumSerivce {
  private readonly AlbumApi = inject(AlbumApi);

  createAlbum(albumForm: AlbumForm): Observable<ReadAlbumDto> {
    let albumDto = this.mapCreateDto(albumForm)
    return this.AlbumApi.createAlbum(albumDto);
  }

  getAlbum(id: string): Observable<ReadAlbumDto> {
    return this.AlbumApi.getAlbum(id);
  }

  getAllAlbums(): Observable<ReadAlbumDto[]> {
    return this.AlbumApi.getAlbums();
  }


  mapCreateDto(albumForm: AlbumForm): CreateAlbumDto {
    return {
      type: 'album',
      // MediaBase / CreateMediaBase fields
      title: albumForm.title,
      description: albumForm.description,
      releaseDate: albumForm.releaseDate,
      genres: albumForm.genres ?? [],
      generalRating: albumForm.userRating,
      assets: [],

      // Album-specific fields
      runTime: albumForm.runTime,
      trackCount: albumForm.trackCount,
      artist: albumForm.artist
    };
  }
}
