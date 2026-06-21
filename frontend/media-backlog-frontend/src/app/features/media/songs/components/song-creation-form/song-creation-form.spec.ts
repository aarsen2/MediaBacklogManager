import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SongCreationForm } from './song-creation-form';

describe('SongCreationForm', () => {
  let component: SongCreationForm;
  let fixture: ComponentFixture<SongCreationForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SongCreationForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SongCreationForm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
