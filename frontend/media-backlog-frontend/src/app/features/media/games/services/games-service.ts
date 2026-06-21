import { inject, Injectable } from '@angular/core';
import { GamesApi } from './games-api';
import { Observable } from 'rxjs';
import { CreateGameDto } from '../../models/create/CreateGameDto';
import { ReadGameDto } from '../../models/read/ReadGameDto';
import { GameForm } from '../../models/forms/GameForm';

@Injectable({
  providedIn: 'root',
})
export class GamesService {
  private readonly gameApi = inject(GamesApi);

  createGame(gameForm: GameForm): Observable<ReadGameDto> {
    let gameDto = this.mapCreateDto(gameForm);
    console.log(gameDto)
    return this.gameApi.createGame(gameDto);
  }

  getGame(id: string): Observable<ReadGameDto> {
    return this.gameApi.getGame(id);
  }

  getAllGames(): Observable<ReadGameDto[]> {
    return this.gameApi.getGames();
  }

  mapCreateDto(gameForm: GameForm): CreateGameDto {
    return {
      // MediaBase / CreateMediaBase fields
      title: gameForm.title,
      description: gameForm.description,
      releaseDate: gameForm.releaseDate,
      genres: gameForm.genres ?? [],
      generalRating: gameForm.userRating,
      assets: [],

      // Game-specific fields
      studio: gameForm.studio,
      platforms: gameForm.platforms,
      contentRating: gameForm.contentRating
    };
  }
}
