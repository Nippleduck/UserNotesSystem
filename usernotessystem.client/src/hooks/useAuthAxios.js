import { useEffect } from "react";
import { authAxios } from "../api/axiosApi";
import useAuth from "./useAuth";

const useAuthAxios = () => {
    const { token } = useAuth();

    useEffect(() => {
        const requestInterceptor = authAxios.interceptors.request.use(
            config => {
                if (!config.headers['Authorization']) {
                    config.headers['Authorization'] = `Bearer ${token}`;
                }
                return config;
            }, (error) => Promise.reject(error)
        );

        return () => {
            authAxios.interceptors.request.eject(requestInterceptor)
        };
    }, [token])

    return authAxios;
}

export default useAuthAxios;