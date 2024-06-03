export interface IContract {
  clientId: string,
  bankAccount: string;
  startDate: string | null;
  fiscalAddress: boolean;
  tariffId: number | null;
  powerContracted: number;
  addresses: IAddress[];
  installations: Installations[];
  solarBattery: SolarBattery[];
  solarEnergies: SolarEnergies[];

  message: string;
  success: boolean;
}

export interface Installations {
  ownMeter: boolean;
  powerContracted: number;
  cup: string;
  energyConsumptions: IEnergyConsumption[];
  installationSolarEnergies: IInstallationSolarEnergy[];

}



export interface SolarBattery {
  description: string;
  capacity: number;
}
export interface SolarEnergies {
  description: string;
  capacity: number;
  injection: boolean;
  battery: boolean;
}


export interface IAddress {
  street: string;
  doorNumber: string;
  city: string;
  zipCode: string;
  provinceId: number;
  isMainAddress: boolean;
}
export interface IProvince {
  id: number;
  name: string;
}
export interface ITariffRate {
  id: number;
  tariffId: number;
  costPerKWh: number;
  startTime: string;
  endTime: string;
  description?: string;
  tariffHoursId: number;

}

export interface ITariff {
  id: number | null;
  name: string;
  description: string;
  rates: ITariffRate[];
}
export interface IEnergyConsumption {
  consumptionType: string;
  amount: number;
  date: Date;
  consumptionKWh: number;
}

export interface IInstallationSolarEnergy {
  installationId: number;
  solarEnergyId: number;
}


export interface Holiday {
  date: string;
  name: string;
  start: string;
  end: string;
  type: string;
  rule: string;
  //note: string;
}
