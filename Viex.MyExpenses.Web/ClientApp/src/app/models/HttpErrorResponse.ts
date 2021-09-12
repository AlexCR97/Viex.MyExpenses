export class HttpResponse {

    message: string
    statusCode: number

    private constructor() {}

    static fromHttpResponse(httpResponse: any) {
        const response = new HttpResponse()
        response.message = httpResponse.message
        response.statusCode = httpResponse.status
        return response
    }

    get isBadRequest() { return this.statusCode == HttpStatusCode.badRequest }
    get isForbidden() { return this.statusCode == HttpStatusCode.forbidden }
    get isInternalServerError() { return this.statusCode == HttpStatusCode.internalServerError }
    get isNotFound() { return this.statusCode == HttpStatusCode.notFound }
    get isServiceUnavailable() { return this.statusCode == HttpStatusCode.serviceUnavailable }
    get isUnauthorized() { return this.statusCode == HttpStatusCode.unauthorized }
    get statusCodeDescription() { return HttpStatusCode[this.statusCode] }
}

export enum HttpStatusCode {
    // Informational
    ok = 200,
    created = 201,
    accepted = 202,
    noContent = 204,

    // Client side errors
    badRequest = 400,
    unauthorized = 401,
    forbidden = 403,
    notFound = 404,

    // Server side errors
    internalServerError = 500,
    serviceUnavailable = 503,
}
