import { Component, HostListener, inject, signal } from '@angular/core';
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
    SongCreationForm],
  templateUrl: './media-creation.html',
  styleUrl: './media-creation.css',
})
export class MediaCreation {

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent) {
    const target = event.target as HTMLElement;
    if (!target.closest('.genre-wrapper')) {
      this.filteredGenres = [];
    }
  }

  //injection
  private backlogService = inject(MediaBacklogService)
  private formBuilder = inject(FormBuilder);
  private router = inject(Router)

  //inital variable setting
  successMessage = signal<string | null>(null)
  errorMessage = signal<string | null>(null)
  isSubmitting: boolean = false;
  filteredGenres: string[] = [];
  possibleGenres = toSignal(
    this.backlogService.getGenres(),
    { initialValue: [] as string[] }
  )
  filteredRecommenders: string[] = [];
  possibleRecommenders = toSignal(
    this.backlogService.getGenres(),
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
      this.filteredGenres = [];
      return;
    }
    const selected = this.form.value.genres as string[];
    
    this.filteredGenres = this.possibleGenres().filter(g =>
      g.toLowerCase().includes(value) && !selected.some(s => s.toLowerCase() === g.toLowerCase())
    ).sort((a, b) => a.localeCompare(b));
  }
  
  selectGenreFromList(genre: string) {
    this.addGenre(genre);
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
      this.filteredRecommenders = [];
      return;
    }
    const selected = this.form.value.recommenders as string[];

    this.filteredRecommenders = this.possibleRecommenders().filter(g =>
      g.toLowerCase().includes(value) && !selected.some(s => s.toLowerCase() === g.toLowerCase())
    ).sort((a, b) => a.localeCompare(b));
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
}
