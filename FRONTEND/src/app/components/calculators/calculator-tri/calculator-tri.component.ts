import { Component } from '@angular/core';
import { CalculatorTrService } from 'src/app/services/calculators/calculator-tr.service';

@Component({
  selector: 'app-calculator-tri',
  templateUrl: './calculator-tri.component.html',
  styleUrls: ['./calculator-tri.component.scss']
})
export class CalculatorTriComponent {
  model: any = {};

  result = '';

  constructor(private calculatorService: CalculatorTrService) { }

  calculateTri(): void {
    this.calculatorService.calculateTri(this.model).subscribe({
      next: response => this.result = response.category
    })
  }
}
