import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationUpdateService {
  private contractSavedSubject = new Subject<void>();

  contractSaved$ = this.contractSavedSubject.asObservable();

  emitContractSaved() {
    this.contractSavedSubject.next();
  }
}
