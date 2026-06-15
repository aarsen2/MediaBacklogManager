import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReadShowDto } from '../../models/read/ReadShowDto';
import { CreateShowDto } from '../../models/create/CreateShowDto';

@Injectable({
  providedIn: 'root',
})
export class ShowApi {

  private baseUrl = 'https://localhost:7170/api/show';

  constructor(private http: HttpClient) { }

  getShows(): Observable<ReadShowDto[]> {
    return this.http.get<ReadShowDto[]>(this.baseUrl, { responseType: 'json' });
  }

  getShow(id: string): Observable<ReadShowDto> {
    return this.http.get<ReadShowDto>(`${this.baseUrl}/${id}`, { responseType: 'json' })
  }

  createShow(dto: CreateShowDto): Observable<string> {
    console.log("Creating Show...");
    console.log(dto);
    return this.http.post(this.baseUrl + '/create', dto, { responseType: 'text' })
  }
}
