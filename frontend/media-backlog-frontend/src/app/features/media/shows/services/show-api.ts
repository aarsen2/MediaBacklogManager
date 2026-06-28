import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReadShowDto } from '../../models/read/ReadShowDto';
import { CreateShowDto } from '../../models/create/CreateShowDto';
import { environment } from '../../../../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class ShowApi {
  private readonly http = inject(HttpClient)
  private apiUrl = environment.apiUrl;
  private baseUrl = this.apiUrl + "/show";


  getShows(): Observable<ReadShowDto[]> {
    return this.http.get<ReadShowDto[]>(this.baseUrl, { responseType: 'json' });
  }

  getShow(id: string): Observable<ReadShowDto> {
    return this.http.get<ReadShowDto>(`${this.baseUrl}/${id}`, { responseType: 'json' })
  }

  createShow(dto: CreateShowDto): Observable<ReadShowDto> {
    console.log("Creating Show...");
    console.log(dto);
    return this.http.post<ReadShowDto>(this.baseUrl + '/create', dto)
  }
}
