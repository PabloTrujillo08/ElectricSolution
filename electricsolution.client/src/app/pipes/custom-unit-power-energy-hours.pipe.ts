import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'customUnitPowerEnergyHours'
})
export class CustomUnitPowerEnergyHoursPipe implements PipeTransform {

  transform(value: number, unit: string = 'kW'): string {
    if (unit === 'kW') {
      return `${(value / 1000).toFixed(2)} kW`;
    } else if (unit === 'kWh') {
      // Si necesitas convertir kW a kWh, puedes hacer la conversión aquí.
      return `${(value / 1000).toFixed(2)} kWh`;
    } else {
      // Manejo para otros casos no especificados.
      return `${value} ${unit}`;
    }
  }

}
