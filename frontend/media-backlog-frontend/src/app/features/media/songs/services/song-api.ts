import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReadSongDto } from '../../models/read/ReadSongDto';
import { CreateSongDto } from '../../models/create/CreateSongDto';

@Injectable({
  providedIn: 'root',
})
export class SongApi {
   private baseUrl = 'https://localhost:7170/api/song';

  constructor(private http: HttpClient) {}

  getSongs(): Observable<ReadSongDto[]> {
    return this.http.get<ReadSongDto[]>(this.baseUrl, {responseType: 'json'});
  }

  getSong(id: string): Observable<ReadSongDto> {
    return this.http.get<ReadSongDto>(`${this.baseUrl}/${id}`, {responseType: 'json'})
  }

  createSong(dto: CreateSongDto) : Observable<ReadSongDto> {
    console.log("Creating Song...");
    console.log(dto);
    return this.http.post<ReadSongDto>(this.baseUrl + '/create', dto)
  } 
}
