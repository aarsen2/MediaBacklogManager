import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlbumCreationForm } from './album-creation-form';

describe('AlbumCreationForm', () => {
  let component: AlbumCreationForm;
  let fixture: ComponentFixture<AlbumCreationForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AlbumCreationForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AlbumCreationForm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
