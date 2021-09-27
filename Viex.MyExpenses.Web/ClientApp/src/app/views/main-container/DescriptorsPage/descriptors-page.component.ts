import { Component, OnInit } from '@angular/core';
import { LoadingModalService } from 'src/app/components/modals/LoadingModal/loading-modal.service';
import { ToastService } from 'src/app/components/Toast/toast.service';
import { TransactionCategoryDescriptor } from 'src/app/models/TransactionCategoryDescriptor.model';
import { ApiService } from 'src/app/services/api/api.service';
import { DescriptorType } from 'src/app/types/Descriptor.type';
import { fromFileToJson, openFiles } from 'src/app/utils/files';
import { waitSeconds } from 'src/app/utils/timers';
import { isNull, isNullOrEmpty, notNull, notNullNorEmpty } from 'src/app/utils/validators';

const template = /*html*/`
<app-header title="Descriptors" variant="menu"></app-header>

<div class="py-5">

  <div *ngFor="let item of descriptorTypes">
    <app-collapse-container [title]="item" (openedChange)="onDescriptorTypeExpanded($event, item)">
      <div class="p-3">

        <div class="mb-3">
          <app-flex justify="end">
            <button class="btn btn-danger ms-3">Drop</button>
            <button class="btn btn-success ms-3" (click)="onSeedClicked(item)">Seed</button>
          </app-flex>
        </div>

        <p *ngIf="!hasDescriptors(item)" class="text-center my-4">Looks like there aren't any descriptors!</p>

        <div *ngFor="let descriptor of getDescriptors(item)">
          <app-list-item [card]="true" [title]="descriptor">
          </app-list-item>
        </div>
        
      </div>
    </app-collapse-container>
  </div>

</div>
`

@Component({
  selector: 'app-descriptors-page',
  template,
})
export class DescriptorsPageComponent implements OnInit {

  descriptorTypes: string[]
  descriptorTypesMap = new Map<DescriptorType, any[]>();
  loading = false

  constructor(
    private api: ApiService,
    private loadingModal: LoadingModalService,
    private toasts: ToastService,
  ) { }

  ngOnInit(): void {
    this.init()
  }

  getDescriptors(type: DescriptorType) {
    return this.descriptorTypesMap.get(type)
  }

  hasDescriptors(type: DescriptorType) {
    if (!this.descriptorTypesMap.has(type))
      return false

    const descriptors = this.descriptorTypesMap.get(type)
    return notNullNorEmpty(descriptors)
  }

  async onDescriptorTypeExpanded(opened: boolean, type: DescriptorType) {
    if (!opened)
      return

    if (this.descriptorTypesMap.has(type))
      return

    const descriptors = await this.api.descriptors.getByType<any>(type)
    this.descriptorTypesMap.set(type, descriptors)
  }

  async onSeedClicked(type: DescriptorType) {
    const files = await openFiles({ accept: '.json' })

    if (isNullOrEmpty(files))
      return

    this.loadingModal.open({ message: `Seeding ${type}...` })

    try {
      const file = files.item(0)
      let descriptors: any[]

      if (type == 'TransactionCategories')
        descriptors = await fromFileToJson<TransactionCategoryDescriptor[]>(file)
      else
        descriptors = await fromFileToJson<string[]>(file)
      
      await waitSeconds(1)
      await this.api.descriptors.createAll(type, descriptors)
      this.descriptorTypesMap.set(type, descriptors)
      this.toasts.open({ message: `${type} seeded!`, variant: 'success' })
    }
    finally {
      this.loadingModal.close()
    }
  }

  private async init() {
    this.loading = true
    this.descriptorTypes = await this.api.descriptors.getDescriptorTypes()
    this.loading = false
  }

}
