import { notNull, notNullNorWhitespace } from "./validators";

export function openFiles(options?: { accept?: string, multiple?: boolean }): Promise<FileList> {
    return new Promise((resolve, reject) => {
        const input = document.createElement('input')
        input.type = 'file'

        if (notNull(options)) {
            input.accept = options.accept
            input.multiple = options.multiple
        }

        input.click();

        input.onchange = () => {
            const files = (input as any).files as FileList
            resolve(files)
        }

        input.onerror = error => reject(error)
    });
}

export function fromFileToJson<T>(file: File): Promise<T> {
    return new Promise((resolve, reject) => {
        const reader = new FileReader()
        reader.readAsText(file, "UTF-8")
        reader.onload = () => resolve(JSON.parse(reader.result as string))
        reader.onerror = (error) => reject(error)
    });
}
