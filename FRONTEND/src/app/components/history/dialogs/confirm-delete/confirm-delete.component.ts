import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { CalculatorAtService } from 'src/app/services/calculators/calculator-at.service';
import { CalculatorBmiService } from 'src/app/services/calculators/calculator-bmi.service';
import { CalculatorColService } from 'src/app/services/calculators/calculator-col.service';
import { CalculatorRmbService } from 'src/app/services/calculators/calculator-rmb.service';
import { CalculatorTrService } from 'src/app/services/calculators/calculator-tr.service';

@Component({
  selector: 'app-confirm-delete',
  templateUrl: './confirm-delete.component.html',
  styleUrls: ['./confirm-delete.component.scss']
})
export class ConfirmDeleteComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<ConfirmDeleteComponent>,
    private rmbService: CalculatorRmbService,
    private bmiService: CalculatorBmiService,
    private atService: CalculatorAtService,
    private triService: CalculatorTrService,
    private colService: CalculatorColService,
    private toastr: ToastrService
  ) { }

  delete(selectedTable: string): void {
    if (selectedTable === 'bmi') {
      this.bmiService.deleteUserBMIs().subscribe({
        next: () => {
          this.toastr.success('History cleaned')
          this.dialogRef.close('success')
        }
      });
    } else if (selectedTable === 'rmb') {
      this.rmbService.deleteUserRMBs().subscribe({
        next: () => {
          this.toastr.success('History cleaned')
          this.dialogRef.close('success')
        }
      });
    } else if (selectedTable === 'blood pressure') {
      this.atService.deleteUserATs().subscribe({
        next: () => {
          this.toastr.success('History cleaned')
          this.dialogRef.close('success')
        }
      });
    } else if (selectedTable === 'triglycerides') {
      this.triService.deleteUserTRIs().subscribe({
        next: () => {
          this.toastr.success('History cleaned')
          this.dialogRef.close('success')
        }
      });
    } else if (selectedTable === 'cholesterol') {
      this.colService.deleteUserCOLs().subscribe({
        next: () => {
          this.toastr.success('History cleaned')
          this.dialogRef.close('success')
        }
      })
    }
  }
}
