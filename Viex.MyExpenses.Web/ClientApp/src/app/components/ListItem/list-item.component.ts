import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { notNullNorWhitespace } from 'src/app/utils/validators';

const template = /*html*/`
<ng-template #listItemContainer>
  <app-flex align="center" width="100%">

    <!-- img -->
    <img *ngIf="img" class="img-fluid rounded-circle profile me-3" [src]="img" alt="cover" style="width: 60px; height: 60px" (error)="onImageNotFound()">
  
    <!-- icon -->
    <div *ngIf="icon" class="app-center-both me-3">
      <span class="material-icons" [ngStyle]="iconStyle">{{icon}}</span>
    </div><!-- icon -->
    
    <!-- title and subtitle -->
    <app-flex [align]="'center'">
      <div>
        <p class="m-0">{{title}}</p>
        <p *ngIf="subtitle" class="ma-0" style="color: gray; font-size: 14px;">{{subtitle}}</p>
      </div>
    </app-flex><!-- title and subtitle -->
    
    <!-- leading -->
    <div class="ms-auto">
      <ng-content select="[slot='trailing']"></ng-content>
    </div><!-- leading -->

  </app-flex>
</ng-template>

<div (click)="onClicked()">
  <div *ngIf="card" class="card shadow-sm">
    <div class="card-body px-3 py-3">
      <ng-template [ngTemplateOutlet]="listItemContainer"></ng-template>
    </div>
  </div>
  <div *ngIf="!card" class="px-3 py-3">
    <ng-template [ngTemplateOutlet]="listItemContainer"></ng-template>
  </div>
</div>
`

@Component({
  selector: 'app-list-item',
  template,
})
export class ListItemComponent implements OnInit {

  @Input() card = false
  @Input() icon: string
  @Input() iconColor: string
  @Input() img: string
  @Input() title = "Title"
  @Input() subtitle: string

  @Output() clicked = new EventEmitter<void>();

  constructor() { }

  ngOnInit(): void { }

  get iconStyle() {
    return {
      "font-size": "28px",
      "color": notNullNorWhitespace(this.iconColor)
        ? this.iconColor
        : "gray",
    };
  }

  onClicked() {
    this.clicked.emit();
  }

  onImageNotFound() {
    // TODO Implement onImageNotFound
  }

}
