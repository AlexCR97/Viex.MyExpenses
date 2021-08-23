import { BehaviorSubject } from "rxjs"
import { Modal } from "src/app/plugins/bootstrap.plugin"
import { isNull, notNull } from "src/app/utils/validators"

export abstract class BaseModalService<TOptions extends ModalOptions> {

    private optionsSubject = new BehaviorSubject<TOptions>(null)
    private modalHtmlElement: HTMLElement
    private modal: any

    abstract getDefaultModalOptions(): TOptions
    abstract getModalConfigurations(): ModalConfigurations
    abstract getModalId(): string

    open(options?: TOptions) {
        if (isNull(this.modalHtmlElement)) {
            this.modalHtmlElement = document.getElementById(this.getModalId())
            this.modal = new Modal(this.modalHtmlElement, this.getModalConfigurations())
        }

        const modalOptions = notNull(options)
            ? options
            : this.getDefaultModalOptions()
        
        this.optionsSubject.next(modalOptions)
        this.modal.show()
    }

    close() {
        this.modal.hide()
    }

    watchOptions() {
        return this.optionsSubject.asObservable()
    }
}

export interface ModalConfigurations {
    backdrop?: boolean | 'static'
    focus?: boolean
    keyboard?: boolean
}

export interface ModalOptions {
    title?: string
}
