import axios from "axios";

export default axios.create({
    baseURL: process.env.REACT_APP_API_URL 
});

const authAxios = axios.create({
    baseURL: process.env.REACT_APP_API_URL,
    headers: { 
        'Content-Type': 'application/json', 
        'Access-Control-Allow-Origin': '*'
     },
    withCredentials: true
});

export { authAxios }