import { Component, inject, Input } from '@angular/core';
import { ReadMediaDto } from '../../../models/read/ReadMediaDto';
import { Router } from '@angular/router';
import { DashboardItemDto } from '../../../../home/models/DashboardItemDto';
import { MediaCard } from "../media-card/media-card";

@Component({
  selector: 'app-media-carousel',
  imports: [MediaCard],
  templateUrl: './media-carousel.html',
  styleUrl: './media-carousel.css',
})
export class MediaCarousel {
  @Input()  items: DashboardItemDto[] | null = [];

}
