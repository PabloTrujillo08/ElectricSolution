import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterClient } from '../../interfaces/RegisterClient.interface';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AddressUpdate {

  private baseUrl = '/api/ClientEdit'; 


  constructor(private http: HttpClient) { }



  UpdateClientData(userId: string, regClient: RegisterClient): Observable<any> {
    console.log("updateUser")
    return this.http.put(`${this.baseUrl}/UpdateClientData/${userId}`, regClient);
  }

  getClientData(userId: string): Observable<RegisterClient> {
    return this.http.get<RegisterClient>(`${this.baseUrl}/GetClientData/${userId}`);
  }
}
