import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'localTime'
})
export class CustomUTCDatePipe implements PipeTransform {

  transform(value: Date | string): string {
    if (!value) return '';

    // Convertir el valor a un objeto Date si no lo es
    const utcDate = new Date(value);

    // Crear una fecha para la zona horaria de Madrid
    const madridDate = new Date(utcDate);

    // Calcular el desplazamiento de Madrid respecto a UTC
    const offset = +60; // Madrid está normalmente en UTC+1 (CET)
    const dstOffset = this.isDstObserved(madridDate) ? +120 : offset; // Ajustar si es horario de verano a UTC+2 (CEST)

    // Ajustar la hora al desplazamiento
    madridDate.setMinutes(utcDate.getUTCMinutes() + dstOffset);

    return madridDate.toLocaleString('es-ES', {

      hour: '2-digit',
      minute: '2-digit',
      second: '2-digit'
    });
  }

  // Función para determinar si se observa DST en una fecha específica en Madrid
  private isDstObserved(date: Date): boolean {
    const jan = new Date(date.getFullYear(), 0, 1); // 1 de enero
    const jul = new Date(date.getFullYear(), 6, 1); // 1 de julio
    return date.getTimezoneOffset() < Math.max(jan.getTimezoneOffset(), jul.getTimezoneOffset());
  }
}


