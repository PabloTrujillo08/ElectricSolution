// src/app/services/form-setup.service.ts
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ibanValidator } from './ibanValidator'; // Asumiendo que también exportas validTariffValidator
import { Holiday, ITariff } from '../../interfaces/Contract.interface';

export function createAddressFormGroup(fb: FormBuilder) {
  return fb.group({
    street: ['C/ Lope de Rueda 62', Validators.required],
    doorNumber: ['Bajo', Validators.required],
    provinceId: [31, Validators.required],
    city: ['Madrid', Validators.required],
    zipCode: ['28009', [Validators.required, Validators.pattern(/^\d{5}$/)]]
  });
}

export function createContractFormGroup(fb: FormBuilder) {
  return fb.group({
    powerContracted: ['3300', Validators.required],
    tariff: [null, [Validators.required, validTariffValidator()]],
    ownMeter: [false, Validators.required],
    hasSolarPanels: [false, Validators.required],
    selectedTariff: [null]
  });
}

export function createSolarInstallationFormGroup(fb: FormBuilder) {
  return fb.group({
    capacity: [''],
    inverterModel: [''],
    battery: [false, Validators.required],
    capacityBattery: [''],
    modelBattery: [''],
    injection: [false, Validators.required],
  });
}

export function createBillingFormGroup(fb: FormBuilder) {



  return fb.group({
    bankAccount: ['ES91 2100 0418 4502 0005 1332', [Validators.required, ibanValidator()]],
    startDate: [getLaboralDate(holidays()), [Validators.required, weekdayValidator(holidays())]],
    fiscalAddress: [true, Validators.required]
  });
}

function validTariffValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value as ITariff;  // Asumiendo que el valor es de tipo TariffDetail
    const isForbidden = value && value.id === null;
    return isForbidden ? { forbiddenTariff: { value } } : null;
  };
}

export function validateNumber(event: KeyboardEvent): void {
  if (!/[0-9]/.test(event.key)) {
    event.preventDefault();
  }
}

export function isFormValid(form: FormGroup): boolean {
  const addressValid = form.get('addressInfo')?.valid ?? false;
  const contractValid = form.get('contractInfo')?.valid ?? false;
  const billingValid = form.get('billingInfo')?.valid ?? false;

  return addressValid && contractValid && billingValid;
}
export function getTodayDate(): string {
  const today = new Date();
  const dayOfWeek = today.getDay(); // 0 es domingo, 6 es sábado
  let increment = 1; 

  if (dayOfWeek === 5)
    increment = 3;  // Saltar al lunes
   else if (dayOfWeek === 6)  // Sábado
    increment = 2;  // También saltar al lunes

  today.setDate(today.getDate() + increment);
  return today.toISOString().split('T')[0];
}
function getLaboralDate(holidays: Holiday[]): string {
  const date = new Date();
  date.setHours(0, 0, 0, 0); 
  let increment = 0;
  const dayOfWeek = date.getDay();


  do {
    // Incrementar la fecha si es necesario para evitar fines de semana.
    if (dayOfWeek === 6)  // Sábado
      increment = 2
     else if (dayOfWeek === 0)  // Domingo
      increment = 1

    date.setDate(date.getDate() + increment);

    // Comprobar si la fecha es un día festivo
    const isHoliday = isHolidayDate(date, holidays);

    if (isHoliday) 
      date.setDate(date.getDate() + 1); // Incrementar un día si es festivo
    
  } while (date.getDay() === 0 || date.getDay() === 6 || isHolidayDate(date, holidays));

  return getFormattedDate(date);

}
function isHolidayDate(date: Date, holidays: Holiday[]): boolean {
  const dateIso = getFormattedDate(date);
  return holidays.some(holiday => {
    const holidayDate = new Date(holiday.date);
    holidayDate.setHours(0, 0, 0, 0); // Ajustar la hora de los festivos para coincidir con la comparación
    const holidayDateIso = getFormattedDate(holidayDate);
    return holidayDateIso === dateIso;
  });
}
function getFormattedDate(date: Date): string {

  const year = date.getFullYear();
  const month = (date.getMonth() + 1).toString().padStart(2, '0'); // Meses van de 0 a 11, así que sumamos 1.
  const day = date.getDate().toString().padStart(2, '0');
  return `${year}-${month}-${day}`;
}

function weekdayValidator(holidays: Holiday[]): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (!control.value) {
      return null;
    }

    const inputDate = new Date(control.value);
    const dateIso = inputDate.toISOString().split('T')[0]; // Convertir la fecha de entrada a formato ISO sin hora

    // Verificar si es fin de semana
    const day = inputDate.getDay();
    if (day === 0 || day === 6) {
      return { weekday: 'Debe seleccionar un día laboral' };
    }

    // Verificar si es un día festivo
    let holidayMessage = null;
    holidays.some(holiday => {
      const holidayDate = new Date(holiday.date);
      const holidayDateIso = `${holidayDate.getFullYear()}-${(holidayDate.getMonth() + 1).toString().padStart(2, '0')}-${holidayDate.getDate().toString().padStart(2, '0')}`;
      if (holidayDateIso === dateIso) {
        holidayMessage = `La fecha seleccionada es un día festivo:<br/><div class='holiday-error'><b>${holiday.name}</b></div>`
        return true; 
      }
      return false;
    });

    if (holidayMessage) 
      return { weekday: holidayMessage };

    return null;

  };
}

