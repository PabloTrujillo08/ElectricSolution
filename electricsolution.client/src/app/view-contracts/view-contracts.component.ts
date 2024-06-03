// Core
import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { ChangeDetectorRef } from '@angular/core';
// Interfaces
import { MyContract, HeaderContractData, MyNotification } from '../interfaces/view-contract.interface';
// Services
import { ViewContractService } from '../services/contract/view-contract.service';
import { AuthService } from '../services/auth.service';
import { NotificationService } from '../services/notification.service';
import { NotificationUpdateService } from '../services/notification-update.service';

@Component({
  selector: 'app-view-contracts',
  templateUrl: './view-contracts.component.html',
  styleUrls: ['./view-contracts.component.css']
})
export class ViewContractsComponent implements OnInit {

  activeTab: { [key: number]: number } = {};
  userId: string = '';
  contracts: MyContract[] = [];
  items: HeaderContractData[] = [];
  pageActualContract: number = 1;

  activeIndex: number | null = null;
  public itemsPerPage: number = 5;

  constructor(
    private viewContractService: ViewContractService,
    private authService: AuthService,
    private renderer: Renderer2,
    private el: ElementRef,
    private notifications: NotificationService,
    private notificationService: NotificationUpdateService,
    private cdr: ChangeDetectorRef) { }


  ngOnInit(): void {
    this.authService.getClientId().subscribe(userId => {
      this.userId = userId;
      if (this.userId) {
        this.viewContractService.GetMyContract(this.userId).subscribe({
          next: (response) => {
            this.contracts = response;
            console.log(this.contracts, 'contratos')
            this.items = this.mapContractDataToItems(this.contracts);
          },
          error: (error) => console.log(error)
        });
      }
    });
  }




  mapContractDataToItems(contractData: MyContract[]): HeaderContractData[] {
    const items: HeaderContractData[] = [];
    contractData.forEach(contract => {
      contract.installation.forEach(installation => {
        const invoices: { issuedDate: Date, amount: number, paymentStatus: number, consumptionKWh: number }[] = contract.invoices.map(invoice => {
          // Encuentra el consumo que coincide exactamente con la fecha de la factura
          const matchingConsumption = installation.energyConsumptions.find(consumption => {
            const invoiceDate = new Date(invoice.issuedDate);
            const consumptionDate = new Date(consumption.date);
            return invoiceDate.getTime() === consumptionDate.getTime();
          });
          return {
            issuedDate: new Date(invoice.issuedDate),
            amount: invoice.amount,
            paymentStatus: invoice.paymentStatus,
            consumptionKWh: matchingConsumption ? matchingConsumption.consumptionKWh : 0 // Asumimos 0 si no hay coincidencia
          };
        });

        const item: HeaderContractData = {
          // tableHeader
          contractNumber: contract.contractNumber,
          cup: installation.cup,
          address: `${contract.address.street} ${contract.address.doorNumber}, ${contract.address.city}, ${contract.address.zipCode}`,
          startDate: new Date(contract.startDate),
          // installation
          ownMeter: installation.ownMeter,
          powerContracted: `${installation.powerContracted} kWh`,
          // tariff
          tariffName: contract.tariffName,
          tariffDescription: contract.tariffDescription,
          // invoice details
          invoices: invoices,
          bankAccount: contract.bankAccount,
          dueDate: invoices.length > 0 ? new Date(invoices[invoices.length - 1].issuedDate) : null,
          notifications: contract.notifications,
          // solar panels
          solarCapacity: installation.installationSolarEnergies.length > 0 ? installation.installationSolarEnergies[installation.installationSolarEnergies.length - 1].solarEnergy.capacity : 0,
          solarInjection: installation.installationSolarEnergies.some(solarEnergy => solarEnergy.solarEnergy.injection),
          solarBattery: installation.installationSolarEnergies.some(solarEnergy => solarEnergy.solarEnergy.battery),
          solarBatteryDescription: contract.solarBattery ? contract.solarBattery.description : '',
        };
        items.push(item);
      });
    });
    console.log(items, 'items')
    return items;
  }

  countNotifications(invoices: { paymentStatus: number }[], notifications: MyNotification[]): number {
    const invoiceCount = invoices.filter(invoice => invoice.paymentStatus === 2).length
    const notificationCount = notifications.filter(n => !n.read).length;
    return invoiceCount + notificationCount;
  }

  getUnreadCount(notifications: MyNotification[]): number {
    return notifications.filter(n => !n.read).length;
  }

  getNotificationInvoiceCount(invoices: { paymentStatus: number }[]): number {
    return invoices.filter(invoice => invoice.paymentStatus === 2).length;
  }

  activateTab(index: number, tabIndex: number) {
    console.log(index, tabIndex, "index", "tabindex");

    this.activeIndex = index; // Esto muestra el detalle del elemento
    const tabSelector = `tab${tabIndex}${index}`;
    document.querySelectorAll(`[id="tab-control${index}"]`).forEach(input => {
      (input as HTMLInputElement).checked = false;
    });
    (document.getElementById(tabSelector) as HTMLInputElement).checked = true;
  }

 
  miactivateTab(localIndex: number, tabIndex: number, currentPage: number, itemsPerPage: number) {

    // Convertir índice local de la página a índice global basado en la paginación
    const globalIndex = localIndex + (currentPage - 1) * itemsPerPage;
    console.log(globalIndex, "Global Index");
    console.log(currentPage, "currentPage");
    console.log(localIndex, tabIndex, "Local Index", "Tab Index");
   

    this.activeIndex = localIndex; // Usar el índice global para manejar estados activos
   
    // Ajustar el selector para usar el índice global
    const tabSelector = `tab${globalIndex}`;
 
    // Desmarcar todos los elementos correspondientes
    const inputs = document.querySelectorAll(`[id="tab-control${localIndex}"]`);

    console.log(localIndex, tabIndex, "miLocal Index", "miTab Index");
    console.log(globalIndex, "miGlobal Index");

    inputs.forEach(input => {
      if (input) {
        (input as HTMLInputElement).checked = false;
      }
    });

    // Marcar el tab específico como activo
    const element = document.getElementById(tabSelector);
      console.log(element, "element")
    if (element) {
      (element as HTMLInputElement).checked = true;
    }
 

  }

  toggleAccordion(index: number): void {
    this.activeIndex = this.activeIndex === index ? null : index;

    setTimeout(() => {
      const radioButtons = this.el.nativeElement.querySelectorAll(`.accordion-detail.show input[type="radio"]`);
      if (radioButtons.length > 0)
        this.renderer.setProperty(radioButtons[0], 'checked', true);

    });
  }

    markAsRead(notificationId: string, index: number): void {
    const realIndex = (this.pageActualContract - 1) * this.itemsPerPage + index;

    this.notifications.putMarkAsRead(notificationId).subscribe({
      next: () => {
        const notification = this.items[realIndex].notifications.find(n => n.id === +notificationId);
        if (notification) {
          notification.read = true;
          this.notificationService.emitContractSaved();
        }
        this.miactivateTab(index, 1, this.pageActualContract, this.itemsPerPage); 
        this.cdr.detectChanges();
      },
      error: (error) => console.error('Error:', error)
    });
  }
  onPageChange(newPage: number): void {
    this.pageActualContract = newPage; 
    this.activeIndex = null;
    this.cdr.detectChanges();
  }
}
