import axios from '@/utils/axios'

export const instrumentsService = {
    getAll(){
        return axios.get(`/Instruments`)
    }
}