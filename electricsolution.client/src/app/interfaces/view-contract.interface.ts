// Interface principal para el contrato
export interface MyContract {
  id: number;
  powerContracted: number;
  contractNumber: string;
  bankAccount: string;
  startDate: string; // Considera usar Date
  endDate: string | null; // Considera usar Date
  fiscalAddress: boolean;
  address: Address;
  clientId: string;
  tariffId: number;
  tariffName: string;
  tariffDescription: string;
  notifications: MyNotification[];
  invoices: Invoice[];
  installation: Installation[];
  solarBattery?: SolarBattery | null; // Marca como opcional ya que algunos objetos no lo tienen
}

// Interface para representar la dirección
export interface Address {
  street: string;
  doorNumber: string;
  city: string;
  zipCode: string;
  provinceId: number;
  province: string;
  isMainAddress: boolean;
}

// Interface para representar las notificaciones
export interface MyNotification {
  message: string;
  sentDate: string; 
  read: boolean;
  reason: number;
  clientId: string;
  client: null; 
  contractId: number;
  id: number;
}

// Interface para las facturas
export interface Invoice {
  issuedDate: string; // Considera usar Date
  dueDate: string; // Considera usar Date
  amount: number;
  paymentStatus: number;
  contractId: number;
  id: number;
}

// Interface para el consumo de energía
export interface EnergyConsumption {
  date: string; // Considera usar Date
  consumptionKWh: number;
  installationId: number;
  id: number;
}

// Interface para las baterías de energía solar
export interface SolarEnergyBattery {
  solarEnergyId: number;
  solarBatteryId: number;
  id: number;
}

// Interface para la energía solar
export interface SolarEnergy {
  description: string;
  capacity: number;
  injection: boolean;
  battery: boolean;
  solarEnergyBatteries: SolarEnergyBattery[];
  id: number;
}

// Interface para las instalaciones solares
export interface InstallationSolarEnergy {
  installationId: number;
  solarEnergyId: number;
  solarEnergy: SolarEnergy;
  id: number;
}

// Interface para la instalación
export interface Installation {
  ownMeter: boolean;
  cup: string;
  powerContracted: number;
  contractId: number;
  energyConsumptions: EnergyConsumption[];
  installationSolarEnergies: InstallationSolarEnergy[];
  id: number;
}

// Interface opcional para la batería solar, si es necesaria
export interface SolarBattery {
  id: number;
  description: string;
  capacity: number;
}



export interface HeaderContractData {
  contractNumber: string;
  cup: string;
  address: string;
  startDate: Date;

  //Instalacion
  ownMeter: boolean;
  powerContracted: string;

  //Tarifa
  tariffName: string;
  tariffDescription: string;

  //Detalles de factura
  invoices: { issuedDate: Date, amount: number, paymentStatus: number, consumptionKWh: number }[]; 
/*  consumptionKWh: number;*/
  bankAccount: string;
  dueDate: Date | null;
  notifications: MyNotification[];

  //Paneles solares
  solarCapacity: number;
  solarInjection: boolean;
  solarBattery: boolean;
  solarBatteryDescription: string;
}
