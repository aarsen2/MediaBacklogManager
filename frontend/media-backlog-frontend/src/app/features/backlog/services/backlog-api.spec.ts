import { TestBed } from '@angular/core/testing';

import { BacklogApi } from './backlog-api';

describe('BacklogApi', () => {
  let service: BacklogApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BacklogApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
