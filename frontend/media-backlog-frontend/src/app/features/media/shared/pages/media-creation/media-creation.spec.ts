import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MediaCreation } from './media-creation';

describe('MediaCreation', () => {
  let component: MediaCreation;
  let fixture: ComponentFixture<MediaCreation>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MediaCreation]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MediaCreation);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
