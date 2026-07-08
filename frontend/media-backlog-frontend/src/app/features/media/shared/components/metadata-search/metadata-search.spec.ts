import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MetadataSearch } from './metadata-search';

describe('MetadataSearch', () => {
  let component: MetadataSearch;
  let fixture: ComponentFixture<MetadataSearch>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MetadataSearch]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MetadataSearch);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
