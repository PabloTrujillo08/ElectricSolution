import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component'; 
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { ContractComponent } from './contract/contract.component';
import { MeasurementsComponent } from './measurements/measurements.component';
import { MyAccountComponent } from './edit-account/edit-account.component';
import { TariffsComponent } from './tariffs/tariffs.component';
import { ViewContractsComponent } from './view-contracts/view-contracts.component';


const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '', component: HomeComponent/*, canActivate: [AuthGuard]*/ },

  { path: 'contract', component: ContractComponent, canActivate: [AuthGuard] }, 
  { path: 'measurements', component: MeasurementsComponent, canActivate: [AuthGuard] }, 
  { path: 'tariffs', component: TariffsComponent, canActivate: [AuthGuard] }, 
  { path: 'my-account', component: MyAccountComponent, canActivate: [AuthGuard] },
  { path: 'view-contracts', component: ViewContractsComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
