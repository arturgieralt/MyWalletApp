import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { ClarityModule } from "@clr/angular";
import { ToastrModule } from 'ngx-toastr';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatMomentDateModule, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { CategoryListComponent } from './category-list/category-list.component';
import { AccountSummaryListComponent } from './account-summary-list/account-summary-list.component';
import { CategoryFormComponent } from './category-form/category-form.component';
import { AccountFormComponent } from './account-form/account-form.component';
import { TransactionListComponent } from './transaction-list/transaction-list.component';
import { TransactionFormComponent } from './transaction-form/transaction-form.component';
import { TransactionsPerDayChart } from './charts/transactions-per-day/transactions-per-day.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CategoryListComponent,
    AccountSummaryListComponent,
    CategoryFormComponent,
    AccountFormComponent,
    TransactionsPerDayChart,
    TransactionListComponent,
    TransactionFormComponent
  ],
  imports: [
    ToastrModule.forRoot(),
    MatDatepickerModule,
    MatMomentDateModule,
    ClarityModule,
    ReactiveFormsModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'categories', component: CategoryListComponent, pathMatch: 'full' },
      { path: 'categories/add', component: CategoryFormComponent, pathMatch: 'full' },
      { path: 'accounts', component: AccountSummaryListComponent, pathMatch: 'full' },
      { path: 'accounts/add', component: AccountFormComponent, pathMatch: 'full' },
      { path: 'accounts/:id/transactions', component: TransactionListComponent, pathMatch: 'full' },
      { path: 'transactions', component: TransactionListComponent, pathMatch: 'full' },
      { path: 'transactions/add', component: TransactionFormComponent, pathMatch: 'full' },
      { path: 'accounts/:id/transactions/add', component: TransactionFormComponent, pathMatch: 'full' }
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
