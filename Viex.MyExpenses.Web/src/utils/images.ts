export default {
    dimensions(imageFile: File): Promise<{ width: number, height: number }> {
        return new Promise<{ width: number, height: number }>((resolve, reject) => {
            const reader = new FileReader();

            reader.readAsDataURL(imageFile);

            reader.onload = $event => {
                const image = new Image();

                image.src = ($event as any).target.result as string;

                image.onload = () => {
                    const height = image.height;
                    const width = image.width;
                    resolve({ width, height });
                };

                image.onerror = error => reject(error);
            };
        })
    },
}

