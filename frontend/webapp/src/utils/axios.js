import axios from 'axios'

const instance = axios.create({
    baseURL: 'http://localhost:8081/api',
    timeout: 15000,
    withCredentials: true
})

export default instance