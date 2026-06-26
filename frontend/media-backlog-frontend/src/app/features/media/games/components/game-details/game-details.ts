import { Component, inject, Input } from '@angular/core';
import { ReadGameDto } from '../../../models/read/ReadGameDto';
import { Router } from '@angular/router';

@Component({
  selector: 'app-game-details',
  imports: [],
  templateUrl: './game-details.html',
  styleUrl: './game-details.css',
})
export class GameDetails {
  @Input() game!: ReadGameDto | null;
  private router = inject(Router)


  searchPlatform(platformName: string) {
    this.router.navigate(['/search'], {
      queryParams: {
        platform: platformName || null,
      }
    })
  }
}
