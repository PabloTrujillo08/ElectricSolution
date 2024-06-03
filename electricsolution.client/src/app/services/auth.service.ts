import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { AuthResponse } from '../interfaces/auth.interface';
import { map } from 'rxjs/operators';
import { RegisterClient } from '../interfaces/RegisterClient.interface';
import { resetPassword } from '../interfaces/reset-password.interface';


const DEFAULT_AUTH_RESPONSE: AuthResponse = {
  email: '',
  token: '',
  userId: '',
  username: '',
  message: ''
};


@Injectable({
  providedIn: 'root'
})



export class AuthService {
  private baseUrl = '/api/auth'; 

  public loggedClient = new BehaviorSubject<AuthResponse>(this.getStoredAuthResponse() || DEFAULT_AUTH_RESPONSE);




  constructor(private http: HttpClient) {

    this.updateAuthStateFromLocalStorage();

  }


  private updateAuthStateFromLocalStorage(): void {
    const storedAuth = this.getStoredAuthResponse();
    if (storedAuth.token) {
      this.loggedClient.next(storedAuth);
    }
  }
  private getStoredAuthResponse(): AuthResponse {
    const storedAuth = localStorage.getItem('auth');
    return storedAuth ? JSON.parse(storedAuth) : DEFAULT_AUTH_RESPONSE;
  }


  register(regClient: RegisterClient): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.baseUrl}/register`, regClient);
  }

  login(email: string, password: string): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.baseUrl}/login`, { email, password }).pipe(
      map(response => {
        console.log()
        localStorage.setItem('auth', JSON.stringify(response)); // Almacena la respuesta en el almacenamiento local
        this.loggedClient.next(response); // Actualiza el estado de autenticación
        return response;
      })
    );
  }

  


  isAuthenticated(): Observable<boolean> {
    return this.loggedClient.asObservable().pipe(
      map(authResponse => !!authResponse?.token)
    );
  }
  getToken(): Observable<string | null> {
    return this.loggedClient.asObservable().pipe(
      map(authResponse => authResponse?.token || null)
    );
  }
  getClient(): Observable<string | null> {
    return this.loggedClient.asObservable().pipe(
      map(authResponse => authResponse?.username || null)
    );
  }
  getClientId(): Observable<string> {
    return this.loggedClient.asObservable().pipe(
      map(authResponse => authResponse?.userId )
    );
  }

 
  changePassword(ResetPassword: resetPassword): Observable<resetPassword> {
    return this.http.post<resetPassword>(`${this.baseUrl}/ModifyPassword`, ResetPassword);
  }

 logout(): Observable<AuthResponse> {

   localStorage.removeItem('jwtToken');
   localStorage.removeItem('auth');
  this.loggedClient.next(DEFAULT_AUTH_RESPONSE);

  return this.http.post<AuthResponse>(`${this.baseUrl}/logout`, {});
}
}
