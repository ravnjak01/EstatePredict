import axios from "axios";
import api from "../interceptor/axios_interceptor";


export const login = async (email:string, password:string) => {
    const response = await api.post(`user/login`, { email, password });
    if (response.data.token) {
        localStorage.setItem('userToken', response.data.token);
    }
    return response.data;
};

export const register = async (firstName:string, lastName:string, email:string, password:string) => {
    const response = await axios.post(`user/register`, {
        firstName,
        lastName,
        email,
        password
    });
    return response.data;
};