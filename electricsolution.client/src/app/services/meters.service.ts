import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { EnergyControl } from '../interfaces/energy-control.interface';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class MetersService {
  private meters$ = new BehaviorSubject<EnergyControl[]>([]);
  token: string | null = null;
  constructor(private http: HttpClient, private authService: AuthService) { }

  updateMeters(): void {
    this.authService.getToken().subscribe(token => {
      this.token = token;
    });


    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.token}`
    });


    this.http.get<EnergyControl[]>('/api/EnergyControls', { headers: headers }).subscribe({
      next: (result) => this.meters$.next(result),
      error: (error) => console.error(error),
      complete: () => console.log('updateMeters completado')
    });
  }

  getMeters(): Observable<EnergyControl[]> {
    return this.meters$.asObservable();
  }
}
