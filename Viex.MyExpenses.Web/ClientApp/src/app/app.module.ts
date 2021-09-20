import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainContainerComponent } from './views/main-container/main-container.component';
import { LoginComponent } from './views/login.component';
import { TransactionsComponent } from './views/main-container/transactions/transactions.component';
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
import { FabComponent } from './components/fab.component';
import { ConfirmModalComponent } from './components/modals/ConfirmModal/confirm-modal.component';
import { LoadingModalComponent } from './components/modals/LoadingModal/loading-modal.component';
import { TransactionDetailsComponent } from './views/main-container/transactions/transaction-details.component';
import { TextFieldComponent } from './components/text-field.component';
import { SelectComponent } from './components/select.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { ToastComponent } from './components/Toast/toast.component';
import { DatePickerComponent } from './components/date-picker.component';
import { HttpInterceptorService } from './services/http-interceptor.service';
import { UsersPageComponent } from './views/main-container/UsersPage/users-page.component';
import { FlexComponent } from './components/flex.component';
import { UserListComponent } from './components/UserList/user-list.component';
import { UserItemComponent } from './components/UserList/user-item.component';
import { ActionDrawerComponent } from './components/ActionDrawer/action-drawer.component';
import { DescriptorsPageComponent } from './views/main-container/DescriptorsPage/descriptors-page.component';
import { ListItemComponent } from './components/ListItem/list-item.component';
import { CollapseContainerComponent } from './components/CollapseContainer/collapse-container.component';

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
    BottomDrawerComponent,
    FabComponent,
    ConfirmModalComponent,
    LoadingModalComponent,
    TransactionDetailsComponent,
    TextFieldComponent,
    SelectComponent,
    ToastComponent,
    DatePickerComponent,
    UsersPageComponent,
    FlexComponent,
    UserListComponent,
    UserItemComponent,
    ActionDrawerComponent,
    DescriptorsPageComponent,
    ListItemComponent,
    CollapseContainerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpInterceptorService,
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
