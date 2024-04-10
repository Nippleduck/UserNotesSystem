import { createContext, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getUser, removeUser, storeUser, getToken } from './storage';

const AuthContext = createContext({});

const AuthProvider = ({ children }) => {
    const auth = useAuthState();
    return (
        <AuthContext.Provider value={ auth }>
            {children}
        </AuthContext.Provider>
    )
}

const useAuthState = () => {
    const [stateLoading, setStateLoading] = useState(true);
    const [currentUser, setUser] = useState(null);
    const [token, setToken] = useState(null);

    const navigate = useNavigate();

    useEffect(() => {
        if (currentUser === null) {
            setUser(getUser());
            setStateLoading(false);
        }
    }, []);

    useEffect(() => {
        if (token === null){
            setToken(getToken());
        }
    }, [token])

    const signIn = (data) => {
        const {userData, token} = storeUser(data);
        setUser(userData);
        setToken(token);
    }

    const signOut = () => {
        removeUser();
        setUser(null);
        setToken(null);
        navigate('/');
    }

    return {
        stateLoading,
        currentUser,
        token,
        signIn,
        signOut
    }
}

export default AuthContext;
export { AuthProvider }