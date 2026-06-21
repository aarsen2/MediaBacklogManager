import { TestBed } from '@angular/core/testing';

import { UserMediaApi } from './user-media-api';

describe('UserMediaApi', () => {
  let service: UserMediaApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserMediaApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
