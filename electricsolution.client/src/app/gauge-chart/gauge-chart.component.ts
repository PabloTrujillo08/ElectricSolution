import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import ZingChart from 'zingchart';
import { scales } from './meters.scales/scales';
import { scaleRules } from './meters.scales/scale-rules';
import { MetersService } from '../services/meters.service';
import { EnergyControl } from '../interfaces/energy-control.interface';
  
@Component({
  selector: 'app-gauge-chart',
  templateUrl: './gauge-chart.component.html',
  styleUrls: ['./gauge-chart.component.css']
})
export class GaugeChartComponent implements OnInit, OnDestroy {
  private subscription = new Subscription();

  @Input() meterId!: string;
  @Input() title!: string;
  @Input() meterScale: number[] = [200, -6000, 6000];
  @Input() scaleKey: string = 'POWER_IO';
  @Input() units!: string;

  currentProgressValuePanelEste: number = 0;
  currentProgressValuePanelOeste: number = 0;

  subMeterScaleLabel!: number[];
  currentScale: string[] = [];
  currentRules: object[] = [];

  constructor(private metersService: MetersService) { }

  ngOnInit(): void {
    this.updateScale();
    this.scheduleMeterUpdate();
    this.subscription.add(this.metersService.getMeters().subscribe((data) => {
      if (data && data.length > 0) {
        this.renderChart(data);
      }
    }));
  }

  private updateScale(): void {
    this.subMeterScaleLabel = this.meterScale.slice(1);
    this.currentScale = scales[this.scaleKey] || scales['POWER_IO'];
    this.currentRules = scaleRules[this.scaleKey] || scaleRules['POWER_IO'];
  }

  scheduleMeterUpdate(): void {
    this.metersService.updateMeters();
    const updateInterval = setInterval(() => {
      this.metersService.updateMeters();
    }, 5000);

    this.subscription.add({
      unsubscribe: () => clearInterval(updateInterval)
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
  renderChart(meters: EnergyControl[]) {

    if (meters.length > 0 && this.meterId) {
      const fieldValue = meters[meters.length - 1][this.meterId as keyof EnergyControl] as number;
      const progressPercentage = (fieldValue / this.meterScale[2]) * 100;
      if (this.meterId === 'dc1_pw') {
        this.currentProgressValuePanelOeste = progressPercentage;
      } else if (this.meterId === 'dc2_pw') {
        this.currentProgressValuePanelEste = progressPercentage;
      }
      // Configurar la serie del gráfico con el valor del campo específico
      const seriesData = [
        {
          values: [Number(fieldValue.toFixed(2))],
          backgroundColor: 'black',
          indicator: [10, 1, 50, 50, 0],
          size: '100%',

        }
      ];
      // Configuración del gráfico
      const myConfig = {
        type: 'gauge',
        globals: {
          fontSize: 25
        }, title: {
          text: this.title,
          fontSize: 15,
         adjustLayout: true
        },
        plotarea: {
          marginTop: -30
        },
        plot: {
          size: '100%',
          placement: 'center',
          valueBox: {
            placement: 'center',
            text: '%v',
            fontSize: 20,
            rules: [{
              rule: '%v < 0',
              text: '<br><br>%v' + this.units,
              fontColor: 'red'
            },
            {
              rule: '%v == 0',
              text: '<br><br>%v' + this.units,
              fontColor: 'black'
            },
            {
              rule: '%v > 0',
              text: '<br><br>%v' + this.units,
              fontColor: '#12a14b'
            },
            ],

          }
        },
        tooltip: {
          borderRadius: 1,
          backgroundColor: 'white', 
          borderColor: 'black', 
          borderWidth: 1, 
          text: '%v', 
          fontSize: 12, 
          fontColor: '#333',
        },
        scaleR: {
          aperture: 180,
          //values: "0:4000:400",
          minValue: this.meterScale[1],
          maxValue: this.meterScale[2],
          step: this.meterScale[0],
          center: {
            visible: true,
            size: 3, 
            backgroundColor: 'white',
          },
          tick: {
            visible: true,
            color: 'black',
            'line-color': 'white',
          },
          item: {
            offsetR: 5,
            color: 'black',
            fontSize: 12,
            rules: [{
              rule: '%i == 9', 
              offsetX: 0
            }]
          },
          labels: this.currentScale,
          
          ring: {
            size: 20,
            rules: this.currentRules
          }
        },
        refresh: {
          type: 'feed',
          transport: 'js',
          url: 'feed()',
          interval: 1500,
          resetTimeout: 1000
        },
        series: seriesData,
        animation: {
          effect: 'ANIMATION_EXPAND_VERTICAL',
          method: 'ANIMATION_BACK_EASE_OUT',
          sequence: 'null',
          speed: 900,
        },
      };

      // Renderizar el gráfico
      ZingChart.render({
        id: this.meterId,
        data: myConfig,
        height: '100%',
        width: '100%' 
      });

    } else {
      console.warn('No se especificó el campo a mostrar o no hay datos disponibles.');
    }
  }
}
