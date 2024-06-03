// Core
import { Component, OnInit, ViewChild } from '@angular/core';
// Router
import { Router } from '@angular/router';
// Forms
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { formatIban } from './functions/ibanValidator';
import { createAddressFormGroup, createContractFormGroup, createSolarInstallationFormGroup, createBillingFormGroup, validateNumber, isFormValid, getTodayDate } from './functions/validationsForms';
// Interfaces
import { IContract, IProvince, ITariff, ITariffRate, IAddress, Installations, SolarBattery, SolarEnergies } from '../interfaces/Contract.interface';
import { AvailablePower } from '../interfaces/available-power';
// Services
import { AuthService } from '../services/auth.service';
import { ContractService } from '../services/contract/contract.service';
import { NotificationUpdateService } from '../services/notification-update.service';
import { LoadSelectsDataService } from '../services/load.selects.data.service';
// Components
import { BarChartComponent } from '../bar-chart/bar-chart.component';
import { NavMenuComponent } from '../nav-menu/nav-menu.component';

@Component({
  selector: 'app-contract',
  templateUrl: './contract.component.html',
  styleUrls: ['./contract.component.css']
})
export class ContractComponent implements OnInit {

  constructor(
    private contractService: ContractService,
    private authService: AuthService,
    private notificationUpdateService: NotificationUpdateService,
    private fb: FormBuilder,
    private router: Router,
    private dataService: LoadSelectsDataService) { }

  public validateNumber = validateNumber;
  public getTodayDate = getTodayDate;

  public AvailablePowers: AvailablePower[] = [];
  public AvailableBatteryModels: AvailablePower[] = [];
  public AvailableInverterModels: AvailablePower[] = [];
  public Provinces: IProvince[] = [];
  public Tariffs: ITariff[] = [];

  @ViewChild(BarChartComponent) barChartComponent!: BarChartComponent;
  @ViewChild(NavMenuComponent) navMenuComponent!: NavMenuComponent;

  selectedTariff: ITariff | null = null;
  hasSolarPanels: boolean = true;

  tariffId: number = 0;
  selectedTariffDescription: string | undefined;
  selectedTariffRates: ITariffRate[] | undefined;

  isAuthenticated = false;
  isTariffSelected: boolean = false;
  isSuccess: boolean = false;

  currentStep: number = 1;
  statusMessage: string = '';

  userId: string = '';
  contractForm!: FormGroup;

  isButtonDisabled: boolean = false;

  ngOnInit(): void {
    this.loadAllSelects();

    this.initializeForm();
    this.setupFormListeners();
    this.authService.getClientId().subscribe(userId => {
      this.userId = userId;
    });

  }

  handleIbanInput(event: Event): void {
    const inputElement = event.target as HTMLInputElement;
    const formattedIban = formatIban(inputElement.value);
    this.contractForm.get('billingInfo.bankAccount')?.setValue(formattedIban, { emitEvent: false });
  }


  loadAllSelects(): void {
    this.dataService.loadAvailablePowers().subscribe(powers => this.AvailablePowers = powers);
    this.dataService.loadAvailableBatteryModels().subscribe(models => this.AvailableBatteryModels = models);
    this.dataService.loadAvailableInverterModels().subscribe(models => this.AvailableInverterModels = models);
    this.dataService.loadProvinces().subscribe(provinces => this.Provinces = provinces);
    this.dataService.loadTariffs().subscribe(tariffs => {
      const msgTariff = { id: null, name: 'Elije la tarifa que más te convenga', description: '', rates: [] };
      this.Tariffs = [msgTariff, ...tariffs];
      this.selectedTariff = msgTariff;
      if (this.Tariffs)
        this.contractForm.get('contractInfo')?.get('tariff')?.setValue(msgTariff);
    });
  }

  initializeForm(): void {
    this.contractForm = this.fb.group({
      addressInfo: createAddressFormGroup(this.fb),
      contractInfo: createContractFormGroup(this.fb),
      solarInstallation: createSolarInstallationFormGroup(this.fb),
      billingInfo: createBillingFormGroup(this.fb)
    });
  }
  validateForm(): boolean {
    if (isFormValid(this.contractForm)) return true
    return false
  }

  setupFormListeners(): void {
    this.contractForm.get('contractInfo.hasSolarPanels')!.valueChanges.subscribe(hasPanels => {
      const solarForm = this.contractForm!.get('solarInstallation');
      if (hasPanels) {
        solarForm?.get('capacity')!.setValidators(Validators.required);
        solarForm?.get('inverterModel')!.setValidators(Validators.required);
        solarForm?.get('battery')!.setValidators(Validators.required);
      } else {
        solarForm?.get('capacity')!.clearValidators();
        solarForm?.get('inverterModel')!.clearValidators();
        solarForm?.get('battery')!.clearValidators();
      }
      solarForm!.updateValueAndValidity();
    });
    this.contractForm.get('contractInfo.tariff')?.valueChanges.subscribe(() => {
      this.updateSelectedTariffDescription();
    });
  }

