import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { SharedModule } from 'shared/modules/shared.module';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { JwtInterceptor } from './services/interceptors/jwt.interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { HomeComponent } from './components/home/home.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AboutComponent } from './components/about/about.component';
import { CalculatorBmiComponent } from './components/calculators/calculator-bmi/calculator-bmi.component';
import { CalculatorRmbComponent } from './components/calculators/calculator-rmb/calculator-rmb.component';
import { CalculatorAtComponent } from './components/calculators/calculator-at/calculator-at.component';
import { CalculatorColComponent } from './components/calculators/calculator-col/calculator-col.component';
import { CalculatorTriComponent } from './components/calculators/calculator-tri/calculator-tri.component';
import { MyaccountComponent } from './components/myaccount/myaccount.component';
import { ChartBmiComponent } from './components/charts/chart-bmi/chart-bmi.component';
import { ChartColComponent } from './components/charts/chart-col/chart-col.component';
import { ChartTriComponent } from './components/charts/chart-tri/chart-tri.component';
import { ChartAtComponent } from './components/charts/chart-at/chart-at.component';
import { StatisticsComponent } from './components/statistics/statistics.component';
import { ChartRmbComponent } from './components/charts/chart-rmb/chart-rmb.component';
import { HistoryComponent } from './components/history/history.component';
import { LoadingEffectInterceptor } from './services/interceptors/loading-effect.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    LoginComponent,
    RegisterComponent,
    AboutComponent,
    CalculatorBmiComponent,
    CalculatorRmbComponent,
    CalculatorAtComponent,
    CalculatorColComponent,
    CalculatorTriComponent,
    MyaccountComponent,
    ChartBmiComponent,
    ChartColComponent,
    ChartTriComponent,
    ChartAtComponent,
    StatisticsComponent,
    ChartRmbComponent,
    HistoryComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    SharedModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingEffectInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
