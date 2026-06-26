import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompletedItemsReport } from './completed-items-report';

describe('CompletedItemsReport', () => {
  let component: CompletedItemsReport;
  let fixture: ComponentFixture<CompletedItemsReport>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CompletedItemsReport]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CompletedItemsReport);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
