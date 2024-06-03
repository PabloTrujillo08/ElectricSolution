import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

// Función validadora de IBAN
export function ibanValidator(): ValidatorFn {

  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value.replace(/\s/g, ''); // Remueve espacios para la validación
    if (!value) {
      return null;
    }

    // Verifica el patrón exacto: dos letras, dos dígitos, y hasta 24 dígitos adicionales (6 grupos de 4)
    const ibanRegex = /^[A-Z]{2}\d{2}(\d{4}){0,6}$/;
    const isValid = ibanRegex.test(value);

    return isValid ? null : { invalidIban: true };
  };
}

// Función de formato de IBAN
export function formatIban(inputValue: string): string {

  let value = inputValue.replace(/\s+/g, '').toUpperCase(); // Limpia y convierte a mayúsculas
  value = value.slice(0, 2).replace(/[^A-Z]/g, '') + value.slice(2).replace(/\D/g, '');
  value = value.slice(0, 24);

  let formatted = value.substring(0, 4); // Primer bloque ya formateado
  value = value.substring(4); // Resto de la cadena

  while (value.length > 0) {
    formatted += value.substring(0, 4) ? ' ' + value.substring(0, 4) : '';
    value = value.substring(4);
  }

  return formatted.trim();
}
