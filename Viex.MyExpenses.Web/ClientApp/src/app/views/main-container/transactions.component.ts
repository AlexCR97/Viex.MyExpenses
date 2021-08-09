import { Component, OnInit } from '@angular/core';
import { TransactionType } from 'src/app/models/TransactionTypeDescriptor';
import { WeeklyTransactionsEntry } from 'src/app/models/WeeklyTransactionsEntry';
import arrays from 'src/app/utils/arrays';
import dates from 'src/app/utils/dates';
import { notNullNorEmpty } from 'src/app/utils/validators';

const template = /*html*/`
<div style="height: 50px">
  <app-header title="Transactions"></app-header>
</div>

<div class="py-4">

  <div class="mb-4">
    <app-date-pager></app-date-pager>
  </div>
  
  <div class="accordion">

    <div *ngFor="let item of weeklyTransactions; let i = index" class="accordion-item">
      <h2 class="accordion-header">
        <button class="accordion-button" type="button" data-bs-toggle="collapse">
          <b>{{getDayName(item)}}</b>
          <div class="mx-1"></div>
          {{getDateShort(item)}}
        </button>
      </h2>
      <div id="panel-{{i}}" class="accordion-collapse collapse show">
        <div class="accordion-body p-0">
          <p *ngIf="!hasTransactions(item)" class="text-center my-4" style="color: gray">No transactions</p>
          <app-transaction-list [transactions]="item.transactions"></app-transaction-list>
        </div>
      </div>
    </div>
    
  </div>

</div>
`

@Component({
  selector: 'app-transactions',
  template,
})
export class TransactionsComponent implements OnInit {

  weeklyTransactions: WeeklyTransactionsEntry[];

  constructor() { }

  ngOnInit(): void {
    this.init();
  }

  getDayName(item: WeeklyTransactionsEntry) {
    return dates.dayName(item.date)
  }

  getDateShort(item: WeeklyTransactionsEntry) {
    return dates.short(item.date)
  }

  hasTransactions(item: WeeklyTransactionsEntry) {
    return notNullNorEmpty(item.transactions)
  }

  private async init() {
    this.weeklyTransactions = [
      {
        date: new Date(),
        totalExpenses: 0,
        totalIncome: 0,
        transactions: arrays.fromRange(0, 3).map(index => ({
          amount: 100,
          category: {
            categoryDescriptorId: undefined,
            dateCreated: new Date(),
            dateUpdated: new Date(),
            description: 'Category',
          },
          categoryId: undefined,
          dateCreated: new Date(),
          dateUpdated: new Date(),
          description: 'This is a transaction',
          transactionEntryId: undefined,
          type: {
            dateCreated: new Date(),
            dateUpdated: new Date(),
            description: index % 2 == 0 ? TransactionType.expense : TransactionType.income,
            transactionTypeDescriptorId: undefined,
          },
          typeId: undefined,
          user: {
            dateCreated: new Date(),
            dateUpdated: new Date(),
            email: undefined,
            firstName: 'First',
            lastName: 'Last',
            password: undefined,
            userId: undefined,
            userName: undefined,
          },
          userId: undefined,
        })),
      },
      {
        date: dates.addDays(new Date(), 1),
        totalExpenses: 0,
        totalIncome: 0,
        transactions: [],
      },
      {
        date: dates.addDays(new Date(), 2),
        totalExpenses: 0,
        totalIncome: 0,
        transactions: [],
      },
    ]
  }

}
