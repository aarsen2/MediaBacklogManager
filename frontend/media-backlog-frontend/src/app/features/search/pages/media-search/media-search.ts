import { CommonModule } from '@angular/common';
import { Component, inject, NgModule, signal } from '@angular/core';
import { NgModel } from '@angular/forms';
import { SearchResultItemDto } from '../../models/SearchResultItemDto';
import { SearchResultsDto } from '../../models/SearchResultsDto';
import { SearchParameters } from '../../models/SearchParameters';
import { toSignal } from '@angular/core/rxjs-interop';
import { catchError, map, of, switchMap } from 'rxjs';
import { routes } from '../../../../app.routes';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MediaSearchService } from '../../Services/media-search-service';
import { MediaCard } from "../../../media/shared/components/media-card/media-card";

@Component({
  selector: 'app-media-search',
  imports: [CommonModule, MediaCard],
  templateUrl: './media-search.html',
  styleUrl: './media-search.css',
})
export class MediaSearch {
  private readonly route = inject(ActivatedRoute)
  private readonly router = inject(Router)
  private readonly searchService = inject(MediaSearchService)

  hasSearched: boolean = false;
  results = signal<SearchResultsDto>({ results: [] })
  errorMessage = signal<string | null>(null)


  ngOnInit() {
    const params = this.route.queryParamMap.pipe(
      map(params => ({
        genericSearch: params.get('q') ?? '',
        genreSearch: params.get('genre') ?? '',
        platformSearch: params.get('platform') ?? '',
        recommenderSearch: params.get('rec') ?? ''
      })),
      switchMap(searchParams =>
        this.searchService.search(searchParams).pipe(
          catchError(err => {
            console.error(err);
            this.errorMessage.set("Something Went Wrong");
            this.results.set({ results: [] });
            return of({ results: [] });
          })
        ))
    ).subscribe(results => {
      console.log(results)
      this.results.set(results);
      this.hasSearched = true;
    })



  };



  search(searchString: string) {
    this.router.navigate([], {
      queryParams: {
        q: searchString || null,
      }
    });
  }

}
