import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MovieCreationForm } from './movie-creation-form';

describe('MovieCreationForm', () => {
  let component: MovieCreationForm;
  let fixture: ComponentFixture<MovieCreationForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MovieCreationForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MovieCreationForm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
