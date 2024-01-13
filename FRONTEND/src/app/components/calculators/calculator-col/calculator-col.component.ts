import { Component } from '@angular/core';
import { CalculatorColService } from 'src/app/services/calculators/calculator-col.service';

@Component({
  selector: 'app-calculator-col',
  templateUrl: './calculator-col.component.html',
  styleUrls: ['./calculator-col.component.scss']
})
export class CalculatorColComponent {
  model: any = {};

  result = '';

  constructor(private calculatorService: CalculatorColService) { }

  calculateCOL(): void {
    this.calculatorService.calculateCOL(this.model).subscribe({
      next: response => this.result = response.category
    })
  }
}
