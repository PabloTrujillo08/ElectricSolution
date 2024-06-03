import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MyContract } from '../../interfaces/view-contract.interface';

@Injectable({
  providedIn: 'root'
})
export class ViewContractService {

  private baseUrl = '/api/Contracts';
  constructor(private http: HttpClient) { }

  GetMyContract(clientId: string): Observable<MyContract[]> {
    return this.http.get<MyContract[]>(`${this.baseUrl}/GetMyContract/${clientId}`);
  }
}
