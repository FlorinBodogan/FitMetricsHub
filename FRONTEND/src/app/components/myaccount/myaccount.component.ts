import { Component } from '@angular/core';
import { CalculatorAtService } from 'src/app/services/calculators/calculator-at.service';
import { CalculatorBmiService } from 'src/app/services/calculators/calculator-bmi.service';
import { CalculatorColService } from 'src/app/services/calculators/calculator-col.service';
import { CalculatorTrService } from 'src/app/services/calculators/calculator-tr.service';

@Component({
  selector: 'app-myaccount',
  templateUrl: './myaccount.component.html',
  styleUrls: ['./myaccount.component.scss']
})
export class MyaccountComponent {
  selectedComponent: string = 'BMI';

  lastUserATCategory?: string;
  lastUserCOLCategory?: string;
  lastUserTRCategory?: string;

  constructor(
    private atService: CalculatorAtService,
    private colService: CalculatorColService,
    private triService: CalculatorTrService
  ) { }

  selectComponent(component: string) {
    this.selectedComponent = component;
  };

  ngOnInit(): void {
    this.getUserATResults();
    this.getUserCOLResults();
    this.getUserTRIResults();
  }

  getUserATResults(): void {
    this.atService.getATResultsForUser().subscribe({
      next: response => {
        this.lastUserATCategory = response[response.length - 1].category;
      }
    });
  }

  getUserCOLResults(): void {
    this.colService.getCOLResultsForUser().subscribe({
      next: response => {
        this.lastUserCOLCategory = response[response.length - 1].category;
      }
    });
  }

  getUserTRIResults(): void {
    this.triService.getTRIResultsForUser().subscribe({
      next: response => {
        this.lastUserTRCategory = response[response.length - 1].category;
      }
    });
  }
}
