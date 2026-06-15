import { TestBed } from '@angular/core/testing';

import { AlbumSerivce } from './album-serivce';

describe('AlbumSerivce', () => {
  let service: AlbumSerivce;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AlbumSerivce);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
