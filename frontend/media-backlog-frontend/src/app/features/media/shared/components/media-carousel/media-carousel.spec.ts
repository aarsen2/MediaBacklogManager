import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MediaCarousel } from './media-carousel';

describe('MediaCarousel', () => {
  let component: MediaCarousel;
  let fixture: ComponentFixture<MediaCarousel>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MediaCarousel]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MediaCarousel);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
