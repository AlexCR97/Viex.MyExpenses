export default {
    wait(milliseconds: number) {
        return new Promise<void>(resolve => {
            setTimeout(() => resolve(), milliseconds)
        })
    }
}

export function waitMilliseconds(milliseconds: number) {
    return new Promise<void>(resolve => {
        setTimeout(() => resolve(), milliseconds)
    })
}

export function waitSeconds(seconds: number) {
    return waitMilliseconds(seconds * 1000)
}
