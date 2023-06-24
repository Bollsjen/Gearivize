import axios from 'axios'

const instance = axios.create({
    baseURL: 'https://localhost:5000/api',
    timeout: 15000,
    withCredentials: true
})

export default instance