  updateSelectedTariffDescription() {
    // Extraer el valor actual de la tarifa seleccionada desde el FormGroup
    const selectedTariff = this.contractForm.get('contractInfo')?.get('tariff')?.value;

    // Actualizar la descripción y las tarifas basadas en la tarifa seleccionada
    if (selectedTariff) {
      this.selectedTariffDescription = selectedTariff.description;
      this.selectedTariffRates = selectedTariff.rates;
      this.tariffId = selectedTariff.id;
      this.isTariffSelected = true;
    } else {
      this.selectedTariffDescription = undefined;
      this.selectedTariffRates = undefined;
      this.isTariffSelected = false;
    }
    this.onTariffChange();
  }

  onTariffChange(): void {

    const tariffValues = this.calculateTariffValues();
    if (this.barChartComponent) {
      this.barChartComponent.customValues = tariffValues;
      this.barChartComponent.renderChart();
    }
  }

  getSelectedTariffName(): string {
    return this.selectedTariff ? this.selectedTariff.name : '';
  }

  getProvinceName(provinceId: number): string {
    const province = this.Provinces.find(prov => prov.id === provinceId);
    return province ? province.name : '';
  }


  RegisterContractClient(): void {

    if (this.isButtonDisabled) return;
    this.isButtonDisabled = true;

    const contract = this.prepareContractData();

    this.contractService.SendRegisterContract(contract).subscribe({
      next: (response) => {
        this.isSuccess = response.success;
        this.statusMessage = response.message;
        this.notificationUpdateService.emitContractSaved();

        if (this.isSuccess) {
          setTimeout(() => {
            this.router.navigate(['/view-contracts']);
          }, 3000);
        }

      },
      error: (error) => {
        this.isSuccess = false;
        this.statusMessage = error.error;
        this.isButtonDisabled = true; 
      }
    });
  }

  calculateTariffValues() {
    let tariffValues = new Array(24).fill(0);

    this.selectedTariffRates?.forEach(rate => {

      const startHour = parseInt(rate.startTime.split(":")[0], 10);
      const endHour = rate.endTime === "00:00:00" ? 24 : parseInt(rate.endTime.split(":")[0], 10);

      for (let i = startHour; i < endHour; i++) {
        const index = i % 24;
        tariffValues[index] = rate.costPerKWh;
      }
      tariffValues = tariffValues.map(value => value !== null ? value : 1);
      // console.log(tariffValues,"tariffValues");
      return tariffValues;
    });

    return tariffValues;
  }


  prepareContractData(): IContract {
    // Acceder a los valores desde el formulario
    const addressInfo = this.contractForm.get('addressInfo')!.value;
    const contractInfo = this.contractForm.get('contractInfo')!.value;
    console.log(contractInfo, "contractInfo")
    console.log(contractInfo.selectedTariff, "contractInfo")
    const solarInstallation = this.contractForm.get('solarInstallation')!.value;
    const billingInfo = this.contractForm.get('billingInfo')!.value;

    const installations: Installations[] = [{
      ownMeter: contractInfo.ownMeter,
      powerContracted: parseInt(contractInfo.powerContracted, 10),
      cup: "",
      energyConsumptions: [],
      installationSolarEnergies: []
    }];

    const solarEnergies: SolarEnergies[] = contractInfo.hasSolarPanels ? [{
      description: solarInstallation.inverterModel,
      capacity: parseFloat(solarInstallation.capacity),
      injection: solarInstallation.injection,
      battery: solarInstallation.battery
    }] : [];

    const solarBattery: SolarBattery[] = contractInfo.hasSolarPanels ? [{
      description: solarInstallation.modelBattery,
      capacity: parseFloat(solarInstallation.capacityBattery)
    }] : [];

    const addresses: IAddress[] = [{
      street: addressInfo.street,
      doorNumber: addressInfo.doorNumber,
      city: addressInfo.city,
      zipCode: addressInfo.zipCode,
      provinceId: addressInfo.provinceId,
      isMainAddress: false
    }];

    return {
      clientId: this.userId ?? "",
      bankAccount: billingInfo.bankAccount,
      startDate: billingInfo.startDate,
      fiscalAddress: billingInfo.fiscalAddress,
      tariffId: this.tariffId ?? 0,
      powerContracted: parseFloat(contractInfo.powerContracted),
      addresses: addresses,
      installations: installations,
      solarBattery: solarBattery,
      solarEnergies: solarEnergies,
      message: '',
      success: false
    };
  }
  nextStep(): void {
    const hasSolarPanels = this.contractForm.get('contractInfo.hasSolarPanels')?.value;

    if (this.currentStep === 2 && !hasSolarPanels)
      this.currentStep = 4;
    else
      this.currentStep++;
  }
  prevStep(): void {
    const hasSolarPanels = this.contractForm.get('contractInfo.hasSolarPanels')?.value;

    if (this.currentStep === 4 && !hasSolarPanels)
      this.currentStep = 2;
    else
      this.currentStep--;

  }
  setStep(step: number): void {
    this.currentStep = step;
  }
}
