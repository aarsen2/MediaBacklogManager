import { inject, Injectable } from '@angular/core';
import { SongApi } from './song-api';
import { Observable } from 'rxjs';
import { CreateSongDto } from '../../models/create/CreateSongDto';
import { ReadSongDto } from '../../models/read/ReadSongDto';
import { SongForm } from '../../models/forms/SongForm';

@Injectable({
  providedIn: 'root',
})
export class SongService {
  private readonly songApi = inject(SongApi);

  createSong(songForm: SongForm): Observable<ReadSongDto> {
    let songDto = this.mapCreateDto(songForm)
    return this.songApi.createSong(songDto);
  }

  getSong(id: string): Observable<ReadSongDto> {
    return this.songApi.getSong(id);
  }

  getAllSongs(): Observable<ReadSongDto[]> {
    return this.songApi.getSongs();
  }

  mapCreateDto(songForm: SongForm): CreateSongDto {
    return {
      type: 'song',
      // MediaBase / CreateMediaBase fields
      title: songForm.title,
      description: songForm.description,
      releaseDate: songForm.releaseDate,
      genres: songForm.genres ?? [],
      generalRating: songForm.userRating,
      assets: [],

      // Song-specific fields
      artist: songForm.artist,
      runTime: songForm.runTime

    };
  }

}
