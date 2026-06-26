import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MediaViewSimple } from './media-view-simple';

describe('MediaViewSimple', () => {
  let component: MediaViewSimple;
  let fixture: ComponentFixture<MediaViewSimple>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MediaViewSimple]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MediaViewSimple);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
