const assign = <T>(obj: T) => Array.isArray(obj)
    ? Object.assign([], obj)
    : Object.assign({}, obj);

const stringify = <T>(obj: T) => JSON.parse(JSON.stringify(obj));

export default {
    clone<T>(source: T, options?: { method?: "assign" | "stringify" }): T {
        if (!options)
            return stringify(source);
        
        if (options.method == "assign")
            return assign(source);

        if (options.method == "stringify")
            return stringify(source);

        return stringify(source);
    },

    cloneMap<T, U>(source: Map<T, U>) {
        const clone = new Map<T, U>();

        source.forEach((value, key) => {
            clone.set(key, value);
        });

        return clone;
    },

    dot<T extends Object>(source: any, dotNotation: string): T | undefined {
        const properties = dotNotation.split('.');
        let currentValue = source;

        for (const property of properties) {
            if (!currentValue) return undefined;
            currentValue = currentValue[property];
        }

        return currentValue;
    },

    guid(): string {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    },

    watch<T extends object>(target: T, listener: (property: string, value: any) => void) {
        return new Proxy(target, {
            set(obj, prop, value) {
                obj[prop] = value
                listener(prop as string, value)
                return true
            }
        })
    },
}
