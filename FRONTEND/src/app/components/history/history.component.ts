import { Component, OnInit } from '@angular/core';
import { CalculatorAtService } from 'src/app/services/calculators/calculator-at.service';
import { CalculatorBmiService } from 'src/app/services/calculators/calculator-bmi.service';
import { CalculatorColService } from 'src/app/services/calculators/calculator-col.service';
import { CalculatorRmbService } from 'src/app/services/calculators/calculator-rmb.service';
import { CalculatorTrService } from 'src/app/services/calculators/calculator-tr.service';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.scss']
})
export class HistoryComponent implements OnInit {
  selectedTable: string = 'bmi';

  userRMB: any;
  userBMI: any;
  userTR: any;
  userCOL: any;
  userAT: any;

  constructor(
    private bmiService: CalculatorBmiService,
    private rmbService: CalculatorRmbService,
    private atService: CalculatorAtService,
    private colService: CalculatorColService,
    private triService: CalculatorTrService
  ) { }

  ngOnInit(): void {
    this.getUserATResults();
    this.getUserRMBResults();
    this.getUserBMIResults();
    this.getUserCOLResults();
    this.getUserTRIResults();
  }

  getUserATResults(): void {
    this.atService.getATResultsForUser().subscribe({
      next: response => {
        this.userAT = response
      }
    })
  };

  getUserBMIResults(): void {
    this.bmiService.getBMIResultsForUser().subscribe({
      next: response => {
        this.userBMI = response
        console.log(this.userBMI)
      }
    })
  };

  getUserRMBResults(): void {
    this.rmbService.getRMBResultsForUser().subscribe({
      next: response => {
        this.userRMB = response
      }
    })
  };

  getUserCOLResults(): void {
    this.colService.getCOLResultsForUser().subscribe({
      next: response => {
        this.userCOL = response
      }
    })
  };

  getUserTRIResults(): void {
    this.triService.getTRIResultsForUser().subscribe({
      next: response => {
        this.userTR = response
      }
    })
  };
}
