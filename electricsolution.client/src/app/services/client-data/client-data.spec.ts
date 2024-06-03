import { TestBed } from '@angular/core/testing';
import { AddressUpdate } from './client-data.service';



describe('UpdateAddresUipdateServiceService', () => {
  let service: AddressUpdate;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddressUpdate);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
