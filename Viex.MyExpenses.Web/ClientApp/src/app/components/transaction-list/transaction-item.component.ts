import { Component, Input, OnInit } from '@angular/core';
import { TransactionEntry } from 'src/app/models/TransactionEntry';
import { TransactionType } from 'src/app/models/TransactionTypeDescriptor';
import timers from 'src/app/utils/timers';
import { notNull } from 'src/app/utils/validators';

const template = /*html*/`
<div class="list-group-item p-0 py-3 pe-2" (click)="onClicked()">
  <div class="d-flex align-items-center">
    
    <span class="text-end me-3" style="min-width: 70px;" [ngClass]="amountClass">
      {{amount}}
    </span>
    
    <div class="me-auto">
      <p class="m-0">{{transaction.description}}</p>
      <p *ngIf="hasCategory" class="m-0" style="color: gray; font-size: 14px">{{transaction.category}}</p>
    </div>

  </div>
</div>

<app-bottom-drawer [(opened)]="bottomDrawerOpened">

  <div class="p-3">
    <p class="m-0">
      <span class="text-end me-3" style="min-width: 70px;" [ngClass]="amountClass">
        {{amount}}
      </span>
      {{transaction.description}}
    </p>
  </div>

  <div class="list-group">

    <div class="list-group-item list-group-item-action py-3">
      <div class="d-flex align-items-center">
        <i class="bi bi-pencil ms-2 me-3"></i>
        <h6 class="m-0">Edit</h6>
      </div>
    </div>

    <div class="list-group-item list-group-item-action py-3" (click)="onDeleteClicked()">
      <div class="d-flex align-items-center">
        <i class="bi bi-trash text-danger ms-2 me-3"></i>
        <h6 class="text-danger m-0">Delete</h6>
      </div>
    </div>

  </div>
</app-bottom-drawer>

<app-confirm-modal
  [(opened)]="confirmModalOpened"
  (confirmed)="onDeleteConfirmed()">
</app-confirm-modal>

<app-loading-modal
  [(opened)]="loadingModalOpened">
</app-loading-modal>
`

@Component({
  selector: 'app-transaction-item',
  template,
})
export class TransactionItemComponent implements OnInit {

  @Input() transaction = new TransactionEntry()

  bottomDrawerOpened = false
  confirmModalOpened = false
  loadingModalOpened = false

  constructor() { }

  ngOnInit(): void {
  }

  get amount(): string {
    const operator = this.isExpense
      ? '-'
      : '+'

    return `${operator} $${this.transaction.amount}`
  }

  get amountClass() {
    return {
      'text-success': this.isIncome,
      'text-danger': this.isExpense,
    };
  }

  get hasCategory() {
    return notNull(this.transaction.category)
  }

  get isExpense() {
    return this.transaction.type == TransactionType.expense
  }

  get isIncome() {
    return this.transaction.type == TransactionType.income
  }

  onClicked() {
    this.bottomDrawerOpened = true
  }

  onDeleteClicked() {
    console.log("this.confirmModalOpened:", this.confirmModalOpened);
    this.confirmModalOpened = true;
  }

  async onDeleteConfirmed() {
    this.loadingModalOpened = true
    await timers.wait(2000);
    this.loadingModalOpened = false
    this.bottomDrawerOpened = false
  }

}