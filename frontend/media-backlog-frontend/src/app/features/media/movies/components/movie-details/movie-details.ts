import { Component, Input } from '@angular/core';
import { ReadMovieDto } from '../../../models/read/ReadMovieDto';

@Component({
  selector: 'app-movie-details',
  imports: [],
  templateUrl: './movie-details.html',
  styleUrl: './movie-details.css',
})
export class MovieDetails {
  @Input() movie!: ReadMovieDto | null;

    formatRunTime(minutes: number): string {

    const h = Math.floor(minutes / 60);
    const m = Math.floor(minutes % 60)
    const s = Math.floor((minutes * 60) % 60)
    var returnString = ""
    if (h > 0) {
      returnString += `${h}h `
    }
    if (m > 0) {
      returnString += `${m}m `
    }
    if (s > 0) {
      returnString += `${s}s`
    }
    return returnString
  }
}
