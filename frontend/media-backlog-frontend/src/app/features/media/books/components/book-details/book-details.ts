import { Component, Input } from '@angular/core';
import { ReadBookDto } from '../../../models/read/ReadBookDto';

@Component({
  selector: 'app-book-details',
  imports: [],
  templateUrl: './book-details.html',
  styleUrl: './book-details.css',
})
export class BookDetails {
  @Input() book!: ReadBookDto | null;
}
