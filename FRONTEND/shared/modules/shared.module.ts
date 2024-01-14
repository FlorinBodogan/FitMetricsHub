import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from "ngx-spinner";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
    declarations: [

    ],
    imports: [
        CommonModule,
        BrowserAnimationsModule,
        MatIconModule,
        FormsModule,
        ReactiveFormsModule,
        ToastrModule.forRoot({
            positionClass: 'toast-top-right'
        }),
        NgxSpinnerModule.forRoot({
            type: 'ball-clip-rotate'
        }),
    ],
    exports: [
        BrowserAnimationsModule,
        MatIconModule,
        FormsModule,
        ReactiveFormsModule,
        ToastrModule,
        NgxSpinnerModule
    ]
})
export class SharedModule { }