import axios from '@/utils/axios'

export const instrumentsService = {
    getAll(){
        return axios.get(`/instruments`)
    },
    createNewInstrument(instrument){
        return axios.post(`/instruments`, instrument)
    },

    updateInstrument(instrument){
        return axios.put(`/instruments`, instrument)
    },

    activateInstrument(instrument){
        return axios.put(`/instruments/inactive`, instrument)
    },

    deleteInstrument(aNumber){
        return axios.delete(`/instruments/inactive/${aNumber}`)
    },

    actualDeleteInstrument(aNumber){
        return axios.delete(`/instruments/${aNumber}`)
    }
}