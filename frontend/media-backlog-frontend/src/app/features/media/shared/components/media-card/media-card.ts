import { Component, inject, Input } from '@angular/core';
import { DashboardItemDto } from '../../../../home/models/DashboardItemDto';
import { Router } from '@angular/router';
import { SearchResultItemDto } from '../../../../search/models/SearchResultItemDto';

@Component({
  selector: 'app-media-card',
  imports: [],
  templateUrl: './media-card.html',
  styleUrl: './media-card.css',
})
export class MediaCard {
  @Input() item: DashboardItemDto | SearchResultItemDto | null = null;
  private router = inject(Router);


  viewItem(item: DashboardItemDto) {
    console.log("clicked");
    console.log(item);
    this.router.navigate(['/media/view', item.mediaType, item.id])
  }

  getMediaIcon(): string {

    switch (this.item?.mediaType.toLowerCase()) {
      case 'album':
        return "album"

      case 'book':
        return "menu_book"

      case 'game':
        return "stadia_controller"

      case 'movie':
        return "movie"

      case 'show':
        return "tv"

      case 'song':
        return "music_note"

      default:
        return "";

    }



  }
}
