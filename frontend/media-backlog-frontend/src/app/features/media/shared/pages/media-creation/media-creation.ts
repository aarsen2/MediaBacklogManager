import { ChangeDetectorRef, Component, HostListener, inject, signal } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MovieCreationForm } from '../../../movies/components/movie-creation-form/movie-creation-form';
import { ShowCreationForm } from '../../../shows/components/show-creation-form/show-creation-form';
import { AlbumCreationForm } from '../../../albums/components/album-creation-form/album-creation-form';
import { BookCreationForm } from '../../../books/components/book-creation-form/book-creation-form';
import { GameCreationForm } from '../../../games/components/game-creation-form/game-creation-form';
import { SongCreationForm } from '../../../songs/components/song-creation-form/song-creation-form';
import { BaseForm } from '../../../models/forms/BaseForm';
import { MediaBacklogService } from '../../../../backlog/services/media-backlog-service';
import { TitleCasePipe } from '@angular/common';
import { Router } from '@angular/router';
import { toSignal } from '@angular/core/rxjs-interop';
import { MediaSearchService } from '../../../../search/Services/media-search-service';
import { MediaSearchApi } from '../../../../search/Services/media-search-api';
import { CreationSearchQuery } from '../../../../search/models/CreationSearchQuery';
import { ReadMediaDto } from '../../../models/read/ReadMediaDto';
import { ReadMovieDto } from '../../../models/read/ReadMovieDto';
import { ReadShowDto } from '../../../models/read/ReadShowDto';
import { ReadGameDto } from '../../../models/read/ReadGameDto';
import { MetadataSearch } from "../../components/metadata-search/metadata-search";


