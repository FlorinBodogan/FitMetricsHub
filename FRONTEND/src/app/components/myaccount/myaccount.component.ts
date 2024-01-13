import { Component } from '@angular/core';
import { CalculatorBmiService } from 'src/app/services/calculators/calculator-bmi.service';

@Component({
  selector: 'app-myaccount',
  templateUrl: './myaccount.component.html',
  styleUrls: ['./myaccount.component.scss']
})
export class MyaccountComponent {
  selectedComponent: string = 'BMI';

  selectComponent(component: string) {
    this.selectedComponent = component;
  }
}
