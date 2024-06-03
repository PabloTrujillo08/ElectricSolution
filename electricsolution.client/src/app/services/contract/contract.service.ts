import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AvailablePower } from '../../interfaces/available-power';
import { HttpClient } from '@angular/common/http';
import { IContract, IProvince, ITariff } from '../../interfaces/Contract.interface';

@Injectable({
  providedIn: 'root'
})
export class ContractService {

  private baseUrl = '/api/RegisterContract';
  constructor(private http: HttpClient) { }

  getAvailablePowers(): Observable<AvailablePower[]> {
    return this.http.get<AvailablePower[]>(`${this.baseUrl}/AvailablePowers`);
  }

  getAvailableBatteryModels(): Observable<AvailablePower[]> {
    return this.http.get<AvailablePower[]>(`${this.baseUrl}/AvailableBatteryModels`);
  }

  getAvailableInverterModels(): Observable<AvailablePower[]> {
    return this.http.get<AvailablePower[]>(`${this.baseUrl}/AvailableInverterModels`);
  }

  getProvinces(): Observable<IProvince[]> {
    return this.http.get<IProvince[]>(`${this.baseUrl}/GetAllProvinces`);
  }

  getTariffs(): Observable<ITariff[]> {
    return this.http.get<ITariff[]>(`${this.baseUrl}/GetAllTariffs`);
  }

  SendRegisterContract(contract: IContract): Observable<IContract> {
    return this.http.post<IContract>(`${this.baseUrl}/AddContract`, contract);
  }


}
