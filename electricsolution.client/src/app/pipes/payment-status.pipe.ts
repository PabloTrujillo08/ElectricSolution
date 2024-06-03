import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'paymentStatus'
})
export class PaymentStatusPipe implements PipeTransform {

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  transform(value: any): string {
    switch (value) {
      case 0:
        return 'Pendiente';
      case 1:
        return 'Pagado';
      case 2:
        return 'Vencida';
      case 3:
        return 'Cancelado';
      default:
        return 'Desconocido';
    }
  }
}
