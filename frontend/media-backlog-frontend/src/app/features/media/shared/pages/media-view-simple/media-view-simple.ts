import { Component, computed, effect, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MediaBacklogService } from '../../../../backlog/services/media-backlog-service';
import { UserMediaService } from '../../services/user-media-service';
import { switchMap } from 'rxjs';
import { ReadBacklogItemDto } from '../../../models/read/ReadBacklogItemDto';
import { SongDetails } from "../../../songs/components/song-details/song-details";
import { GameDetails } from "../../../games/components/game-details/game-details";
import { BookDetails } from "../../../books/components/book-details/book-details";
import { AlbumDetails } from "../../../albums/components/album-details/album-details";
import { ShowDetails } from "../../../shows/components/show-details/show-details";
import { MovieDetails } from "../../../movies/components/movie-details/movie-details";
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-media-view-simple',
  imports: [SongDetails, GameDetails, BookDetails, AlbumDetails, ShowDetails, MovieDetails, DatePipe],
  templateUrl: './media-view-simple.html',
  styleUrl: './media-view-simple.css',
})
export class MediaViewSimple {

  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router)
  private readonly backlogService = inject(MediaBacklogService);
  private readonly userMediaService = inject(UserMediaService)



  ngOnInit() {
    this.route.paramMap.pipe(
      switchMap(params => {
        const id = params.get('id')!;
        return this.backlogService.getBacklogItem(id)
      })).subscribe(item => {
        this.backlogItem.set(item);
      }),
      { initialValue: null }
  }

  constructor() {

    effect(() => {
      const item = this.backlogItem();
      if (item) {
        console.log('Media:', item);
      }
    });
  }


  backlogItem = signal<ReadBacklogItemDto | null>(null);


  releaseDate = computed(() => {
    const m = this.backlogItem()
    return m ? new Date(m.media.releaseDate) : null
  })

  prioritized = computed(() => {
    return this.backlogItem()?.prioritized
  })

  deleteItem() {
    if (confirm("Are you sure you want to delete this item?")) {
      console.log("Deleting Item")
      console.log(this.backlogItem())
      const id = this.backlogItem()?.media.id;
      if (id) {
        this.userMediaService.deleteItem(id).subscribe({
          next: (res) => {
            console.log(res)
            this.router.navigate(['/home']);
          },
          error: (err) => {
            console.log(err)
            alert("An Error occurred while deleting Media ID: " + id)
          }
        });
      }
      else { alert("Unable to delite Media with ID 0") }
    }
  }

  editItem() {
    this.router.navigate(['/media/edit', this.backlogItem()?.media.type, this.backlogItem()?.media.id])
  }


  formatStatus(status: string) {
    if (status == "InProgress")
      return "In Progress"

    return status
  }

  toggleFavorite() {
    const id = this.backlogItem()?.mediaId as number;
    if (id == null || id < 1) {
      return;
    }
    this.backlogService.toggleFavorite(id).subscribe({
      next: (res) => {
        this.backlogItem.set(res);
      },
      error: () => {
        console.error("Unable to update the priority of this item")
      }
    })
  }

  searchRecommender(recommenderName: string) {
    this.router.navigate(['/search'], {
      queryParams: {
        rec: recommenderName || null,
      }
    })
  }

  searchGenre(genreName: string) {
    this.router.navigate(['/search'], {
      queryParams: {
        genre: genreName || null,
      }
    })
  }
}
