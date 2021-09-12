import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ValidationErrors } from 'fluentvalidation-ts/dist/ValidationErrors';
import { LoadingModalService } from 'src/app/components/modals/LoadingModal/loading-modal.service';
import { ToastService } from 'src/app/components/Toast/toast.service';
import { TransactionType } from 'src/app/models/Descriptors';
import { TransactionEntry, TransactionEntryValidator } from 'src/app/models/TransactionEntry.model';
import arrays from 'src/app/utils/arrays';
import objects from 'src/app/utils/objects';
import timers from 'src/app/utils/timers';
import { isNullOrZero } from 'src/app/utils/validators';

const template = /*html*/`
<div style="height: 50px">
  <app-header [title]="title" variant="back"></app-header>
</div>

<div class="container py-4">

  <app-text-field icon="currency-dollar" label="Amount" type="number" [errorMessage]="transactionValidations.amount" [(value)]="transaction.amount"></app-text-field>
  <div class="mb-4"></div>
  
  <app-text-field label="Description" type="text" [errorMessage]="transactionValidations.description" [(value)]="transaction.description"></app-text-field>
  <div class="mb-4"></div>
  
  <app-select label="Type" [errorMessage]="transactionValidations.transactionTypeDescriptor" [items]="types" [(value)]="transaction.transactionTypeDescriptor"></app-select>
  <div class="mb-4"></div>

  <app-date-picker [(value)]="transaction.dateCreated"></app-date-picker>
  <div class="mb-4"></div>
  
  <app-select label="Category" [errorMessage]="transactionValidations.categoryDescriptor" [items]="categories" [(value)]="transaction.categoryDescriptor"></app-select>
  <div class="mb-4"></div>

</div>

<app-fab icon="save2" [iconStyle]="{ 'font-size': '26px' }" (clicked)="onSaveClicked()"></app-fab>
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

  private transactionValidator = new TransactionEntryValidator()

  constructor(
    private loadingModal: LoadingModalService,
    private location: Location,
    private router: Router,
    private toast: ToastService,
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
    this.loadingModal.open({ message: 'Saving Transaction' })
    await timers.wait(2000)
    this.loadingModal.close()
    this.toast.open({ message: 'Transaction Saved', variant: 'success' })
    //await timers.wait(100)
    //this.router.navigateByUrl('/app/transactions')
  }

  private watchTransactionChanges() {
    this.transaction = objects.watch(new TransactionEntry(), (prop, value) => {
      console.log(`${prop} = ${value}`)
    })
  }

}
