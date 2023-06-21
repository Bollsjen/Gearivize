import axios from 'axios'

const instance = axios.create({
    baseURL: 'https://176.23.69.93:3000/api',
    timeout: 15000,
    withCredentials: true
})

export default instance