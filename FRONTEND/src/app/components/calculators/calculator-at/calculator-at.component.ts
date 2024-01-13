import { Component } from '@angular/core';
import { CalculatorAtService } from 'src/app/services/calculators/calculator-at.service';

@Component({
  selector: 'app-calculator-at',
  templateUrl: './calculator-at.component.html',
  styleUrls: ['./calculator-at.component.scss']
})
export class CalculatorAtComponent {
  model: any = {};
  result = '';

  constructor(private calculatorService: CalculatorAtService) { }

  calculateAT(): void {
    this.calculatorService.calculateAT(this.model).subscribe({
      next: response => this.result = response.category
    })
  }
}
