import { inject, Injectable } from '@angular/core';
import { BookApi } from './book-api';
import { Observable } from 'rxjs';
import { CreateBookDto } from '../../models/create/CreateBookDto';
import { ReadBookDto } from '../../models/read/ReadBookDto';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  private readonly BookApi = inject(BookApi);

  createMedia(BookDto: CreateBookDto): Observable<string> {
    return this.BookApi.createBook(BookDto);
  }

  getBook(id: string): Observable<ReadBookDto> {
    return this.BookApi.getBook(id);
  }

  getAllBooks(): Observable<ReadBookDto[]> {
    return this.BookApi.getBooks();
  }
}
