import axios from "@/utils/axios";

export const userService = {
    getAll() {
        return axios.get(`/user`)
    },

    getAllNoPasswords() {
        return axios.get(`/user/no-password`)
    },

    getUserById(id){
        return axios.get(`/user/${id}`)
    },

    createUser(user){
        return axios.post(`/user`, user)
    },

    updateUser(user, id){
        return axios.put(`/user/${id}`, user)
    },

    deleteUser(id){
        return axios.delete(`/user/${id}`)
    },
}