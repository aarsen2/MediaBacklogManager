import { Component, Input, } from '@angular/core';
import { ReadSongDto } from '../../../models/read/ReadSongDto';

@Component({
  selector: 'app-song-details',
  imports: [],
  templateUrl: './song-details.html',
  styleUrl: './song-details.css',
})
export class SongDetails {
  @Input() song!: ReadSongDto | null;
  
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
