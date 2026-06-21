import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReadAlbumDto } from '../../models/read/ReadAlbumDto';
import { Observable } from 'rxjs';
import { CreateAlbumDto } from '../../models/create/CreateAlbumDto';

@Injectable({
  providedIn: 'root',
})
export class AlbumApi {
     private baseUrl = 'https://localhost:7170/api/album';

  constructor(private http: HttpClient) {}

  getAlbums(): Observable<ReadAlbumDto[]> {
    return this.http.get<ReadAlbumDto[]>(this.baseUrl, {responseType: 'json'});
  }

  getAlbum(id: string): Observable<ReadAlbumDto> {
    return this.http.get<ReadAlbumDto>(`${this.baseUrl}/${id}`, {responseType: 'json'})
  }

  createAlbum(dto: CreateAlbumDto) : Observable<ReadAlbumDto> {
    console.log("Creating Album...");
    console.log(dto);
    return this.http.post<ReadAlbumDto>(this.baseUrl + '/create', dto)
  } 
}
