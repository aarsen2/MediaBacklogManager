import { Component, inject, Input } from '@angular/core';
import { ReadMediaDto } from '../../../models/read/ReadMediaDto';
import { Router } from '@angular/router';
import { DashboardItemDto } from '../../../../home/pages/home-page/models/DashboardItemDto';

@Component({
  selector: 'app-media-carousel',
  imports: [],
  templateUrl: './media-carousel.html',
  styleUrl: './media-carousel.css',
})
export class MediaCarousel {
  @Input()  items: DashboardItemDto[] | null = [];
  private router = inject(Router);


  viewItem(item: DashboardItemDto) {
    console.log("clicked");
    console.log(item);
    this.router.navigate(['/media/view', item.mediaType, item.id])
  }

}
