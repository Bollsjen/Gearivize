import axios from '@/utils/axios'

export const templateService = {
    getAll() {
        return axios.get(`/template`)
    },
}