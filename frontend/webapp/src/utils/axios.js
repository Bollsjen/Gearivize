import axios from 'axios'

const instance = axios.create({
    baseURL: 'https://192.168.0.174:3000/api',
    timeout: 15000,
    withCredentials: true
})

export default instance