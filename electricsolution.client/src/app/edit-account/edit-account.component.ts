import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { AddressUpdate } from '../services/client-data/client-data.service';
//import { Observable } from 'rxjs';
import { IProvince } from '../interfaces/Contract.interface';
import { Router } from '@angular/router';
import { ContractService } from '../services/contract/contract.service';
import { RegisterClient } from '../interfaces/RegisterClient.interface';
import { resetPassword, ToggleFields } from '../interfaces/reset-password.interface';

@Component({
  selector: 'edit-account',
  templateUrl: './edit-account.component.html',
  styleUrls: ['./edit-account.component.css']
})
export class MyAccountComponent implements OnInit {
  // Variables para los datos del formulario
  Name: string = '';
  LastName: string = '';
  UserName: string = '';
  Address: string = '';
  City: string = '';
  selectedProvince: number = 0;
  ZipCode: string = '';
  DoorNumber: string = '';
  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  PhoneNumber: string = '';
  IsMainAddress: boolean = true
  isAuthenticated = false;
  vat: string= "12345678Z"
  passwordFieldType: boolean = false;
  currentPasswordFieldType: boolean = false;
  // Variable para controlar el paso actual del formulario
  currentStep: number = 1;
  feedbackMessage: string = '';

  isSuccess: boolean = false;
  showSuccessAlert = false;
  successMessage: string = '';

  public Provinces: IProvince[] = [];

  showBlurEffect = false;
  alertSuccess = true;

  constructor(private authService: AuthService, private router: Router, private contractService: ContractService, private AddressUpdate: AddressUpdate) { }

  ngOnInit() {
    // this.checkLoginStatus();
    this.loadProvinces();
    this.authService.getClientId().subscribe(userId => {
      if (userId)
        this.loadClientData(userId);
    });
  }

  //checkLoginStatus() {
  //  this.authService.isAuthenticated().subscribe(isAuth => {
  //    this.isAuthenticated = isAuth;
  //    console.log('User is authenticated:', this.isAuthenticated)
  //    if (!this.isAuthenticated) {
  //      console.log('User is not authenticated')
  //      // Redirigir al usuario al login si no está autenticado
  //      // this.router.navigate(['/login']);
  //    }
  //  });
  //}
  togglePasswordFieldType(fieldType: keyof ToggleFields) {
    this[fieldType] = !this[fieldType];
  }


  update(): void {
    const regClient: RegisterClient = {
      email: this.email,
      password: this.password,
      name: this.Name,
      lastName: this.LastName,
      userName: this.UserName,
      phoneNumber: this.PhoneNumber,
      vat: this.vat,
      birthDate: new Date(),
      creationDate: new Date(),
      addresses: [{
        street: this.Address,
        doorNumber: this.DoorNumber,
        city: this.City,
        zipCode: this.ZipCode,
        provinceId: this.selectedProvince,
        isMainAddress: this.IsMainAddress
      }],
    };

    this.authService.getClientId().subscribe(userId => {
      if (userId) {
        this.AddressUpdate.UpdateClientData(userId, regClient).subscribe({
          next: (success) => {
            this.feedbackMessage = success.message;
            this.isSuccess = true;
            setTimeout(() => {
              this.feedbackMessage = '';
              this.isSuccess = false;
            }, 3000);
          },
          error: (error) => {
 
           
            this.feedbackMessage = error.message;
            this.isSuccess = false;
            setTimeout(() => {
              this.feedbackMessage = '';
            }, 3000);
          }
        });
      }
    });
  }

  loadClientData(userId: string) {

    this.AddressUpdate.getClientData(userId).subscribe(clientData => {

      const { name = '', lastName = '', userName = '', email = '', phoneNumber = '', addresses = [] } = clientData;
      this.Name = name;
      this.LastName = lastName;
      this.UserName = userName;
      this.email = email;
      this.PhoneNumber = phoneNumber;

      const { street = '', doorNumber = '', city = '', zipCode = '', provinceId = 0 } = addresses[0];
      this.Address = street;
      this.DoorNumber = doorNumber;
      this.City = city;
      this.ZipCode = zipCode;
      this.selectedProvince = provinceId;

    });
  }

  loadProvinces(): void {
    this.contractService.getProvinces().subscribe(
      provinces => {
        this.Provinces = provinces;
      }
    );
  }

  ChangePassword(): void {

    const changePasswordModel: resetPassword = {
      Email: this.email,
      OriginalPassword: this.password,
      ModifiedPassword: this.confirmPassword,
      ConfirmedPassword: this.confirmPassword,
      message: ''
    };

    this.authService.changePassword(changePasswordModel).subscribe({
      next: (success) => {

        this.successMessage = success.message;
        this.showSuccessAlert = true;
        this.showBlurEffect = true;
        this.alertSuccess = true;

        setTimeout(() => {
          this.showBlurEffect = false;
          this.authService.logout();
          this.router.navigate(['/login']);
        }, 2500);

      },
      error: error => {
        this.showSuccessAlert = true;
        this.successMessage = error.error.message;
        this.alertSuccess = false;
        setTimeout(() => {
          this.showSuccessAlert = false;
          this.showBlurEffect = false;

        }, 1200);


      }
    });
  }




  nextStep(): void { this.currentStep++; }
  prevStep(): void { this.currentStep--; }



  formatDate(date: Date | string): string {
    if (typeof date === 'string') {
      date = new Date(date);
    }
    return date.toLocaleDateString('es-ES');
  }
}
