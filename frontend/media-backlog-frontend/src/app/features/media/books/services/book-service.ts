import { inject, Injectable } from '@angular/core';
import { BookApi } from './book-api';
import { Observable } from 'rxjs';
import { CreateBookDto } from '../../models/create/CreateBookDto';
import { ReadBookDto } from '../../models/read/ReadBookDto';
import { BookForm } from '../../models/forms/BookForm';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  private readonly BookApi = inject(BookApi);

  createBook(bookForm: BookForm): Observable<ReadBookDto> {
    let bookDto = this.mapCreateDto(bookForm)
    return this.BookApi.createBook(bookDto);
  }

  getBook(id: string): Observable<ReadBookDto> {
    return this.BookApi.getBook(id);
  }

  getAllBooks(): Observable<ReadBookDto[]> {
    return this.BookApi.getBooks();
  }

  mapCreateDto(bookFrom: BookForm): CreateBookDto {
    return {
      type: 'book',
      // MediaBase / CreateMediaBase fields
      title: bookFrom.title,
      description: bookFrom.description,
      releaseDate: bookFrom.releaseDate,
      genres: bookFrom.genres ?? [],
      generalRating: bookFrom.userRating,
      assets: [],

      // Book-specific fields
      author: bookFrom.author,
      language: bookFrom.language,
      pageCount: bookFrom.pageCount
    };
  }
}
