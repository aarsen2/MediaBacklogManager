import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PriorityItemsReport } from './priority-items-report';

describe('PriorityItemsReport', () => {
  let component: PriorityItemsReport;
  let fixture: ComponentFixture<PriorityItemsReport>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PriorityItemsReport]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PriorityItemsReport);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
