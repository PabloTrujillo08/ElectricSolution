//Core
import { LOCALE_ID, NgModule } from '@angular/core';
import localeEs from '@angular/common/locales/es';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
// Routes
import { AuthGuard } from './guards/auth.guard';
import { AppRoutingModule } from './app-routing.module';
// Forms
import { FormsModule } from '@angular/forms'; //
import { ReactiveFormsModule } from '@angular/forms';
import { registerLocaleData } from '@angular/common';
//Pagination
import { NgxPaginationModule } from 'ngx-pagination';
// Components
import { AppComponent } from './app/app.component';
import { GaugeChartComponent } from './gauge-chart/gauge-chart.component';
import { RegisterComponent } from './register/register.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ContractComponent } from './contract/contract.component';
import { BarChartComponent } from './bar-chart/bar-chart.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { MeasurementsComponent } from './measurements/measurements.component';
import { MyAccountComponent } from './edit-account/edit-account.component';
import { TariffsComponent } from './tariffs/tariffs.component';
import { ViewContractsComponent } from './view-contracts/view-contracts.component';
// Pipes
import { CustomCurrencyPipe } from './pipes/custom-currency.pipe';
import { PaymentStatusPipe } from './pipes/payment-status.pipe';
import { MaskSensitiveInfoPipe } from './pipes/mask-sensitive-info.pipe';
import { CustomUnitPowerEnergyHoursPipe } from './pipes/custom-unit-power-energy-hours.pipe';
import { CustomUTCDatePipe } from './pipes/custom-utc.date.pipe';





registerLocaleData(localeEs);

@NgModule({
  declarations: [
    AppComponent,
    GaugeChartComponent,
    BarChartComponent,
    RegisterComponent,
    LoginComponent,
    HomeComponent,
    NavMenuComponent,
    ContractComponent,
    CustomCurrencyPipe,
    MeasurementsComponent,
    MyAccountComponent,
    TariffsComponent,
    ViewContractsComponent,
    PaymentStatusPipe,
    MaskSensitiveInfoPipe,
    CustomUnitPowerEnergyHoursPipe,
    CustomUTCDatePipe,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    NgxPaginationModule
  ],
  providers: [AuthGuard, { provide: LOCALE_ID, useValue: 'es-ES' }],
  bootstrap: [AppComponent]
})
export class AppModule { }
