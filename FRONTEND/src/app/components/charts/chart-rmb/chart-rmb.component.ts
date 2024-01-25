import { DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { Chart } from 'chart.js';
import { CalculatorRmbService } from 'src/app/services/calculators/calculator-rmb.service';

@Component({
  selector: 'app-chart-rmb',
  templateUrl: './chart-rmb.component.html',
  styleUrls: ['./chart-rmb.component.scss'],
  providers: [DatePipe]
})
export class ChartRmbComponent {
  chart: any;

  resultsArray: any[] = [];
  datesArray: any[] = [];

  constructor(private calculatorRmbService: CalculatorRmbService, private datePipe: DatePipe) { }

  ngOnInit(): void {
    this.getUserResults();
  };

  createChart() {
    this.chart = new Chart('chart', {
      type: 'bar',
      data: {
        labels: this.datesArray,
        datasets: [
          {
            label: 'Result',
            data: this.resultsArray,
            backgroundColor: '#f2ff00d5',
          },
        ],
      },
      options: {
        aspectRatio: 2.2,
        maintainAspectRatio: false,
        scales: {
          x: {
            beginAtZero: true,
            ticks: {
              color: '#ddd'
            }
          },
          y: {
            beginAtZero: true,
            ticks: {
              color: '#ddd',
            }
          }
        },
        plugins: {
          legend: {
            labels: {
              color: '#ddd'
            }
          },
          title: {
            display: true,
            text: 'BMI Results',
            color: 'white'
          }
        }
      }
    });
  }

  getUserResults(): void {
    this.calculatorRmbService.getUserResults().subscribe({
      next: response => {
        this.resultsArray = response.map((result: any) => Math.floor(result.result));
        this.datesArray = response.map((result: any) => this.formatDate(result.created));

        this.createChart();
      },
      error: error => {
        console.error('Error fetching user results: ', error);
      }
    });
  }

  private formatDate(date: string): string {
    const formattedDate = this.datePipe.transform(date, 'dd MM yyyy');
    return formattedDate || '';
  }
}
