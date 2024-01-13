import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { AboutComponent } from './components/about/about.component';
import { authGuard } from './services/guards/auth.guard';
import { CalculatorBmiComponent } from './components/calculators/calculator-bmi/calculator-bmi.component';
import { CalculatorRmbComponent } from './components/calculators/calculator-rmb/calculator-rmb.component';
import { CalculatorAtComponent } from './components/calculators/calculator-at/calculator-at.component';
import { CalculatorColComponent } from './components/calculators/calculator-col/calculator-col.component';
import { CalculatorTriComponent } from './components/calculators/calculator-tri/calculator-tri.component';
import { MyaccountComponent } from './components/myaccount/myaccount.component';
import { StatisticsComponent } from './components/statistics/statistics.component';
import { HistoryComponent } from './components/history/history.component';
import { nonAuthGuard } from './services/guards/non-auth.service';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      { path: 'calculator-bmi', component: CalculatorBmiComponent },
      { path: 'calculator-rmb', component: CalculatorRmbComponent },
      { path: 'calculator-at', component: CalculatorAtComponent },
      { path: 'calculator-col', component: CalculatorColComponent },
      { path: 'calculator-tri', component: CalculatorTriComponent },
      { path: 'myAccount', component: MyaccountComponent },
      { path: 'statistics', component: StatisticsComponent },
      { path: 'history', component: HistoryComponent },
    ]
  },
  { path: 'register', component: RegisterComponent, canActivate: [nonAuthGuard] },
  { path: 'login', component: LoginComponent, canActivate: [nonAuthGuard] },
  { path: 'about', component: AboutComponent },
  { path: '**', component: HomeComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
