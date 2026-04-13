import axios from 'axios';

const BASE_URL = 'https://localhost:7224/api';

export default axios.create({
    baseURL: BASE_URL
});

export const axiosPrivate = axios.create({
    baseURL: BASE_URL,
    headers: { 'Content-Type': 'application/json' },
    withCredentials: true
});

export const authAxios = () => {
    const token = localStorage.getItem('accessToken');
    return axios.create({
        baseURL: BASE_URL,
        headers: { 
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    });
};