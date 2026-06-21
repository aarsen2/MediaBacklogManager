import { Component, inject } from '@angular/core';
import { ControlContainer, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-movie-creation-form',
  imports: [ReactiveFormsModule],
  templateUrl: './movie-creation-form.html',
  styleUrl: './movie-creation-form.css',
})
export class MovieCreationForm {
  private formBuilder = inject(FormBuilder);
  private controlContainer = inject(ControlContainer)


  movieForm = this.formBuilder.group({
    runTime: ['0'],
    language: ['English'],
    director: ['',],
    contentRating: ['PG', Validators.required],
  });



  ngOnInit() {
    const parentForm: any = this.controlContainer.control as FormGroup;
    parentForm.addControl('movie', this.movieForm)
  }

  ngOnDestroy() {
    const parent = this.controlContainer.control as FormGroup;
    parent.removeControl('movie');
  }
}
