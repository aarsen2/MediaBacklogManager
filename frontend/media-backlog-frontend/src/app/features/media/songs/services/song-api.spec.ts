import { TestBed } from '@angular/core/testing';

import { SongApi } from './song-api';

describe('SongApi', () => {
  let service: SongApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SongApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
