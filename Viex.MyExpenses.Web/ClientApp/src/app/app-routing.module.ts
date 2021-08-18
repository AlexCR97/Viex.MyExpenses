import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './views/login.component';
import { MainContainerComponent } from './views/main-container/main-container.component';
import { HomeComponent } from './views/main-container/home.component';
import { SettingsComponent } from './views/main-container/settings.component';
import { TransactionsComponent } from './views/main-container/transactions/transactions.component';
import { TransactionDetailsComponent } from './views/main-container/transactions/transaction-details.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'app',
    component: MainContainerComponent,
    children: [
      {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full',
      },
      {
        path: 'home',
        component: HomeComponent,
      },
      {
        path: 'transactions',
        component: TransactionsComponent,
      },
      {
        path: 'transactions/new',
        component: TransactionDetailsComponent,
      },
      {
        path: 'transactions/:id',
        component: TransactionDetailsComponent,
      },
      {
        path: 'settings',
        component: SettingsComponent,
      },
    ],
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }