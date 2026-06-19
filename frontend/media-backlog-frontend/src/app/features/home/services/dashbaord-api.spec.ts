import { TestBed } from '@angular/core/testing';

import { DashbaordApi } from './dashbaord-api';

describe('DashbaordApi', () => {
  let service: DashbaordApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DashbaordApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
