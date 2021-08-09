import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainContainerComponent } from './views/main-container/main-container.component';
import { LoginComponent } from './views/login.component';
import { TransactionsComponent } from './views/main-container/transactions.component';
import { HomeComponent } from './views/main-container/home.component';
import { SettingsComponent } from './views/main-container/settings.component';
import { HeaderComponent } from './components/header.component';
import { DrawerComponent } from './components/SideNavigator/side-navigator.component';
import { UserDropdownComponent } from './components/user-dropdown.component';
import { DatePagerComponent } from './components/date-pager.component';
import { TransactionListComponent } from './components/transaction-list/transaction-list.component';
import { TransactionItemComponent } from './components/transaction-list/transaction-item.component';
import { IconButtonComponent } from './components/icon-button.component';
import { BottomDrawerComponent } from './components/bottom-drawer.component';

@NgModule({
  declarations: [
    AppComponent,
    MainContainerComponent,
    LoginComponent,
    TransactionsComponent,
    HomeComponent,
    SettingsComponent,
    HeaderComponent,
    DrawerComponent,
    UserDropdownComponent,
    DatePagerComponent,
    TransactionListComponent,
    TransactionItemComponent,
    IconButtonComponent,
    BottomDrawerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