function holidays(): Holiday[] {
  const holidays: Holiday[] = [{
    "date": "2024-01-01 00:00:00",
    "start": "2023-12-31T23:00:00.000Z",
    "end": "2024-01-01T23:00:00.000Z",
    "name": "Año Nuevo",
    "type": "public",
    "rule": "01-01"
  }, {
    "date": "2024-01-06 00:00:00",
    "start": "2024-01-05T23:00:00.000Z",
    "end": "2024-01-06T23:00:00.000Z",
    "name": "Día de los Reyes Magos",
    "type": "public",
    "rule": "01-06"
  }, {
    "date": "2024-03-19 00:00:00",
    "start": "2024-03-18T23:00:00.000Z",
    "end": "2024-03-19T23:00:00.000Z",
    "name": "San José",
    "type": "observance",
    "rule": "03-19"
  }, {
    "date": "2024-03-28 00:00:00",
    "start": "2024-03-27T23:00:00.000Z",
    "end": "2024-03-28T23:00:00.000Z",
    "name": "Jueves Santo",
    "type": "public",
    "rule": "easter -3"
  }, {
    "date": "2024-03-29 00:00:00",
    "start": "2024-03-28T23:00:00.000Z",
    "end": "2024-03-29T23:00:00.000Z",
    "name": "Viernes Santo",
    "type": "public",
    "rule": "easter -2"
  }, {
    "date": "2024-03-31 00:00:00",
    "start": "2024-03-30T23:00:00.000Z",
    "end": "2024-03-31T22:00:00.000Z",
    "name": "Pascua",
    "type": "observance",
    "rule": "easter"
  }, {
    "date": "2024-05-01 00:00:01",
    "start": "2024-04-30T22:00:00.000Z",
    "end": "2024-05-01T22:00:00.000Z",
    "name": "Día del trabajador",
    "type": "public",
    "rule": "05-01"
  }, {
    "date": "2024-05-05 00:00:00",
    "start": "2024-05-04T22:00:00.000Z",
    "end": "2024-05-05T22:00:00.000Z",
    "name": "Día de la Madre",
    "type": "observance",
    "rule": "1st sunday in May"
  }, {
    "date": "2024-05-19 00:00:00",
    "start": "2024-05-18T22:00:00.000Z",
    "end": "2024-05-19T22:00:00.000Z",
    "name": "Pentecostés",
    "type": "observance",
    "rule": "easter 49"
  }, {
    "date": "2024-07-25 00:00:00",
    "start": "2024-07-24T22:00:00.000Z",
    "end": "2024-07-25T22:00:00.000Z",
    "name": "Santiago Apostol",
    "type": "observance",
    "rule": "07-25"
  }, {
    "date": "2024-08-15 00:00:00",
    "start": "2024-08-14T22:00:00.000Z",
    "end": "2024-08-15T22:00:00.000Z",
    "name": "Asunción",
    "type": "public",
    "rule": "08-15"
  }, {
    "date": "2024-10-12 00:00:00",
    "start": "2024-10-11T22:00:00.000Z",
    "end": "2024-10-12T22:00:00.000Z",
    "name": "Fiesta Nacional de España",
    "type": "public",
    "rule": "10-12"
  }, {
    "date": "2024-11-01 00:00:00",
    "start": "2024-10-31T23:00:00.000Z",
    "end": "2024-11-01T23:00:00.000Z",
    "name": "Todos los Santos",
    "type": "public",
    "rule": "11-01"
  }, {
    "date": "2024-12-06 00:00:00",
    "start": "2024-12-05T23:00:00.000Z",
    "end": "2024-12-06T23:00:00.000Z",
    "name": "Día de la Constitución Española",
    "type": "public",
    "rule": "12-06"
  }, {
    "date": "2024-12-08 00:00:00",
    "start": "2024-12-07T23:00:00.000Z",
    "end": "2024-12-08T23:00:00.000Z",
    "name": "La inmaculada concepción",
    "type": "public",
    "rule": "12-08"
  }, {
    "date": "2024-12-09 00:00:00",
    "start": "2024-12-08T23:00:00.000Z",
    "end": "2024-12-09T23:00:00.000Z",
    "name": "La inmaculada concepción (día sustituto)",
    "type": "observance",
    "rule": "substitutes 12-08 if sunday then next monday"
  }, {
    "date": "2024-12-25 00:00:00",
    "start": "2024-12-24T23:00:00.000Z",
    "end": "2024-12-25T23:00:00.000Z",
    "name": "Navidad",
    "type": "public",
    "rule": "12-25"
  }]



  return holidays
}
