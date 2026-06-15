import { Component, Input } from '@angular/core';
import { ReadMovieDto } from '../../../models/read/ReadMovieDto';
import { ReadShowDto } from '../../../models/read/ReadShowDto';

@Component({
  selector: 'app-show-details',
  imports: [],
  templateUrl: './show-details.html',
  styleUrl: './show-details.css',
})
export class ShowDetails {
  @Input() show!: ReadShowDto | null;
}
