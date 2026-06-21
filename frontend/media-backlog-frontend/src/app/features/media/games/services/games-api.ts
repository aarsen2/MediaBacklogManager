import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReadGameDto } from '../../models/read/ReadGameDto';
import { HttpClient } from '@angular/common/http';
import { CreateGameDto } from '../../models/create/CreateGameDto';

@Injectable({
  providedIn: 'root',
})
export class GamesApi {
  private baseUrl = 'https://localhost:7170/api/game';

  constructor(private http: HttpClient) { }

  getGames(): Observable<ReadGameDto[]> {
    return this.http.get<ReadGameDto[]>(this.baseUrl, { responseType: 'json' });
  }

  getGame(id: string): Observable<ReadGameDto> {
    return this.http.get<ReadGameDto>(`${this.baseUrl}/${id}`, { responseType: 'json' })
  }

  createGame(dto: CreateGameDto): Observable<ReadGameDto> {
    console.log("Creating Game...");
    console.log(dto);
    return this.http.post<ReadGameDto>(this.baseUrl + '/create', dto)
  }
}
