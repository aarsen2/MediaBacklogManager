import { TestBed } from '@angular/core/testing';

import { GamesApi } from './games-api';

describe('GamesApi', () => {
  let service: GamesApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GamesApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
