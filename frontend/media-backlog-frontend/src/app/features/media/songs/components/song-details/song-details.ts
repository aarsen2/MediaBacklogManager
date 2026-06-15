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

}
