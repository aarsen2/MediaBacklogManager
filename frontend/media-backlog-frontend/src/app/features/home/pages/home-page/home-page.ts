import { Component, effect, inject } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { RouterLink } from "@angular/router";
import { MovieService } from '../../../media/movies/services/media-service';
import { toSignal } from '@angular/core/rxjs-interop';
import { MediaCarousel } from "../../../media/shared/components/media-carousel/media-carousel";
import { ShowService } from '../../../media/shows/services/show-service';
import { AlbumSerivce } from '../../../media/albums/services/album-serivce';
import { BookService } from '../../../media/books/services/book-service';
import { GamesService } from '../../../media/games/services/games-service';
import { SongService } from '../../../media/songs/services/song-service';
import { DashbaordService } from '../../services/dashbaord-service';
import { DashboardDto } from './models/DashboardDto';
import { DashboardSectionDto } from './models/DashboardSectionDto';

@Component({
  selector: 'app-home-page',
  imports: [MediaCarousel],
  templateUrl: './home-page.html',
  styleUrl: './home-page.css',
})
export class HomePage {
  private readonly dashboardService = inject(DashbaordService)


  dashboard = toSignal(
  this.dashboardService.getDashboard(),
  {
    initialValue: {
      sections: []
    } as DashboardDto
  }
);



  constructor() {
    effect(() => {
      console.log('Dashboard:', this.dashboard());
    });
  }
}
