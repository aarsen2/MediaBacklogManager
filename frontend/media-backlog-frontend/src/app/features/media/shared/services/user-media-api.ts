import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateUserMediaDto } from '../../models/create/CreateUserMediaDto';
import { HttpClient } from '@angular/common/http';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';
import { ReadBacklogItemDto } from '../../models/read/ReadBacklogItemDto';

@Injectable({
  providedIn: 'root',
})
export class UserMediaApi {
  private readonly http = inject(HttpClient)

  private baseUrl = 'https://localhost:7170/api/user-media';

  addItem(dto: CreateUserMediaDto): Observable<object> {
    console.log("Adding Item to backlog...");
    console.log(dto);
    return this.http.post(this.baseUrl + '/add', dto, { responseType: 'text' as 'json' })
  }


  deleteItem(id: number): Observable<object> {
    console.log("Deleteing item from backlog");
    return this.http.delete(`${this.baseUrl}/${id}`);
  }

}
