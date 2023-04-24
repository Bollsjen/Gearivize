import axios from "@/utils/axios";

export const userService = {
    getAll() {
        return axios.get(`/user`)
    },

    getUserById(id){
        return axios.get(`/user/${id}`)
    },
}