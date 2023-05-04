import axios from "@/utils/axios";

export const authService = {
    login(email, password) {
        return axios.post(`/authentication/${email}/${password}`)
    },

    logout(){
        return axios.delete(`/authentication`)
    },

    checkAuthenticationcation() {
        return axios.get(`/authentication`)
    }
}