import axios from '@/utils/axios'

export const templateService = {
    getAll() {
        return axios.get(`/template`)
    },
    getById(id) {
        return axios.get(`/template/${id}`)
    },
    getInstruments() {
        return axios.get(`/template/Instruments`)
    },
    createTemplate(template) {
        return axios.post(`/template/Template`, template)
    },
    addInstrumentToTemplate(templateId, instrumentId) {
        return axios.post(`/template/Instrument/${templateId}/${instrumentId}`)
    },
    updateTemplate(template) {
        return axios.put(`/template/Template`, template)
    },
    deleteTemplate(id) {
        return axios.delete(`/template/${id}`)
    },
    deleteInstrumentFromTemplate(templateId, instrumentId) {
        return axios.delete(`/template/Instrument/${templateId}/${instrumentId}`)
    }
}