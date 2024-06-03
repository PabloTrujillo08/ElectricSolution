import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Subscription } from 'rxjs';
import ZingChart from 'zingchart';
import { ReeEsiosResponse, ReeEsiosSalesResponse } from '../interfaces/ReeEsios.interface';

@Component({
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
  styleUrls: ['./bar-chart.component.css']
})
export class BarChartComponent implements OnInit, OnDestroy {

  private meters$ = new BehaviorSubject<number[]>([]);
  private metersSale$ = new BehaviorSubject<number[]>([]);
  private subscription = new Subscription();
  @Input() _title!: string;

  @Input() _sale: boolean =true;
  @Input() _buy: boolean =false;

  @Input() customValues: number[] = [];

  constructor(private http: HttpClient) { }
   
 
  ngOnInit() {



    this.scheduleMeterUpdate();
    this.subscription.add(this.meters$.subscribe(() => {
      this.renderChart();

    }));

    this.subscription.add(this.metersSale$.subscribe(() => {
      this.renderChart();

    }));
   
  }


  scheduleMeterUpdate() {

    // llamada a la API para obtener los datos de compra
    this.updateMetersBuy();

    const updateInterval =  setInterval(() => {
      this.updateMetersBuy();
    }, 30000);

    this.subscription.add({
      unsubscribe: () => clearInterval(updateInterval)
    });

    // ---

    // llamada a la API para obtener los datos de venta
    this.updateMetersSale();

    const updateIntervalSale =  setInterval(() => {
      this.updateMetersSale();
    }, 30000);
    this.subscription.add({
      unsubscribe: () => clearInterval(updateIntervalSale)
    });

   // ---
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  updateMetersBuy() {
    this.http.get<ReeEsiosResponse>('/api/ReeEsiosIndicators/1001').subscribe({
      next: (result) => {
        const filteredValues = result.indicator.values.filter(value => value.geo_id === 8741);
        const seriesValues = filteredValues.map(value => parseFloat((value.value / 1000).toFixed(2)));
        this.meters$.next(seriesValues); 


      },
      error: (error) => {
        console.error(error);
      },
      complete: () => {
        console.log('Completado');
      }
    });
  }

  updateMetersSale() {
    this.http.get<ReeEsiosSalesResponse>('/api/ReeEsiosIndicators/1739').subscribe({
      next: (results) => {
       // console.log("results",results.indicator)
        const seriesValues = results.indicator.values.map(result => ({
          value: parseFloat((result.value / 100).toFixed(2)), // Ajusta según necesites
        }));

        // Suponiendo que tienes otro BehaviorSubject para los valores de venta
       this.metersSale$.next(seriesValues.map(item => item.value));
      },
      error: (error) => {
        console.error(error);
      },
      complete: () => {
        console.log('Datos de venta completados');
      }
    });
  }





  renderChart() {
    const compraKWh = this.meters$.value;
    const ventaKWh = this.metersSale$.value;

    const series = this.customValues.length > 0
      ? [{ values: this.customValues, "background-color": "#12a14b", text: "Precio Calambre Energy kWh" }]
      : [];

    if (this._sale) 
      series.push({ "values": compraKWh, "background-color": "#0d6efd", text: "Precio Compra kWh" });
    

    if (this._buy)
      series.push({ "values": ventaKWh, "background-color": "#12a14b", text: "Precio Venta Excedentes" });
   


    const myConfig = {
      "graphset": [{
        "type": "bar",
        "background-color": "white",
        "title": {
          "text": this._title,
          "font-color": "#7E7E7E",
          "backgroundColor": "none",
          "font-size": "22px",
          "alpha": 1,
          "adjust-layout": true,
        },
        "plotarea": {
          "margin": "dynamic"
        },
        "legend": {
          "layout": "x3",
          "overflow": "page",
          "alpha": 0.05,
          "shadow": false,
          "align": "center",
          "adjust-layout": true,
          "marker": {
            "type": "circle",
            "border-color": "none",
            "size": "10px"
          },
          "border-width": 0,
          "maxItems": 3,
          "toggle-action": "hide",
          "pageOn": {
            "backgroundColor": "#000",
            "size": "10px",
            "alpha": 0.65
          },
          "pageOff": {
            "backgroundColor": "#7E7E7E",
            "size": "10px",
            "alpha": 0.65
          },
          "pageStatus": {
            "color": "black"
          }
        },
        "plot": {
          groupBars: false,
          "bars-space-left": 0.15,
          showZero: false,
          'border-radius': "0px",
          barWidth: 20,
          "bars-space-right": 0.15,
          "animation": {
            "effect": "ANIMATION_SLIDE_BOTTOM",
            "sequence": 1,
            "speed": 600,
            "delay": 0
          }
        },
        "scale-y": {
          "line-color": "#7E7E7E",
          "item": {
            "font-color": "#7e7e7e"
          },
          "prefix": "€ ",
          "values": "0:0.250:0.025",
          "guide": {
            "visible": true
          },

        },
        "scaleX": {
          "values": [
            "00h", "01h", "02h", "03h", "04h", "05h", "06h", "07h", "08h", "09h", "10h", "11h", "12h", "13h", "14h", "15h", "16h", "17h", "18h", "19h", "20h", "21h", "22h", "23h"
          ],
          "placement": "default",
          "tick": {
            "size": 8,
            "placement": "cross"
          },
          "itemsOverlap": true,
          "item": {
            "offsetY": 0
          }
        },

        "crosshair-x": {
          "line-width": "100%",
          "alpha": 0.18,
          "plot-label": {
            "header-text": "%k"
          }
        },
        "series": series
      }]
    };

    ZingChart.render({
      id: 'myChart',
      data: myConfig,
      height: '100%',
      width: '100%'
    });


  }
}
