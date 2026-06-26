import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimeToCompleteReport } from './time-to-complete-report';

describe('TimeToCompleteReport', () => {
  let component: TimeToCompleteReport;
  let fixture: ComponentFixture<TimeToCompleteReport>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TimeToCompleteReport]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TimeToCompleteReport);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
