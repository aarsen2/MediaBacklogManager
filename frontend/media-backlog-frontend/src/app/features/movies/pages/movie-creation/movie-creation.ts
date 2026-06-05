import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MoviesApi } from '../../services/movies-api';
import { CreateMovieDto } from '../../models/CreateMovieDto';

@Component({
  selector: 'app-movie-creation',
  imports: [ReactiveFormsModule],
  templateUrl: './movie-creation.html',
  styleUrl: './movie-creation.css',
})
export class MovieCreation {

  isSubmitting = false;
  successMessage = '';
  errorMessage = '';
  form!: FormGroup;
  constructor(
    private fb: FormBuilder,
    private api: MoviesApi
  ) {
    this.form = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],

      releaseDate: ['', Validators.required],

      generalRating: [0, [Validators.min(0), Validators.max(10)]],
      runTime: [0, [Validators.required, Validators.min(1)]],

      language: ['English', Validators.required],
      director: ['', Validators.required],
      contentRating: ['PG13', Validators.required],

      // arrays from your DTO
      assets: this.fb.control<any[]>([]),
      genres: this.fb.control<any[]>([])
    });

  }

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;
    this.successMessage = '';
    this.errorMessage = '';

    const dto: CreateMovieDto = {
      title: this.form.value.title ?? '',
      description: this.form.value.description ?? '',
      releaseDate: this.form.value.releaseDate ?? '',
      generalRating: this.form.value.generalRating ?? 0,
      runTime: this.form.value.runTime ?? 0,
      language: this.form.value.language ?? 'English',
      director: this.form.value.director ?? '',
      contentRating: this.form.value.contentRating ?? 'PG13',
      assets: this.form.value.assets ?? [],
      genres: this.form.value.genres ?? []
    };

    this.api.createMovie(dto).subscribe({
      next: () => {
        this.successMessage = 'Movie created successfully!';
        this.isSubmitting = false;

        this.form.reset({
          language: 'English',
          contentRating: 'PG13',
          generalRating: 0,
          runTime: 0,
          assets: [],
          genres: []
        });
      },
      error: () => {
        this.errorMessage = 'Failed to create movie';
        this.isSubmitting = false;
      }
    });
  }
}