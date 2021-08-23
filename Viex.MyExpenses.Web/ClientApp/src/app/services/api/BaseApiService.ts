import { environment } from "src/environments/environment"

export abstract class BaseApiService {
    
    abstract endpoint: string

    get url() {
        return `${environment.apiUrl}/${this.endpoint}`
    }
}