import { TestBed } from '@angular/core/testing';

import { LoadSelectsDataService } from './load.selects.data.service';

describe('LoadSelectsDataService', () => {
  let service: LoadSelectsDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoadSelectsDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
