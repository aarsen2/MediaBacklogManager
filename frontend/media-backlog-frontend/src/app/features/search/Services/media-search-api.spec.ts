import { TestBed } from '@angular/core/testing';
import { MediaSearchApi } from './media-search-api';


describe('MediaSearchApi', () => {
  let service: MediaSearchApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MediaSearchApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
