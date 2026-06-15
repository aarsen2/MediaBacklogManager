import { TestBed } from '@angular/core/testing';

import { ShowApi } from './show-api';

describe('ShowApi', () => {
  let service: ShowApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ShowApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
