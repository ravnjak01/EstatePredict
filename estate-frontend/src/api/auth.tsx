import axios from "axios";

const API_URL='http://localhost:5151/api/user/auth';

export const login = async (email:string, password:string) => {
    const response = await axios.post(`${API_URL}/login`, { email, password });
    if (response.data.token) {
        localStorage.setItem('userToken', response.data.token);
    }
    return response.data;
};

export const register = async (firstName:string, lastName:string, email:string, password:string) => {
    const response = await axios.post(`${API_URL}/register`, {
        firstName,
        lastName,
        email,
        password
    });
    return response.data;
};