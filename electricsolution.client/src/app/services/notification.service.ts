import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { notification } from '../interfaces/notification.interface';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})

export class NotificationService {

  private baseUrl = '/api/Notifications';
  constructor(private http: HttpClient) { }

  getUnreadNotifications(clientId: string): Observable<notification[]> {
    return this.http.get<notification[]>(`${this.baseUrl}/GetUnreadNotification/${clientId}`);
  }

  putMarkAsRead(notificationId: string): Observable<notification[]> {
    return this.http.put<notification[]>(`${this.baseUrl}/MarkAsRead/${notificationId}`, null);
  }

}
