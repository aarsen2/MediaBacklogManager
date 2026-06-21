import { Component, inject } from '@angular/core';
import { ControlContainer, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-book-creation-form',
  imports: [ReactiveFormsModule],
  templateUrl: './book-creation-form.html',
  styleUrl: './book-creation-form.css',
})
export class BookCreationForm {
  private formBuilder = inject(FormBuilder);
  private controlContainer = inject(ControlContainer)


  bookForm = this.formBuilder.group({
    author: ['0'],
    pageCount: [0 ],
    Language: ['English'],
  });



  ngOnInit() {
    const parentForm: any = this.controlContainer.control as FormGroup;
    parentForm.addControl('book', this.bookForm)
  }

  ngOnDestroy() {
    const parent = this.controlContainer.control as FormGroup;
    parent.removeControl('book');
  }
}
