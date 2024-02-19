import { Component } from '@angular/core';
import { CalculatorBmiService } from 'src/app/services/calculators/calculator-bmi.service';

@Component({
  selector: 'app-calculator-bmi',
  templateUrl: './calculator-bmi.component.html',
  styleUrls: ['./calculator-bmi.component.scss']
})
export class CalculatorBmiComponent {
  model: any = {};
  result = '';
  error = '';

  constructor(private calculatorService: CalculatorBmiService) { }

  calculateBmi(): void {
    this.calculatorService.calculateBmi(this.model).subscribe({
      next: response => {
        this.result = response.result
      },
      error: err => {
        this.error = err.error
      }
    })
  }
}
