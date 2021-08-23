import { Injectable } from '@angular/core';
import { BaseModalService, ModalConfigurations, ModalOptions } from '../BaseModalService';

@Injectable({
  providedIn: 'root'
})
export class ConfirmModalService extends BaseModalService<ConfirmModalOptions> {

  getDefaultModalOptions(): ConfirmModalOptions {
    return {
      title: 'Are you sure?',
      message: 'Do you really want to perform this action?',
      textCancel: 'Cancel',
      textConfirm: 'Confirm',
    }
  }

  getModalConfigurations(): ModalConfigurations {
    return {
      backdrop: 'static',
      keyboard: false,
    }
  }

  getModalId(): string {
    return 'confirmModal'
  }

  constructor() {
    super();
  }
}

export interface ConfirmModalOptions extends ModalOptions {
  message: string
  textCancel: string
  textConfirm: string
  onConfirm?: () => void
  onConfirmAsync?: () => Promise<void>
}
