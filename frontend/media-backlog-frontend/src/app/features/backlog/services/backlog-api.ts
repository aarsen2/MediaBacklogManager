import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReadBacklogItemDto } from '../../media/models/read/ReadBacklogItemDto';
import { Form } from '@angular/forms';
import { BaseForm } from '../../media/models/forms/BaseForm';
import { UpdateBacklogItemDto } from '../../media/models/update/UpdateBacklogItemDto';

@Injectable({
  providedIn: 'root',
})
export class BacklogApi {

  private http = inject(HttpClient)

  private baseUrl = 'https://localhost:7170/api/backlog';


  public getBacklogItem(id: string): Observable<ReadBacklogItemDto> {
    console.log("Getting Dashboard")
    return this.http.get<ReadBacklogItemDto>(`${this.baseUrl}/item/${id}`);
  }

  public togglePriority(id: number): Observable<ReadBacklogItemDto> {
    console.log("Toggling Priority on Media ID " + id)
    return this.http.post<ReadBacklogItemDto>(`${this.baseUrl}/item/${id}/priority/toggle`, null)
  }

  public updateBacklogItem(userMedia: UpdateBacklogItemDto, mediaId: string): Observable<string> {
    console.log("Updating Media Item: " + userMedia.media.title)
    console.log(userMedia)
    return this.http.post(`${this.baseUrl}/item/${mediaId}/update`, userMedia, { responseType: 'text' })
  }


  public getGenres(): Observable<string[]> {
    console.log("Getting all genres")
    return this.http.get<string[]>(`${this.baseUrl}/genres`)
  }

  public getPlatforms(): Observable<string[]> {
    console.log("Getting all genres")
    return this.http.get<string[]>(`${this.baseUrl}/platforms`)

  }

  public exportBacklog(): Observable<Blob> {
    return this.http.get(`${this.baseUrl}/export`, { responseType: 'blob' })
  }
}
