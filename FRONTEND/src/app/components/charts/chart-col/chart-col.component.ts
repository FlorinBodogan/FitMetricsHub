import { Component } from '@angular/core';
import { CalculatorColService } from 'src/app/services/calculators/calculator-col.service';
import { Chart } from 'chart.js';

@Component({
  selector: 'app-chart-col',
  templateUrl: './chart-col.component.html',
  styleUrls: ['./chart-col.component.scss']
})
export class ChartColComponent {
  chart: any;

  resultsArray: any[] = [];
  datesArray: any[] = [];

  constructor(private calculatorColService: CalculatorColService) { }

  ngOnInit(): void {
    this.getColResults();
  };

  createChart() {
    this.chart = new Chart('chart', {
      type: 'pie',
      data: {
        labels: ['Normal', 'BorderLine High', 'High'],
        datasets: [
          {
            label: 'Cholesterol',
            data: this.resultsArray,
            backgroundColor: ['#f2ff00d5', '#ff5733', '#4caf50'],
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
              color: '#222'
            }
          },
          y: {
            beginAtZero: true,
            ticks: {
              color: '#222',
            }
          }
        },
        plugins: {
          legend: {
            labels: {
              color: '#222'
            }
          },
          title: {
            display: true,
            text: 'Cholesterol Results',
            color: '#222'
          }
        }
      }
    });
  }

  getColResults(): void {
    this.calculatorColService.getColResults().subscribe({
      next: response => {
        this.resultsArray = Object.values(response);
        this.createChart();
      },
      error: error => {
        console.error('Error fetching user results: ', error);
      }
    });
  };
}
