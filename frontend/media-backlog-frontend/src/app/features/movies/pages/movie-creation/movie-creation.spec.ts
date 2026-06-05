import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MovieCreation } from './movie-creation';

describe('MovieCreation', () => {
  let component: MovieCreation;
  let fixture: ComponentFixture<MovieCreation>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MovieCreation]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MovieCreation);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
