import { inject, Injectable } from '@angular/core';
import { SongApi } from './song-api';
import { Observable } from 'rxjs';
import { CreateSongDto } from '../../models/create/CreateSongDto';
import { ReadSongDto } from '../../models/read/ReadSongDto';

@Injectable({
  providedIn: 'root',
})
export class SongService {
  private readonly SongApi = inject(SongApi);

  createMedia(SongDto: CreateSongDto): Observable<string> {
    return this.SongApi.createSong(SongDto);
  }

  getSong(id: string): Observable<ReadSongDto> {
    return this.SongApi.getSong(id);
  }

  getAllSongs(): Observable<ReadSongDto[]> {
    return this.SongApi.getSongs();
  }
}
