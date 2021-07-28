import environment from '@/environment'
import storage from '@/storage'
import { notNullNorWhitespace } from '@/utils/validators'
import axios from 'axios'

axios.defaults.baseURL = environment.apiUrl

axios.interceptors.request.use(request => {

    const accessToken = storage.getAccessToken();
    
    if (notNullNorWhitespace(accessToken)) {
        request.headers['Authorization'] = `Bearer ${accessToken}`
    }
    
    return request
})