@Component({
  selector: 'app-media-creation',
  standalone: true,
  imports: [TitleCasePipe,
    ReactiveFormsModule,
    MovieCreationForm,
    ShowCreationForm,
    AlbumCreationForm,
    BookCreationForm,
    GameCreationForm,
    SongCreationForm, MetadataSearch],
  templateUrl: './media-creation.html',
  styleUrl: './media-creation.css',
})
export class MediaCreation {

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent) {
    const target = event.target as HTMLElement;
    if (!target.closest('.autocomplete-wrapper')) {
      this.filteredGenres.set([]);
      this.filteredRecommenders.set([]);
    }
  }

  //injection
  private backlogService = inject(MediaBacklogService)
  private formBuilder = inject(FormBuilder);
  private router = inject(Router)
  private mediaSearchService = inject(MediaSearchService);
  private cdr = inject(ChangeDetectorRef);


  movie = signal<ReadMovieDto | null>(null);
  show = signal<ReadShowDto | null>(null);
  game = signal<ReadGameDto | null>(null);



  //inital variable setting
  successMessage = signal<string | null>(null)
  errorMessage = signal<string | null>(null)
  isSubmitting: boolean = false;
  filteredGenres = signal<string[]>([]);
  possibleGenres = toSignal(
    this.backlogService.getGenres(),
    { initialValue: [] as string[] }
  )
  filteredRecommenders = signal<string[]>([]);
  possibleRecommenders = toSignal(
    this.backlogService.getRecommenders(),
    { initialValue: [] as string[] }
  )

  form = this.formBuilder.group({
    title: ['', [Validators.required, Validators.maxLength(200)]],
    mediaType: ["", [Validators.required]],
    releaseDate: [new Date().toISOString().split('T')[0]],
    description: ['', [Validators.maxLength(1000)]],
    genreInput: [''],
    genres: [[] as string[]],
    recommenderInput: [''],
    recommenders: [[] as string[]],
    status: ["Backlog"],
    prioritized: ["no"],
    userRating: [0],
    notes: [''],
  });


  isSearchOpen = signal(false);
  searchQuery = signal<CreationSearchQuery>({ title: '', mediaType: 'movie' });



  addGenre(genre?: string | null) {
    const value = !genre ? this.form.value.genreInput?.trim() : genre;
    if (!value) return;

    const current = this.form.value.genres as string[];

    const exists = current.some(g => g.toLowerCase() === value.toLowerCase());
    if (exists) {
      this.form.patchValue({ genreInput: '' });
      return;
    }

    this.form.patchValue({
      genres: [...current, value], // ✅ NEW ARRAY
      genreInput: ''
    });

    this.filteredGenres.set([]);
  }

  removeGenre(genre: string) {
    const genres = this.form.value.genres as string[];
    this.form.patchValue({
      genres: genres.filter(g => g !== genre)
    });
  }


  handleGenreKeydown(event: KeyboardEvent) {
    if (event.key === ',' || event.key === 'Enter') {
      event.preventDefault();
      this.addGenre();
    }
  }


  onGenreInput() {
    const value = this.form.value.genreInput?.toLowerCase() ?? "";

    if (!value) {
      this.filteredGenres.set([]);
      return;
    }
    const selected = this.form.value.genres as string[];

    this.filteredGenres.set(this.possibleGenres().filter(g =>
      g.toLowerCase().includes(value) && !selected.some(s => s.toLowerCase() === g.toLowerCase())
    ).sort((a, b) => a.localeCompare(b)));
  }

  selectGenreFromList(genre: string) {
    this.addGenre(genre);
  }


  addRecommender(recommender?: string | null) {
    const value = !recommender ? this.form.value.recommenderInput?.trim() : recommender;
    if (!value) return;

    const current = this.form.value.recommenders as string[];

    const exists = current.some(g => g.toLowerCase() === value.toLowerCase());
    if (exists) {
      this.form.patchValue({ recommenderInput: '' });
      return;
    }

    this.form.patchValue({
      recommenders: [...current, value], // ✅ FIXED
      recommenderInput: ''
    });

    this.filteredRecommenders.set([]);
  }

  removeRecommender(recommender: string) {
    const recommenders = this.form.value.recommenders as string[];
    this.form.patchValue({
      recommenders: recommenders.filter(g => g !== recommender)
    });
  }
  handleRecommenderKeydown(event: KeyboardEvent) {
    if (event.key === ',' || event.key === 'Enter') {
      event.preventDefault();
      this.addRecommender();
    }
  }

  onRecommenderInput() {
    const value = this.form.value.recommenderInput?.toLowerCase() ?? "";

    if (!value) {
      this.filteredRecommenders.set([]);
      return;
    }
    const selected = this.form.value.recommenders as string[];

    this.filteredRecommenders.set(this.possibleRecommenders().filter(g =>
      g.toLowerCase().includes(value) && !selected.some(s => s.toLowerCase() === g.toLowerCase())
    ).sort((a, b) => a.localeCompare(b)));
  }

  selectRecommenderFromList(recommender: string) {
    this.addRecommender(recommender);
  }





  private mapBase(formValue: any): BaseForm {
    return {
      title: formValue.title,
      mediaType: formValue.mediaType,
      releaseDate: formValue.releaseDate,
      description: formValue.description,
      genres: formValue.genres ?? [],
      recommenders: formValue.recommenders ?? [],
      status: formValue.status,
      prioritized: formValue.prioritized,
      userRating: formValue.userRating ?? 0,
      notes: formValue.notes ?? ''
    };
  }



  submit() {
    //Checks the form's validitiy and shows any errors that exist in the form
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      console.error("form is invalid")
      return;
    }
    this.isSubmitting = true;



    const mediaType = this.form.get('mediaType')?.value ?? 'unknown';
    const formValue = this.form.getRawValue() as any;

    const subForm = formValue[mediaType];


    if (!subForm) {
      console.error('No subform found for type:', mediaType);
      this.isSubmitting = false;
      return;
    }

    const fullForm = {
      ...this.mapBase(formValue),
      ...subForm
    }

    console.log("Submitting Full Form: " + fullForm)

    this.backlogService.CreateAndAddItem(fullForm).subscribe({
      next: (res) => {
        console.log(res)
        console.log("Success")
        this.successMessage.set("Media Created and Added Successfully")
        this.errorMessage.set('')
        this.isSubmitting = false;
        this.router.navigate(['/home'])
      },
      error: (err) => {
        console.error(err)
        this.successMessage.set("")
        this.errorMessage.set("An Error Occurred")
        this.isSubmitting = false;
      }
    })
  }



  search() {
    const title = this.form.get('title')?.value;
    const mediaType = this.form.get('mediaType')?.value as MediaType;

    if (!title || !mediaType) {
      this.form.get('title')?.markAsTouched();
      this.form.get('mediaType')?.markAsTouched();
      return;
    }

    this.searchQuery.set({
      title,
      mediaType
    });

    this.isSearchOpen.set(true);
  }

  onMetadataSelected(item: ReadMediaDto) {
    this.fillInfo(item);
    this.isSearchOpen.set(false);
  }

  closeSearch() {
    this.isSearchOpen.set(false);
  }



  fillInfo(media: ReadMediaDto) {
    const genres = (media.genres ?? []).map(g => g.name);

    this.form.patchValue({ title: media.title });
    this.form.patchValue({ releaseDate: this.formatDate(media.releaseDate) });
    this.form.patchValue({ description: media.description });
    this.form.patchValue({ genres: genres })


    console.log("Setting Movie")
    console.log(media)
    switch (media.mediaType) {
      case 'movie': {
        this.movie.set(media as ReadMovieDto);
        break;
      }
      case 'show': {
        this.show.set(media as ReadShowDto);
        break;
      }
      case 'game': {
        this.game.set(media as ReadGameDto);
        break;
      }
    }
  }


  formatDate(rawDate: string) {
    return rawDate ? new Date(rawDate).toISOString().split('T')[0] : null
  }
}
