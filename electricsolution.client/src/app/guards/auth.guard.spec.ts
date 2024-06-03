import { TestBed } from '@angular/core/testing';
import { CanActivate } from '@angular/router';
import { AuthGuard } from './auth.guard'; // Asegúrate de que la importación tenga el nombre correcto

describe('AuthGuard', () => {
  let guard: AuthGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(AuthGuard); // Obtiene una instancia de AuthGuard
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });

  // Aquí puedes añadir más pruebas para los métodos de AuthGuard
});
