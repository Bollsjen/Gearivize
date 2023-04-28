import axios from '@/utils/axios'

export const storeService = {
    checkAuthenticationcation() {
        return axios.get(`/authentication`)
    }
}