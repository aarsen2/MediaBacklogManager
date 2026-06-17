import { Component, Input } from '@angular/core';
import { ReadAlbumDto } from '../../../models/read/ReadAlbumDto';

@Component({
  selector: 'app-album-details',
  imports: [],
  templateUrl: './album-details.html',
  styleUrl: './album-details.css',
})
export class AlbumDetails {
  @Input() album!: ReadAlbumDto | null;


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
