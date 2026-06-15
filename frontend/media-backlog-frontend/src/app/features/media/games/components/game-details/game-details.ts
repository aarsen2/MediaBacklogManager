import { Component, Input } from '@angular/core';
import { ReadGameDto } from '../../../models/read/ReadGameDto';

@Component({
  selector: 'app-game-details',
  imports: [],
  templateUrl: './game-details.html',
  styleUrl: './game-details.css',
})
export class GameDetails {
  @Input() game!: ReadGameDto | null;

}
