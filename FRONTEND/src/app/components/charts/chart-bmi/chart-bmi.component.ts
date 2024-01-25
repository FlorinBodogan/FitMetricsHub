import { Component, OnInit } from '@angular/core';
import { CalculatorBmiService } from 'src/app/services/calculators/calculator-bmi.service';
import { DatePipe } from '@angular/common';
import { Chart, registerables } from 'chart.js';
Chart.register(...registerables);

@Component({
  selector: 'app-chart-bmi',
  templateUrl: './chart-bmi.component.html',
  styleUrls: ['./chart-bmi.component.scss'],
  providers: [DatePipe]
})
export class ChartBmiComponent implements OnInit {
  chart: any;

  resultsArray: any[] = [];
  datesArray: any[] = [];

  constructor(private calculatorBmiService: CalculatorBmiService, private datePipe: DatePipe) { }

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
            backgroundColor: '#04ff00e1',
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
  };

  getUserResults(): void {
    this.calculatorBmiService.getUserResults().subscribe({
      next: response => {
        this.resultsArray = response.map((result: any) => result.result);
        this.datesArray = response.map((result: any) => this.formatDate(result.created));

        this.createChart();
      },
      error: error => {
        console.error('Error fetching user results: ', error);
      }
    });
  };

  private formatDate(date: string): string {
    const formattedDate = this.datePipe.transform(date, 'dd MM yyyy');
    return formattedDate || '';
  };
}
