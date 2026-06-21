import { TestBed } from '@angular/core/testing';

import { MediaBacklogService } from './media-backlog-service';

describe('MediaBacklogService', () => {
  let service: MediaBacklogService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MediaBacklogService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
