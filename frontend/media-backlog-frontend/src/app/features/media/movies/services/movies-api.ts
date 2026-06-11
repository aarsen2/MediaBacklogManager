import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReadMovieDto } from '../../models/read/ReadMovieDto';
import { CreateMovieDto } from '../../models/create/CreateMovieDto';

@Injectable({
  providedIn: 'root',
})
export class MoviesApi {
  private baseUrl = 'https://localhost:7170/api/Movie';

  constructor(private http: HttpClient) {}

  getMovies(): Observable<ReadMovieDto[]> {
    return this.http.get<ReadMovieDto[]>(this.baseUrl, {responseType: 'json'});
  }

  getMovie(id: string): Observable<ReadMovieDto> {
    return this.http.get<ReadMovieDto>(`${this.baseUrl}/${id}`, {responseType: 'json'})
  }

  createMovie(dto: CreateMovieDto) : Observable<string> {
    console.log("Creating Movie...");
    console.log(dto);
    return this.http.post(this.baseUrl + '/Create', dto, {responseType: 'text'})
    
  } 
}
