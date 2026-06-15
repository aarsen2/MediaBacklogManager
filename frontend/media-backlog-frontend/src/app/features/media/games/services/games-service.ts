import { inject, Injectable } from '@angular/core';
import { GamesApi } from './games-api';
import { Observable } from 'rxjs';
import { CreateGameDto } from '../../models/create/CreateGameDto';
import { ReadGameDto } from '../../models/read/ReadGameDto';

@Injectable({
  providedIn: 'root',
})
export class GamesService {
  private readonly GameApi = inject(GamesApi);

  createMedia(GameDto: CreateGameDto): Observable<string> {
    return this.GameApi.createGame(GameDto);
  }

  getGame(id: string): Observable<ReadGameDto> {
    return this.GameApi.getGame(id);
  }

  getAllGames(): Observable<ReadGameDto[]> {
    return this.GameApi.getGames();
  }
}
