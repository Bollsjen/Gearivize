import axios from "@/utils/axios";

export const authService = {
    login(email, password) {
        return axios.post(`/authentication/${email}/${password}`)
    },
}