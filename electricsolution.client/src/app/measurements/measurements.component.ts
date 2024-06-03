import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { HttpClient } from '@angular/common/http';
import { TooltipMeasurements } from '../interfaces/tooltip-measurements-data.interface';
import { MetersService } from '../services/meters.service';
import { EnergyControl } from '../interfaces/energy-control.interface';

   // eslint-disable-next-line @typescript-eslint/no-explicit-any
   declare let bootstrap: any;
@Component({
  selector: 'app-measurements',
  templateUrl: './measurements.component.html',
  styleUrls: ['./measurements.component.css']
})
export class MeasurementsComponent implements OnInit {
  isAuthenticated = false;
  @Input() userName: string | null = null;

  tooltipTexts: TooltipMeasurements = {
    consumoRedElectrica: '',
    temperaturaInversor: '',
    consumoEnergiaHogar: '',
    produccionEnergiaEste: '',
    produccionEnergiaOeste: '',
    potenciaGenerada: '',
    tensionRedElectrica: '',
  };
  meters: EnergyControl[] = [];

  constructor(private authService: AuthService, private router: Router, private http: HttpClient, private metersService: MetersService) { }

  ngOnInit() {
    this.checkLoginStatus();
    this.loadTooltipData();
    this.loadMeters(); 
    setTimeout(() => {
      this.initializeTooltips();
    }, 100);
  }

  trackById(index: number, meter: EnergyControl): number {
    return meter.ID;  // Asume que `ID` es único para cada `meter`
  }

  loadMeters() {
    this.metersService.getMeters().subscribe(meters => {
      this.meters = meters;
    });
  }
  checkLoginStatus() {
    this.authService.isAuthenticated().subscribe(isAuth => {
      this.isAuthenticated = isAuth;
      console.log('User is authenticated:', this.isAuthenticated)
      if (!this.isAuthenticated) {
        console.log('User is not authenticated')
        // Redirigir al usuario al login si no está autenticado
        this.router.navigate(['/login']);
      }
    });
  }
  loadTooltipData() {
    this.http.get<TooltipMeasurements>('assets/json/tooltip-data.json').subscribe(data => {
      this.tooltipTexts = data;
    
    });
  }
  initializeTooltips() {
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));

    tooltipTriggerList.forEach(tooltipTriggerEl => {

      const tooltipInstance = bootstrap.Tooltip.getInstance(tooltipTriggerEl);
      if (tooltipInstance) 
        tooltipInstance.dispose();

      new bootstrap.Tooltip(tooltipTriggerEl);
    });
  }
  
}
