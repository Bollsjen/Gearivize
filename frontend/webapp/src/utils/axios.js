import axios from 'axios'

const instance = axios.create({
    baseURL: 'https://192.168.150.70:5000/api',
    timeout: 15000,
    withCredentials: true
})

export default instance