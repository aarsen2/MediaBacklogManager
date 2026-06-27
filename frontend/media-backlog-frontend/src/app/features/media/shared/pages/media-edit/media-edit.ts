import { Component, HostListener, inject, signal } from '@angular/core';
import { MediaBacklogService } from '../../../../backlog/services/media-backlog-service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseForm } from '../../../models/forms/BaseForm';
import { TitleCasePipe } from '@angular/common';
import { MovieCreationForm } from '../../../movies/components/movie-creation-form/movie-creation-form';
import { ShowCreationForm } from '../../../shows/components/show-creation-form/show-creation-form';
import { AlbumCreationForm } from '../../../albums/components/album-creation-form/album-creation-form';
import { BookCreationForm } from '../../../books/components/book-creation-form/book-creation-form';
import { GameCreationForm } from '../../../games/components/game-creation-form/game-creation-form';
import { SongCreationForm } from '../../../songs/components/song-creation-form/song-creation-form';
import { ReadBacklogItemDto } from '../../../models/read/ReadBacklogItemDto';
import { switchMap } from 'rxjs';
import { ShowForm } from '../../../models/forms/ShowForm';
import { ReadShowDto } from '../../../models/read/ReadShowDto';
import { ReadMovieDto } from '../../../models/read/ReadMovieDto';
import { ReadSongDto } from '../../../models/read/ReadSongDto';
import { ReadGameDto } from '../../../models/read/ReadGameDto';
import { ReadBookDto } from '../../../models/read/ReadBookDto';
import { ReadAlbumDto } from '../../../models/read/ReadAlbumDto';
import { toSignal } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-media-edit',
  imports: [TitleCasePipe,
    ReactiveFormsModule,
    MovieCreationForm,
    ShowCreationForm,
    AlbumCreationForm,
    BookCreationForm,
    GameCreationForm,
    SongCreationForm],
  templateUrl: './media-edit.html',
  styleUrl: './media-edit.css',
})
export class MediaEdit {

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
  private route = inject(ActivatedRoute)

  //inital variable setting
  successMessage = signal<string | null>(null)
  errorMessage = signal<string | null>(null)
  isSubmitting: boolean = false;
  backlogItem = signal<ReadBacklogItemDto | null>(null);
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


  ngOnInit() {
    console.log("Initializing edit form")
    this.route.paramMap.pipe(
      switchMap(params => {
        const id = params.get('id')!;
        return this.backlogService.getBacklogItem(id)
      })).subscribe(item => {
        this.backlogItem.set(item);
        this.prefillForm();
      }),
      { initialValue: null }
  }

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

  prefillForm() {
    console.log("filling the form");

    //Base Form prefill
    this.form.patchValue({ title: this.backlogItem()?.media.title })
    this.form.patchValue({ mediaType: this.backlogItem()?.media.type })
    this.form.patchValue({ releaseDate: this.formatDate() })
    this.form.patchValue({ description: this.backlogItem()?.media.description })
    this.form.patchValue({ genres: this.getGenres() })
    this.form.patchValue({ recommenders: this.getRecommenders() })
    this.form.patchValue({ prioritized: this.backlogItem()?.prioritized ? "yes" : "no" })
    this.form.patchValue({ userRating: this.backlogItem()?.userRating })
    this.form.patchValue({ notes: this.backlogItem()?.notes })
  }

  getGenres(): string[] {
    return this.backlogItem()?.media.genres?.map(g => g.name) ?? [];
  }

  getRecommenders(): string[] {
    return this.backlogItem()?.recommenders ?? [];
  }


  formatDate() {
    const rawDate = this.backlogItem()?.media.releaseDate;
    return rawDate ? new Date(rawDate).toISOString().split('T')[0] : null
  }

  addGenre(genre?: string | null) {

    const value = !genre ? this.form.value.genreInput?.trim() : genre;
    console.log(value)

    if (!value) return;

    const genres = this.form.value.genres as string[];

    // prevent duplicates (case-insensitive)
    const exists = genres.some(g => g.toLowerCase() === value.toLowerCase());
    if (exists) {
      this.form.patchValue({
        genreInput: ''
      });
      return;
    }

    genres.push(value);
    this.form.patchValue({ genres });

    this.form.patchValue({ genreInput: '' });
    this.filteredGenres.set([]);
  }

  removeGenre(genre: string) {
    const genres = this.form.value.genres as string[];
    this.form.patchValue({
      genres: genres.filter(g => g !== genre)
    });
  }

  // optional: allow comma to add
  handleKeydown(event: KeyboardEvent) {
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

  selectFromList(genre: string) {
    this.addGenre(genre);
    this.filteredGenres.set([]);
  }


  addRecommender(recommender?: string | null) {

    const value = !recommender ? this.form.value.recommenderInput?.trim() : recommender;
    console.log(value)

    if (!value) return;

    const recommenders = this.form.value.recommenders as string[];

    // prevent duplicates (case-insensitive)
    const exists = recommenders.some(g => g.toLowerCase() === value.toLowerCase());
    if (exists) {
      this.form.patchValue({
        recommenderInput: ''
      });
      return;
    }

    recommenders.push(value);
    this.form.patchValue({ recommenders: recommenders });

    this.form.patchValue({ recommenderInput: '' });

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
    console.log(this.filteredRecommenders())
  }

  selectRecommenderFromList(recommender: string) {
    this.addRecommender(recommender);
    this.filteredGenres.set([]);
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

    console.log("Submitting Full Form: ")
    console.log(fullForm)


    this.route.paramMap.pipe(
      switchMap(params => {
        const id = params.get('id')!;
        return this.backlogService.UpdateItem(fullForm, id)
      }))
      .subscribe({
        next: (res) => {
          console.log(res)
          console.log("Success")
          this.successMessage.set("Media Updated Successfully")
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


  get movie(): ReadMovieDto | null {
    const item = this.backlogItem();
    if (!item || item.media.type !== 'movie') return null;
    return item.media as ReadMovieDto
  }
  get show(): ReadShowDto | null {
    const item = this.backlogItem();
    if (!item || item.media.type !== 'show') return null;
    return item.media as ReadShowDto
  }
  get album(): ReadAlbumDto | null {
    const item = this.backlogItem();
    if (!item || item.media.type !== 'album') return null;
    return item.media as ReadAlbumDto
  }
  get book(): ReadBookDto | null {
    const item = this.backlogItem();
    if (!item || item.media.type !== 'book') return null;
    return item.media as ReadBookDto
  }
  get game(): ReadGameDto | null {
    const item = this.backlogItem();
    if (!item || item.media.type !== 'game') return null;
    return item.media as ReadGameDto
  }
  get song(): ReadSongDto | null {
    const item = this.backlogItem();
    if (!item || item.media.type !== 'song') return null;
    return item.media as ReadSongDto
  }
}
