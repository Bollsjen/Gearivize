import axios from '@/utils/axios'

export const importService = {
    importInstruments(file) {
        return axios.post('/import/instruments', file, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        })
    }
}