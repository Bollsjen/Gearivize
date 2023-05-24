import axios from '@/utils/axios'

export const notificationService = {

    getAll(){
        return axios.get(`notifications/by-user`)
    },

    updateNotification(notification){
        return axios.put(`notifications`, notification)
    }

}