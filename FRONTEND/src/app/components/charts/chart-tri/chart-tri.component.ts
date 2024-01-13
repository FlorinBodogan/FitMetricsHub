import { Component } from '@angular/core';
import { Chart } from 'chart.js';
import { CalculatorTrService } from 'src/app/services/calculators/calculator-tr.service';

@Component({
  selector: 'app-chart-tri',
  templateUrl: './chart-tri.component.html',
  styleUrls: ['./chart-tri.component.scss']
})
export class ChartTriComponent {
  chart: any;

  resultsArray: any[] = [];
  datesArray: any[] = [];

  constructor(private calculatorTriService: CalculatorTrService) { }

  ngOnInit(): void {
    this.getTriResults();
  };

  createChart() {
    this.chart = new Chart('chartTri', {
      type: 'pie',
      data: {
        labels: ['Normal', 'BorderLine High', 'High', 'Very High'],
        datasets: [
          {
            label: 'Triglycerides',
            data: this.resultsArray,
            backgroundColor: ['#f2ff00d5', 'blue', '#4caf50', 'red'],
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
            text: 'Triglycerides Results',
            color: '#222'
          }
        }
      }
    });
  }

  getTriResults(): void {
    this.calculatorTriService.getTriResults().subscribe({
      next: response => {
        console.log(response);
        this.resultsArray = Object.values(response);
        this.createChart();
      },
      error: error => {
        console.error('Error fetching user results: ', error);
      }
    });
  };
}
