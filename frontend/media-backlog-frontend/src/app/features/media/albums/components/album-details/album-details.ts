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

}
