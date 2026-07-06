import { Component, effect, inject, Injector, Input, runInInjectionContext, Signal } from '@angular/core';
import { ControlContainer, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ReadBookDto } from '../../../models/read/ReadBookDto';

@Component({
  selector: 'app-book-creation-form',
  imports: [ReactiveFormsModule],
  templateUrl: './book-creation-form.html',
  styleUrl: './book-creation-form.css',
})
export class BookCreationForm {
  private formBuilder = inject(FormBuilder);
  private controlContainer = inject(ControlContainer)
  private injector = inject(Injector);
  @Input() book!: Signal<ReadBookDto | null>;

  bookForm = this.formBuilder.group({
    author: ['0'],
    pageCount: [0],
    language: ['English'],
  });



  ngOnInit() {
    const parentForm: any = this.controlContainer.control as FormGroup;
    parentForm.addControl('book', this.bookForm)
    if (this.book != null) {
      this.prefillFrom();
    }

    runInInjectionContext(this.injector, () => {
      effect(() => {
        const album = this.book();

        if (album) {
          this.prefillFrom();
        }
      })
    })
  }

  ngOnDestroy() {
    const parent = this.controlContainer.control as FormGroup;
    parent.removeControl('book');
  }
  prefillFrom() {
    this.bookForm.patchValue({ pageCount: this.book()?.pageCount })
    this.bookForm.patchValue({ author: this.book()?.author })
    this.bookForm.patchValue({ language: this.book()?.language })

    const code = this.book()?.language;

    let language = code ?? '';

    try {
      if (code) {
        const displayNames = new Intl.DisplayNames(['en'], { type: 'language' });
        language = displayNames.of(code) ?? code;
      }
    } catch {
      language = code ?? '';
    }

    this.bookForm.patchValue({
      language: language
    });
  }
}
