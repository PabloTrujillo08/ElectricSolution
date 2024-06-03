import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'; // Importa Router desde @angular/router
import { AuthService } from '../services/auth.service';
//import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  email: string = 'cliente@demo.com';
  password: string = '#El4008640*';

  loginError: string = '';
  registrationSuccess: boolean = false;
  constructor(private authService: AuthService, private router: Router) { } // Inyecta Router en el constructor

  ngOnInit(): void {

    const registrationSuccess = localStorage.getItem('registrationSuccess');
    if (registrationSuccess === 'true') {
      // Si hay un registro exitoso, muestra el mensaje y elimina el estado
      this.registrationSuccess = true;
      localStorage.removeItem('registrationSuccess');
    }
  }

  cancelLogin() {

    this.router.navigate(['/']); // Redirige al usuario a la ruta '/'
  }


  login(): void {
    this.authService.login(this.email, this.password).subscribe({
      next: (response) => {
        console.log('Login successful!', response);
        // Actualiza el BehaviorSubject con los nuevos datos de la respuesta
        this.authService.loggedClient.next(response);
        //localStorage.setItem('jwtToken', response.token);
        // Redirige al usuario a la ruta '/'
        this.router.navigate(['/']);
      },
      error: (error) => {
        console.error('Login error', error);
        if (error.status === 401) {
          this.loginError = 'Credenciales incorrectas. Por favor, inténtelo de nuevo.';
        } else {
          this.loginError = error.message;
          //this.loginError = 'Por favor, verifique su email y contraseña e inténtelo nuevamente.';
        }
      }
    });
  }

  goToRegistro() {
    this.router.navigate(['/register']);
  }

}
