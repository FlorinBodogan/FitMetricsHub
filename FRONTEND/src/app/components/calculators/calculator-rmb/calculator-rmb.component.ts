import { Component } from '@angular/core';
import { CalculatorRmbService } from 'src/app/services/calculators/calculator-rmb.service';

@Component({
  selector: 'app-calculator-rmb',
  templateUrl: './calculator-rmb.component.html',
  styleUrls: ['./calculator-rmb.component.scss']
})
export class CalculatorRmbComponent {
  model: any = {};
  error = '';
  result = '';

  constructor(private calculatorService: CalculatorRmbService) { }

  calculateRmb(): void {
    this.calculatorService.calculateRmb(this.model).subscribe({
      next: response => {
        this.result = response.result
      },
      error: err => {
        this.error = err.error
      }
    })
  }
}
