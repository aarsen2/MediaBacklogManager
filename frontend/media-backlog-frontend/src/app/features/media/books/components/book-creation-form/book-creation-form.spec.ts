import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookCreationForm } from './book-creation-form';

describe('BookCreationForm', () => {
  let component: BookCreationForm;
  let fixture: ComponentFixture<BookCreationForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookCreationForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookCreationForm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
