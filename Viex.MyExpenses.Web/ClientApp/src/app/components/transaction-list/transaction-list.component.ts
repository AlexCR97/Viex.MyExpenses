import { Component, Input, OnInit } from '@angular/core';
import { TransactionEntry } from 'src/app/models/TransactionEntry.model';

const template = /*html*/`
<div class="list-group list-group-flush">
  <app-transaction-item *ngFor="let item of transactions"
    [transaction]="item">
  </app-transaction-item>
</div>
`

@Component({
  selector: 'app-transaction-list',
  template,
})
export class TransactionListComponent implements OnInit {

  @Input() transactions: TransactionEntry[]

  constructor() { }

  ngOnInit(): void {
  }

}
