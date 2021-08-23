import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Toast } from 'src/app/plugins/bootstrap.plugin';
import { isNull, notNull } from 'src/app/utils/validators';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  private optionsSubject = new BehaviorSubject<ToastOptions>(null)
  private toastHtmlElement: HTMLElement
  private toastId = 'toast'
  private toast: any

  getDefaultToastOptions(): ToastOptions {
    return {
      message: 'This is a toast',
      variant: 'primary',
    }
  }
  
  open(options: ToastOptions) {
    if (isNull(this.toastHtmlElement)) {
      this.toastHtmlElement = document.getElementById(this.toastId)
      this.toast = new Toast(this.toastHtmlElement)
    }

    const toastOptions = notNull(options)
      ? options
      : this.getDefaultToastOptions()

    this.optionsSubject.next(toastOptions)
    this.toast.show()
  }

  watchOptions() {
    return this.optionsSubject.asObservable()
  }
}

export interface ToastOptions {
  message: string
  variant: "primary" | "secondary" | "dark" | "light" | "info" | "success" | "danger"
}
