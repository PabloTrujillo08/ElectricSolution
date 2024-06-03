import { TestBed } from '@angular/core/testing';

import { ViewContractService } from './view-contract.service';

describe('ViewContractService', () => {
  let service: ViewContractService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ViewContractService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
