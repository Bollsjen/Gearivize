import axios from "@/utils/axios";

export const userService = {
    getAll() {
        return axios.get(`/user`)
    },

    getUserById(id){
        return axios.get(`/user/${id}`)
    },

    createUser(user){
        return axios.post(`/user`, user)
    },

    updateUser(user, id){
        return axios.post(`/user/${id}`, user)
    },

    deleteUser(id){
        return axios.delete(`/user/${id}`)
    },
}