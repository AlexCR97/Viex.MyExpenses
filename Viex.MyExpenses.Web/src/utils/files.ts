import { notNull, notNullNorWhitespace } from "./validators";

export default {
  toBase64(file: File) {
    return new Promise<string>((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result as string);
      reader.onerror = error => reject(error);
    });
  },
  
  /**
   * Receives a JSON file and returns contents
   * 
   * usage:
   * 
   * async function() {
   *    const foo = await this.getJsonContents<Type>(file);
   * }
   * 
   * @param file JSON file
   * @returns Promise of contents
   */
  getJsonContents<T>(file: File): Promise<T> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsText(file, "UTF-8");
      reader.onload = () => resolve(JSON.parse(reader.result as string));
      reader.onerror = (error) => reject(error);
    });
  },

  triggerFileInputClick(options?: { accept?: string }): Promise<FileList> {
    return new Promise<FileList>((resolve, reject) => {
      const input = document.createElement('input');
      input.type = 'file';
      
      if (notNull(options)) {
        if (notNullNorWhitespace(options.accept))
          input.accept = options.accept;
      }
  
      input.click();

      input.onchange = () => {
        const files = (input as any).files;
        resolve(files);
      }

      input.onerror = error => reject(error);
    });
  },
}
