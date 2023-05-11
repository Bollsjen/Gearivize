import axios from '@/utils/axios'

export const instrumentsService = {
    getAll(){
        return axios.get(`/instruments`)
    },

    getImage(imageName){
        return axios.get(`instruments/image/${imageName}`, {
            responseType: 'blob'
        })
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
    },


    uploadInstrumentImage(formData, aNumber){
        console.log(aNumber)
        console.log(formData)
        return axios.post(`/instruments/${aNumber}`, formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        })
    }
}