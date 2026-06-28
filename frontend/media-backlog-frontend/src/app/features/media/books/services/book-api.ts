import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReadBookDto } from '../../models/read/ReadBookDto';
import { Observable } from 'rxjs';
import { CreateBookDto } from '../../models/create/CreateBookDto';
import { environment } from '../../../../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class BookApi {
  private apiUrl = environment.apiUrl;
  private baseUrl = this.apiUrl + "/book";

  constructor(private http: HttpClient) { }

  getBooks(): Observable<ReadBookDto[]> {
    return this.http.get<ReadBookDto[]>(this.baseUrl, { responseType: 'json' });
  }

  getBook(id: string): Observable<ReadBookDto> {
    return this.http.get<ReadBookDto>(`${this.baseUrl}/${id}`, { responseType: 'json' })
  }

  createBook(dto: CreateBookDto): Observable<ReadBookDto> {
    console.log("Creating Book...");
    console.log(dto);
    return this.http.post<ReadBookDto>(this.baseUrl + '/create', dto)
  }
}
