import axios from '@/utils/axios'

export const instrumentsService = {
    getAll(){
        return axios.get(`/instruments`)
    },

    getByXMonths(months, external){
        return axios.get(`/instruments/list/x-months/${months}/${external}`)
    },

    getOverdue(external){

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
    }
}