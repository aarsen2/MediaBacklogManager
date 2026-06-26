import { Component, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators, ValueChangeEvent } from '@angular/forms';
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
import { HomePage } from '../../../../home/pages/home-page/home-page';

@Component({
  selector: 'app-media-creation',
  imports: [TitleCasePipe,ReactiveFormsModule, MovieCreationForm, ShowCreationForm, AlbumCreationForm, BookCreationForm, GameCreationForm, SongCreationForm],
  templateUrl: './media-creation.html',
  styleUrl: './media-creation.css',
})
export class MediaCreation {

  //injection
  private backlogService = inject(MediaBacklogService)
  private formBuilder = inject(FormBuilder);
  private router = inject(Router)

  //inital variable setting
  successMessage = signal<string|null>(null) 
  errorMessage = signal<string|null>(null) 
  isSubmitting: boolean = false;


  form = this.formBuilder.group({
    title: ['', [Validators.required, Validators.maxLength(200)]],
    mediaType: ["", [Validators.required]],
    releaseDate: [new Date().toISOString().split('T')[0]],
    description: ['', [Validators.maxLength(1000)]],
    genreInput: [''],
    genres: [[] as string[]],
    status: ["Backlog"],
    prioritized: ["no"],
    userRating: [0],
    notes: [''],
  });

  addGenre() {
    console.log("adding Genre")
    const value = this.form.value.genreInput?.trim();
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

  // optional: allow comma to add
  handleKeydown(event: KeyboardEvent) {
    if (event.key === ',' || event.key === 'Enter') {
      event.preventDefault();
      this.addGenre();
    }

  }


  private mapBase(formValue: any): BaseForm {
    return {
      title: formValue.title,
      mediaType: formValue.mediaType,
      releaseDate: formValue.releaseDate,
      description: formValue.description,
      genres: formValue.genres ?? [],
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
