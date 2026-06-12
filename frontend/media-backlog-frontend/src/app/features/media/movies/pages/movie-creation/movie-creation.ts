import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CreateMovieDto } from '../../../models/create/CreateMovieDto';
import { MediaService } from '../../../shared/services/media-service';

@Component({
  selector: 'app-movie-creation',
  imports: [ReactiveFormsModule],
  templateUrl: './movie-creation.html',
  styleUrl: './movie-creation.css',
})
export class MovieCreation {

  //injection
  private mediaService = inject(MediaService);
  private formBuilder = inject(FormBuilder);
  successMessage: string = ""
  errorMessage: string = ""
  isSubmitting: boolean = false;


  form = this.formBuilder.group({
    title: ['', [Validators.required, Validators.maxLength(200)]],
    description: ['', [Validators.maxLength(1000)]],
    releaseDate: [new Date().toISOString().split('T')[0]],
    generalRating: [0, [Validators.min(0), Validators.max(5)]],
    runTime: [0],
    language: ['English'],
    director: ['', [Validators.maxLength(100)]],
    contentRating: ['PG']
  });

  submit() {
    //Checks the form's validitiy and shows any errors that exist in the form
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.isSubmitting = true;

    const formValue = this.form.getRawValue();


    const newMovie: CreateMovieDto = {
      title: formValue.title!,
      description: formValue.description ?? '',
      releaseDate: formValue.releaseDate ?? '', // 
      generalRating: formValue.generalRating ?? 0,
      runTime: formValue.runTime ?? 0,
      language: formValue.language ?? 'English',
      director: formValue.director ?? '',
      contentRating: formValue.contentRating ?? 'PG',
      genres: [],       // List of genre types. I'll figure this out later.
      assets: [] // replace with propper asset storage later
    }

    this.mediaService.createMedia<CreateMovieDto>(newMovie).subscribe({
      next: (res) => {
        console.log('Movie created:');
        console.log(res)
        this.successMessage = "Movie Created Successfully"
        this.form.reset();
      },
      error: (err) => {
        console.error('Failed to create movie:');
        this.errorMessage = "An Error Occured";
        console.error(err.error)
      }
    });


    this.isSubmitting = false;
    console.log(newMovie);
  }
}