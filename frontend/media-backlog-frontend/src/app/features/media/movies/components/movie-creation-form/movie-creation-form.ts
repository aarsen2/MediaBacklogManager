import { Component, effect, inject, Injector, Input, runInInjectionContext, Signal, SimpleChanges } from '@angular/core';
import { ControlContainer, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ReadMovieDto } from '../../../models/read/ReadMovieDto';
import { SignalFormsConfig } from '@angular/forms/signals';
@Component({
  selector: 'app-movie-creation-form',
  imports: [ReactiveFormsModule,],
  templateUrl: './movie-creation-form.html',
  styleUrl: './movie-creation-form.css',
})
export class MovieCreationForm {
  private formBuilder = inject(FormBuilder);
  private controlContainer = inject(ControlContainer)
  private injector = inject(Injector);
  @Input() movie!: Signal<ReadMovieDto | null>;

  
  movieForm = this.formBuilder.group({
    runTime: [0],
    language: ['English'],
    director: ['',],
    contentRating: ["PG", Validators.required],
  });



  ngOnInit() {
    const parentForm: any = this.controlContainer.control as FormGroup;
    parentForm.addControl('movie', this.movieForm)
    if (this.movie() != null) {
      this.prefillFrom();
    }

    runInInjectionContext(this.injector, () => {
      effect(() => {
        const movie = this.movie();

        if (movie) {
          this.prefillFrom();
        }
      })
    })
  }

  ngOnDestroy() {
    const parent = this.controlContainer.control as FormGroup;
    parent.removeControl('movie');
  }
  prefillFrom(){
    this.movieForm.patchValue({
    director: this.movie()?.director,
    contentRating: this.movie()?.contentRating,
    runTime: this.movie()?.runTime
  });

  const code = this.movie()?.language;

  let language = code ?? '';

  try {
    if (code) {
      const displayNames = new Intl.DisplayNames(['en'], { type: 'language' });
      language = displayNames.of(code) ?? code;
    }
  } catch {
    language = code ?? '';
  }

  this.movieForm.patchValue({
    language: language
  });
  }
}
