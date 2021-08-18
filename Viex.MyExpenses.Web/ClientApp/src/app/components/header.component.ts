import { Location } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { isNull, notNullNorEmpty } from '../utils/validators';

const template = /*html*/`
<div class="fixed-top shadow py-1" style="background-color: white;">
  <div class="d-flex justify-content-between align-items-center px-2 py-1">

    <app-icon-button *ngIf="isVariantNone" [disabled]="true"></app-icon-button>

    <app-icon-button *ngIf="isVariantBack" icon="chevron-left" (click)="onBackClicked()"></app-icon-button>
    
    <button *ngIf="isVariantMenu" class="btn d-flex justify-content-center align-items-center rounded-circle p-2" style="width: 40px; height: 40px;" (click)="onMenuClicked()">
      <i class="bi bi-list"></i>
    </button>

    <p class="fw-bold m-0">{{title}}</p>

    <app-icon-button *ngIf="!hasActions" [disabled]="true"></app-icon-button>

    <app-icon-button *ngIf="hasActions" icon="three-dots-vertical"></app-icon-button>

  </div>
</div>

<br><br><br>
<br><br><br>

<app-side-navigator [(opened)]="drawerOpened"></app-side-navigator>
`

@Component({
  selector: 'app-header',
  template,
})
export class HeaderComponent implements OnInit {

  @Input() actions: any[]
  @Input() title: string
  @Input() variant: 'menu' | 'back'

  drawerOpened = false

  constructor(
    private location: Location,
  ) { }

  ngOnInit(): void { }

  get hasActions() {
    return notNullNorEmpty(this.actions)
  }

  get isVariantNone() {
    return isNull(this.variant)
  }

  get isVariantBack() {
    return this.variant == 'back'
  }

  get isVariantMenu() {
    return this.variant == 'menu'
  }

  onBackClicked() {
    this.location.back();
  }

  onMenuClicked() {
    this.drawerOpened = true
  }

}
