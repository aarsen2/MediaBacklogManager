import { Component, inject, Input } from '@angular/core';
import { ControlContainer, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ReadMovieDto } from '../../../models/read/ReadMovieDto';

@Component({
  selector: 'app-movie-creation-form',
  imports: [ReactiveFormsModule],
  templateUrl: './movie-creation-form.html',
  styleUrl: './movie-creation-form.css',
})
export class MovieCreationForm {
  private formBuilder = inject(FormBuilder);
  private controlContainer = inject(ControlContainer)
  @Input() movie!: ReadMovieDto | null;

  movieForm = this.formBuilder.group({
    runTime: [0],
    language: ['English'],
    director: ['',],
    contentRating: ["PG", Validators.required],
  });



  ngOnInit() {
    const parentForm: any = this.controlContainer.control as FormGroup;
    parentForm.addControl('movie', this.movieForm)
    if (this.movie != null) {
      this.prefillFrom();
    }
  }

  ngOnDestroy() {
    const parent = this.controlContainer.control as FormGroup;
    parent.removeControl('movie');
  }
  prefillFrom() {
    this.movieForm.patchValue({ director: this.movie?.director })
    this.movieForm.patchValue({ language: this.movie?.language })
    this.movieForm.patchValue({ contentRating: this.movie?.contentRating })
    this.movieForm.patchValue({ runTime: this.movie?.runTime })
  }
}
