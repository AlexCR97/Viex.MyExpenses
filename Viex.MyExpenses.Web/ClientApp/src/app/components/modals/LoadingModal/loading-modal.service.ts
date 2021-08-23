import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Modal } from 'src/app/plugins/bootstrap.plugin';
import { isNull, notNull } from 'src/app/utils/validators';
import { BaseModalService, ModalConfigurations, ModalOptions } from '../BaseModalService';

@Injectable({
  providedIn: 'root'
})
export class LoadingModalService extends BaseModalService<LoadingModalOptions> {

  getDefaultModalOptions(): LoadingModalOptions {
    return {
      title: 'Loading',
    }
  }

  getModalConfigurations(): ModalConfigurations {
    return {
      backdrop: 'static',
      keyboard: false,
    }
  }

  getModalId(): string {
    return 'loadingModal'
  }

  constructor() {
    super();
  }
}

export interface LoadingModalOptions extends ModalOptions {
  message?: string
}
