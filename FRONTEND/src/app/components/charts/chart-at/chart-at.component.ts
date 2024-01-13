import { Component } from '@angular/core';
import { Chart } from 'chart.js';
import { CalculatorAtService } from 'src/app/services/calculators/calculator-at.service';

@Component({
  selector: 'app-chart-at',
  templateUrl: './chart-at.component.html',
  styleUrls: ['./chart-at.component.scss']
})
export class ChartAtComponent {
  chart: any;

  resultsArray: any[] = [];
  datesArray: any[] = [];

  constructor(private calculatorATService: CalculatorAtService) { }

  ngOnInit(): void {
    this.getColResults();
  };

  createChart() {
    this.chart = new Chart('chartAT', {
      type: 'pie',
      data: {
        labels: ['Optimal ', 'Normal', 'Elevated', 'Hypertension Stage 1', 'Hypertension Stage 2', 'Hypertension Stage 3', 'Isolated Systolic Hypertension', 'Undetermined'],
        datasets: [
          {
            label: 'Arterial Tension',
            data: this.resultsArray,
            backgroundColor: ['#f2ff00d5', '#ff5733', '#4caf50', 'red', 'brown', 'blue', 'purple', 'black'],
          },
        ],
      },
      options: {
        aspectRatio: 2.5,
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
            text: 'Arterial Tension Results',
            color: '#222'
          }
        }
      }
    });
  }

  getColResults(): void {
    this.calculatorATService.getATResults().subscribe({
      next: response => {
        this.resultsArray = Object.values(response);
        this.createChart();
      },
      error: error => {
        console.error('Error fetching user results: ', error);
      }
    });
  }
}
