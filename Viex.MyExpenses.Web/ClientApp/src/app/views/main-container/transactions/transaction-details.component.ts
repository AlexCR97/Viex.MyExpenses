import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ValidationErrors } from 'fluentvalidation-ts/dist/ValidationErrors';
import { TransactionEntry, TransactionEntryValidator } from 'src/app/models/TransactionEntry';
import { TransactionType } from 'src/app/models/TransactionTypeDescriptor';
import arrays from 'src/app/utils/arrays';
import objects from 'src/app/utils/objects';
import timers from 'src/app/utils/timers';
import { isNull, isNullOrZero } from 'src/app/utils/validators';

const template = /*html*/`
<div style="height: 50px">
  <app-header [title]="title" variant="back"></app-header>
</div>

<div class="container py-4">

  <app-text-field icon="currency-dollar" label="Amount" type="number" [errorMessage]="transactionValidations.amount" [(value)]="transaction.amount"></app-text-field>
  <div class="mb-4"></div>
  
  <app-text-field label="Description" type="text" [errorMessage]="transactionValidations.description" [(value)]="transaction.description"></app-text-field>
  <div class="mb-4"></div>
  
  <app-select label="Type" [errorMessage]="transactionValidations.type" [items]="types" [(value)]="transaction.type"></app-select>
  <div class="mb-4"></div>

  <app-date-picker [(value)]="transaction.dateCreated"></app-date-picker>
  <div class="mb-4"></div>
  
  <app-select label="Category" [errorMessage]="transactionValidations.category" [items]="categories" [(value)]="transaction.category"></app-select>
  <div class="mb-4"></div>

</div>

<app-fab icon="save2" [iconStyle]="{ 'font-size': '26px' }" (clicked)="onSaveClicked()"></app-fab>

<app-loading-modal message="Saving Transaction" [(opened)]="loadingModalOpened"></app-loading-modal>

<app-toast message="Transaction Saved" variant="success" [(opened)]="successToastOpened"></app-toast>

<app-toast message="Could not save transaction" variant="danger" [(opened)]="errorToastOpened"></app-toast>
`

@Component({
  selector: 'app-transaction-details',
  template,
})
export class TransactionDetailsComponent implements OnInit {

  transaction: TransactionEntry
  transactionValidations: ValidationErrors<TransactionEntry> = {}
  categories = arrays.fromRange(0, 5).map(index => `Category ${index + 1}`)
  types = [ TransactionType.expense, TransactionType.income ]

  loadingModalOpened = false
  successToastOpened = false
  errorToastOpened = false

  private transactionValidator = new TransactionEntryValidator()

  constructor(
    private location: Location,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.watchTransactionChanges()
  }

  get isTransactionValid() {
    return this.transactionValidator.isValid(this.transactionValidations)
  }

  get title() {
    return this.isNewTransaction
      ? 'New Transaction'
      : 'Edit Transaction'
  }

  get isNewTransaction() {
    return isNullOrZero(this.transaction.transactionEntryId)
  }

  async onSaveClicked() {
    this.transactionValidations = this.transactionValidator.validate(this.transaction)

    if (!this.isTransactionValid) {
      const snapshot = objects.clone(this.transactionValidations)
      this.transactionValidations = {}
      await timers.wait(1)
      this.transactionValidations = snapshot
      return
    }

    this.saveTransaction()
  }

  private async saveTransaction() {
    this.loadingModalOpened = true
    await timers.wait(2000)
    this.loadingModalOpened = false
    this.successToastOpened = true
    //await timers.wait(100)
    //this.router.navigateByUrl('/app/transactions')
  }

  private watchTransactionChanges() {
    this.transaction = objects.watch(new TransactionEntry(), (prop, value) => {
      console.log(`${prop} = ${value}`)
    })
  }

}
