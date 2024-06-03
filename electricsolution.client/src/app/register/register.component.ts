import { Component, OnInit } from '@angular/core';
import { RegisterClient } from '../interfaces/RegisterClient.interface';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { IProvince } from '../interfaces/Contract.interface';
import { ContractService } from '../services/contract/contract.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // Variables para los datos del formulario
  Name: string = 'Cliente';
  LastName: string = 'Demo';
  UserName: string = 'clientedemo';
  Address: string = 'calle benancio 4';
  City: string = 'Madrid';
  Province: number = 31;
  ZipCode: string = '28015';
  DoorNumber: string = '4';
  email: string = 'cliente@demo.com';
  confirmEmail: string = 'cliente@demo.com';
  password: string = '#El4008640*';
  PhoneNumber: string = '666666666';
  vat: string = "12345678Z";
  birthDate: Date | null = null;

  isAuthenticated = false;

  // Variable para controlar el paso actual del formulario
  currentStep: number = 1;
  errorMessage: string = '';
  mailError: boolean = false;

  public Provinces: IProvince[] = [];
  constructor(private authService: AuthService, private router: Router, private contractService: ContractService) { }

  ngOnInit() {
    this.checkLoginStatus();
    this.loadProvinces();
  }

  checkLoginStatus() {
    this.authService.isAuthenticated().subscribe(isAuth => {
      this.isAuthenticated = isAuth;
      console.log('User is authenticated:', this.isAuthenticated)
      if (!this.isAuthenticated) {
        console.log('User is not authenticated')
        // Redirigir al usuario al login si no está autenticado
        // this.router.navigate(['/login']);
      }
    });
  }


  register(): void {

    const regClient: RegisterClient = {
      email: this.email,
      password: this.password,
      name: this.Name,
      lastName: this.LastName,
      userName: this.UserName,
      phoneNumber: this.PhoneNumber,
      vat: this.vat,
      birthDate: this.birthDate ? this.birthDate : new Date(),
      creationDate: new Date(),
      addresses: [{
        street: this.Address,
        doorNumber: this.DoorNumber,
        city: this.City,
        zipCode: this.ZipCode,
        provinceId: this.Province,
        isMainAddress: true
      }],
    };
    console.log("valor de register:", regClient)
    // Lógica para registrar al usuario usando el servicio de autenticación
    this.authService.register(regClient).subscribe({
      next: (success) => {
        localStorage.setItem('registrationSuccess', 'true');
        this.router.navigate(['/login']);
        console.log('Registro exitoso', success);
      },
      error: (error) => {
        this.mailError = true;
        // Asegúrate de acceder correctamente a los detalles del error dependiendo de cómo estén estructurados
        this.errorMessage = `${error.statusText}: ${error.error.message}`;
      }
    });
  }

  loadProvinces(): void {
    this.contractService.getProvinces().subscribe(
      provinces => {
        console.log(provinces);
        this.Provinces = provinces;
      }
    );
  }

  // Método para avanzar al siguiente paso
  nextStep(): void {
    this.currentStep++;
  }

  // Método para regresar al paso anterior
  prevStep(): void {
    this.currentStep--;
  }

  setStep(step: number): void {
    this.currentStep = step;
  }


  formatDate(date: Date | string): string {
    if (typeof date === 'string') {
      date = new Date(date);
    }
    return date.toLocaleDateString('es-ES'); // Utiliza el local de España para el formato europeo
  }
}
