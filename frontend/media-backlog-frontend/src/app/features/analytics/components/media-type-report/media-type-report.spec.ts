import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MediaTypeReport } from './media-type-report';

describe('MediaTypeReport', () => {
  let component: MediaTypeReport;
  let fixture: ComponentFixture<MediaTypeReport>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MediaTypeReport]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MediaTypeReport);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
