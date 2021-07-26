import environment from "@/environment"

export abstract class BaseApi {
    
    abstract endpoint: string
    
    get uri() {
        return `${environment.apiUrl}/api/${this.endpoint}`
    }
}
