import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ContractService } from './contract/contract.service';
import { AvailablePower } from '../interfaces/available-power';
import { IProvince, ITariff } from '../interfaces/Contract.interface';
@Injectable({
  providedIn: 'root'
})
export class LoadSelectsDataService {

constructor(private contractService: ContractService) { }

  loadAvailablePowers(): Observable<AvailablePower[]> {
    return this.contractService.getAvailablePowers();
  }

  loadAvailableBatteryModels(): Observable<AvailablePower[]> {
    return this.contractService.getAvailableBatteryModels();
  }

  loadAvailableInverterModels(): Observable<AvailablePower[]> {
    return this.contractService.getAvailableInverterModels();
  }

  loadProvinces(): Observable<IProvince[]> {
    return this.contractService.getProvinces();
  }

  loadTariffs(): Observable<ITariff[]> {
    return this.contractService.getTariffs();
  }
}
