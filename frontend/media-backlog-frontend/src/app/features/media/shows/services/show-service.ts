import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateShowDto } from '../../models/create/CreateShowDto';
import { ReadShowDto } from '../../models/read/ReadShowDto';
import { ShowApi } from './show-api';
import { ShowForm } from '../../models/forms/ShowForm';

@Injectable({
  providedIn: 'root',
})
export class ShowService {

  private readonly songApi = inject(ShowApi);

  createShow(showForm: ShowForm): Observable<ReadShowDto> {
    let showDto = this.mapCreateDto(showForm)
    return this.songApi.createShow(showDto);
  }

  getShow(id: string): Observable<ReadShowDto> {
    return this.songApi.getShow(id);
  }

  getAllShows(): Observable<ReadShowDto[]> {
    return this.songApi.getShows();
  }

  mapCreateDto(showForm: ShowForm): CreateShowDto {
    return {
      type: 'show',
      // MediaBase / CreateMediaBase fields
      title: showForm.title,
      description: showForm.description,
      releaseDate: showForm.releaseDate,
      genres: showForm.genres ?? [],
      generalRating: showForm.userRating,
      assets: [],

      // Show-specific fields
      episodeCount: showForm.episodeCount,
      seasonCount: showForm.seasonCount,
      contentRating: showForm.contentRating

    };
  }
}
